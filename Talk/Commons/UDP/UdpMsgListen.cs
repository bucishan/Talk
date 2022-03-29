using Talk.Commons.Controls;
using Talk.Commons.Tools;
using Talk.DB;
using Talk.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Talk.Commons.UDP
{
    /// <summary>
    /// 监听UDP消息类
    /// </summary>
    public class UdpMsgListen
    {
        private UCTabPageOnlineMenu OnlineMenu;         //在线用户列表
        private Control ChatContainer;                  //通信窗口容器

        public UdpMsgListen(UCTabPageOnlineMenu ctrlMenu, Control ctrlContainer)
        {
            OnlineMenu = ctrlMenu;
            ChatContainer = ctrlContainer;
        }

        /// <summary>
        /// 查找当前消息的通信窗口
        /// </summary>
        /// <returns>通讯窗口对象</returns>
        public UCChat GetChatWindow(UserInfo user)
        {
            UCChat chatControl = null;
            ChatContainer.Invoke(new Action(() =>
            {
                if (ChatContainer.Controls.Find("chat_" + user.MachineCode, true).Count() > 0)
                {
                    chatControl = (UCChat)ChatContainer.Controls.Find("chat_" + user.MachineCode, true)[0];
                }
            }));
            return chatControl;
        }
        //在程序运行后保持监听2425端口，负责处理各种类型消息
        //1.用户第一次登陆发送":USER:"消息，收到此类消息会将对方加入到自己的在线好友列表中
        //2.接收到":USER:"消息后，向对方发送":REPY:"消息，让对方将自己加入好友列表中
        //3.
        public void StartUdpThread()
        {
            UdpClient udpClient = new UdpClient(2425);
            //绑定任意ip地址和一个端口号用来接收别人的信息
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                byte[] buff = udpClient.Receive(ref ipEndPoint);
                string userInfo = Encoding.UTF8.GetString(buff);
                string msgHead = userInfo.Substring(0, 6);//消息前6位为消息类型标识符
                string msgBody = userInfo.Substring(6);//第7位开始为消息实体内容

                switch (msgHead)
                {
                    /*用户第一次登录时发送USER消息到广播地址，收到此类消息会将对方
                     * 加入到自己的在线好友列表中，并回复对方消息，告诉对方自己在线 */
                    case ":USER:":
                        try
                        {
                            string[] mBody = msgBody.Split(':');
                            //添加用户
                            UserInfo user = new UserInfo();
                            user.MachineCode = mBody[0];
                            user.UserName = mBody[1];
                            user.ComputerName = mBody[2];
                            user.IP = mBody[3];
                            user.UserGroup = mBody[4];
                            user.HeadPic = mBody[5];
                            user.RSAPublic = mBody[6];

                            OnlineMenu.AddMenuItem(user);

                            //创建数据表
                            DBSqlite.CreateChatTable(user.MachineCode);

                            //回复消息
                            UdpBoardCast CReply = new UdpBoardCast();
                            CReply.BCReply(user.IP);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;

                    //聊天消息MESG
                    case ":MESG:":
                        try
                        {
                            NotifyIconTool.IconFlashingStart();
                            SoundTool.PlaySound();

                            string[] mBody = msgBody.Split('|');
                            UserInfo user = new UserInfo();
                            user.MachineCode = mBody[0];
                            user.UserName = mBody[1];
                            user.ComputerName = mBody[2];
                            user.IP = mBody[3];
                            string EncryptMsg = mBody[4];
                            string DecryptMsg = RSATool.Decryption(EncryptMsg);

                            MsgStatus msg_status = MsgStatus.UnRead;
                            //显示消息到窗口
                            UCChat chat = GetChatWindow(user);
                            if (chat != null)
                            {
                                chat.OutputMessage(ChatType.MESG, ChatStatus.Invalid, MsgType.Other, DecryptMsg);
                                msg_status = MsgStatus.HaveRead;
                            }

                            //插入已发送的通信信息到通信表
                            DBSqlite.InsertChatTable(user.MachineCode, MsgType.Other, user.IP, DecryptMsg, msg_status, ChatType.MESG);

                            //显示消息到在线用户菜单项中
                            UCMenuItem item = OnlineMenu.GetMenuItem(user);
                            item.ItemLatestMsg = DecryptMsg;
                            item.ItemLatestMsgTime = Tool.GetLongDateTime();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;

                    //以GAME开头的消息，表示有人发送有游戏请求
                    case ":GAME:":
                        try
                        {
                            NotifyIconTool.IconFlashingStart();
                            SoundTool.PlaySound();

                            string[] mBody = msgBody.Split('|');
                            UserInfo user = new UserInfo();
                            user.MachineCode = mBody[0];
                            user.IP = mBody[1];
                            int gameType = Convert.ToInt32(mBody[2]);
                            string EncryptMsg = mBody[3];
                            string DecryptMsg = RSATool.Decryption(EncryptMsg);

                            //显示消息到在线用户菜单项中
                            UCMenuItem item = OnlineMenu.GetMenuItem(user);
                            item.ItemLatestMsg = DecryptMsg;
                            item.ItemLatestMsgTime = Tool.GetLongDateTime();

                            ChatType _ChatType = ChatType.GAME;
                            ChatStatus _ChatStatus = ChatStatus.Effective;
                            switch ((GameType)gameType)
                            {
                                case GameType.Request:
                                    {
                                        if (__Global.IsGameIn)
                                        {
                                            _ChatStatus = ChatStatus.Invalid;
                                            UdpSendMessage send = new UdpSendMessage(item.ItemUserInfo);
                                            send.SendGameMessage(GameType.GameIn, "我的游戏正在进行ing...");
                                        }
                                        else
                                        {
                                            _ChatStatus = ChatStatus.Effective;
                                        }
                                        _ChatType = ChatType.GAME;
                                    }
                                    break;
                                case GameType.Agree:
                                    {
                                        if (__Global.Game_Gobang != null)
                                        {
                                            __Global.Game_Gobang.SetGameStatus("对方已连接");
                                        }
                                        _ChatType = ChatType.MESG;
                                        _ChatStatus = ChatStatus.Invalid;
                                        DBSqlite.UpdateChatTypeToSpecify(user.MachineCode, ChatType.GAME, ChatStatus.Invalid);
                                    }
                                    break;
                                case GameType.DisAgree:
                                    {
                                        if (__Global.Game_Gobang != null)
                                        {
                                            __Global.Game_Gobang.SetGameStatus("对方已拒绝");
                                        }
                                        //__Global.Game_Gobang.CloseNetwork();
                                        _ChatType = ChatType.MESG;
                                        _ChatStatus = ChatStatus.Invalid;
                                        DBSqlite.UpdateChatTypeToSpecify(user.MachineCode, ChatType.GAME, ChatStatus.Invalid);
                                    }
                                    break;
                                case GameType.GameIn:
                                    {
                                        if (__Global.Game_Gobang != null)
                                        {
                                            __Global.Game_Gobang.SetGameStatus("对方正在游戏中");
                                        }
                                        //__Global.Game_Gobang.CloseNetwork();
                                        _ChatType = ChatType.MESG;
                                        _ChatStatus = ChatStatus.Invalid;
                                        DBSqlite.UpdateChatTypeToSpecify(user.MachineCode, ChatType.GAME, ChatStatus.Invalid);
                                    }
                                    break;
                            }
                            MsgStatus msg_status = MsgStatus.UnRead;

                            //显示消息到窗口
                            UCChat chat = GetChatWindow(user);
                            if (chat != null)
                            {
                                chat.OutputMessage(ChatType.GAME, _ChatStatus, MsgType.Other, DecryptMsg);
                                msg_status = MsgStatus.HaveRead;
                            }

                            //插入已发送的通信信息到通信表
                            DBSqlite.InsertChatTable(user.MachineCode, MsgType.Other, user.IP, DecryptMsg, msg_status, _ChatType, _ChatStatus);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;

                    //用户退出时发送QUIT开头的消息
                    case ":QUIT:":
                        try
                        {
                            string[] mBody = msgBody.Split('|');
                            UserInfo user = new UserInfo();
                            user.MachineCode = mBody[0];
                            OnlineMenu.RemoveMenuItem(user);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;

                    /*自己上线时会向广播发送消息，在接到别人以REPY开头的回复消息时，
                    将对方加入自己的在线好友列表中*/
                    case ":REPY:":
                        try
                        {
                            string[] mBody = msgBody.Split(':');
                            //添加用户
                            UserInfo user = new UserInfo();
                            user.MachineCode = mBody[0];
                            user.UserName = mBody[1];
                            user.ComputerName = mBody[2];
                            user.IP = mBody[3];
                            user.UserGroup = mBody[4];
                            user.HeadPic = mBody[5];
                            user.RSAPublic = mBody[6];
                            OnlineMenu.AddMenuItem(user);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    //以DATA开头的消息，表示有人发送文件
                    case ":DATA:":
                        try
                        {
                            //string[] mBody = msgBody.Split('|');
                            //string msgName = mBody[0];
                            //string msgID = mBody[1];
                            //string msgIP = mBody[2];
                            //string msgFileName = mBody[3];
                            //string msgFileLen = mBody[4];

                            //string msgDetail = "【发送文件】" + msgFileName;
                            ////创建一条新线程接收消息
                            //ClassReceiveMsg cRecMsg = new ClassReceiveMsg(msgIP, msgName, msgID, msgDetail);
                            //Thread tRecMsg = new Thread(new ThreadStart(cRecMsg.StartRecMsg));
                            //tRecMsg.Start();
                        }
                        catch
                        {
                        }
                        break;

                    //接到以ACEP开头的消息，表示对方同意接收文件
                    case ":ACEP:":
                        try
                        {
                            //string[] mFileBody = msgBody.Split('|');
                            //string mFilePath = mFileBody[3];
                            //string mIP = mFileBody[2];

                            //ClassSendFile cSendFile = new ClassSendFile(mFilePath, mIP);
                            //Thread tSendFile = new Thread(new ThreadStart(cSendFile.SendFile));
                            //tSendFile.IsBackground = true;
                            //tSendFile.Start();
                        }
                        catch
                        {
                        }
                        break;
                }
            }
        }
    }
}
