﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Gobangs
{
    public class NetworkInterface
    {
        public delegate void Act();

        public Gobang Owner { get; private set; }

        public event EventHandler ClientConnected;
        public event EventHandler Connected;
        public event EventHandler AbortedByException;

        public bool Running
        {
            get
            {
                return server != null || socket != null || acceptThread != null || listenThread != null || sendThread != null;
            }
        }

        List<byte[]> messages;

        Random random;
        Socket server;
        Socket socket;
        Thread acceptThread;
        Thread listenThread;
        Thread sendThread;

        public NetworkInterface(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Gobang))
            {
                throw new Exception("无效的父对象!");
            }

            Owner = (Gobang)obj;
            messages = new List<byte[]>();
            random = new Random();
        }

        public void Start(int port)
        {
            if (Running)
            {
                throw new Exception("对象非空!");
            }
            try
            {
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Bind(new IPEndPoint(IPAddress.Any, port));
                server.Listen(1);
            }
            catch
            {
                if (server != null)
                {
                    server.Close();
                    server = null;
                }
                throw;
            }

            acceptThread = new Thread(accept);
            acceptThread.Start();
            Talk.__Global.IsGameIn = true;
        }

        void accept()
        {
            socket = server.Accept();
            server.Close();
            server = null;

            messages.Clear();

            listenThread = new Thread(receiver);
            listenThread.Start();

            sendThread = new Thread(sender);
            sendThread.Start();

            if (ClientConnected != null)
            {
                ClientConnected(this, new EventArgs());
            }
        }

        public void Connect(IPEndPoint ip)
        {
            if (Running)
            {
                throw new Exception("对象非空!");
            }

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ip);
            }
            catch
            {
                if (socket != null)
                {
                    socket.Close();
                    socket = null;
                }
                throw;
            }

            messages.Clear();

            listenThread = new Thread(receiver);
            listenThread.Start();

            sendThread = new Thread(sender);
            sendThread.Start();

            if (Connected != null)
            {
                Connected(this, new EventArgs());
            }
            Talk.__Global.IsGameIn = true;
        }

        public void Send(MessageType type, int x = 0, int y = 0)
        {
            byte[] head = new byte[StreamHead.HeadLength];
            StreamHead.WriteHead(head, type, x, y);

            lock (messages)
            {
                messages.Add(head);
            }
        }

        void receiver()
        {
            while (true)
            {
                MessageType type;
                int x, y;

                byte[] head = new byte[StreamHead.HeadLength];
                byte[] temp = new byte[StreamHead.HeadLength];

                try
                {
                    int sub = StreamHead.HeadLength;
                    int length;
                    while (true)
                    {
                        int l = (temp.Length < sub) ? temp.Length : sub;
                        length = socket.Receive(temp, l, SocketFlags.None);
                        if (length <= 0)
                        {
                            goto End;
                        }
                        if (length == sub)
                        {
                            Array.Copy(temp, 0, head, head.Length - sub, length);
                            break;
                        }
                        else // if (length < sub)
                        {
                            Array.Copy(temp, 0, head, head.Length - sub, length);
                            sub -= length;
                        }
                    }
                    StreamHead.Read(head, out type, out x, out y);

                    if (type == MessageType.Reset)
                    {
                        Gobang.Piece self = random.Next(0, 2) == 0 ? Gobang.Piece.Black : Gobang.Piece.White;
                        Gobang.Piece now = Gobang.Piece.Black;

                        // 先设置棋子 再请求刷新 否则可能不一致
                        Owner.RefreshText(self, now);
                        Owner.Reset();

                        Send(MessageType.Piece, (int)Gobang.Reserve(self), (int)now);
                    }
                    if (type == MessageType.Piece)
                    {
                        Gobang.Piece self = (Gobang.Piece)x;
                        Gobang.Piece now = (Gobang.Piece)y;

                        Owner.RefreshText(self, now);
                        Owner.Reset();
                    }
                    if (type == MessageType.Set)
                    {
                        Owner.Set(new Point(x, y), false);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    goto End;
                }
            }

        End:
            new Thread(abort).Start();
        }

        void sender()
        {
            while (true)
            {
                byte[] temp = null;

                lock (messages)
                {
                    if (messages.Count > 0)
                    {
                        temp = messages[0];
                        messages.RemoveAt(0);
                    }
                }

                if (temp != null)
                {
                    try
                    {
                        socket.Send(temp);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        goto End;
                    }
                }
                else
                {
                    Thread.Sleep(1);
                }
            }

        End:
            new Thread(abort).Start();
        }

        /// <summary>
        /// 关闭连接和清理线程
        /// </summary>
        public void Abort()
        {
            if (server != null)
            {
                server.Close();
                server = null;
            }
            if (socket != null)
            {
                socket.Close();
                socket = null;
            }
            if (acceptThread != null)
            {
                acceptThread.Abort();
                acceptThread = null;
            }
            if (listenThread != null)
            {
                listenThread.Abort();
                listenThread = null;
            }
            if (sendThread != null)
            {
                sendThread.Abort();
                sendThread = null;
            }
            Talk.__Global.IsGameIn = false;
        }

        void abort()
        {
            Abort();

            if (AbortedByException != null)
            {
                AbortedByException(this, new EventArgs());
            }
        }
    }
}
