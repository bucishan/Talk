using Talk.Commons.Tools;
using Talk.DB;
using Talk.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Talk.Commons.UDP
{
    /// <summary>
    /// 消息发送类
    /// </summary>
    public class UdpSendMessage
    {
        /// <summary>
        /// 目标用户信息
        /// </summary>
        private UserInfo UserTarget;

        /// <summary>
        /// 本地用户信息
        /// </summary>
        private UserInfo UserLocal;

        /// <summary>
        /// 消息发送端点
        /// </summary>
        private IPEndPoint SendMsgIPEndPoint = null;

        /// <summary>
        /// 消息发送服务
        /// </summary>
        UdpClient SendMsgUdpClient = new UdpClient();

        /// <summary>
        /// 发送消息构造函数
        /// </summary>
        /// <param name="Target">发送目标用户信息</param>
        public UdpSendMessage(UserInfo Target)
        {
            UserTarget = Target;
            UserLocal = Tool.GetUserInfo();
            SendMsgIPEndPoint = new IPEndPoint(IPAddress.Parse(UserTarget.IP), 2425);
        }

        /// <summary>
        /// 使用UDP发送消息
        /// </summary>
        /// <param name="SendMsg"></param>
        public void SendMessage(string DecryptMsg)
        {
            string EncryptMsg = RSATool.Encryption(DecryptMsg, UserTarget.RSAPublic);
            string MsgInfo = string.Format(":MESG:{0}|{1}|{2}|{3}|{4}", UserLocal.MachineCode, UserLocal.UserName, UserLocal.ComputerName, UserLocal.IP, EncryptMsg);
            byte[] buff = Encoding.UTF8.GetBytes(MsgInfo);
            SendMsgUdpClient.Send(buff, buff.Length, SendMsgIPEndPoint);

            //插入已发送的通信信息到通信表
            DBSqlite.InsertChatTable(UserTarget.MachineCode, MsgType.Personal, UserLocal.IP, DecryptMsg, MsgStatus.HaveRead, ChatType.MESG);
        }

        /// <summary>
        /// 发送游戏请求
        /// </summary>
        public void SendGameMessage(GameType game_type, string DecryptMsg)
        {
            string EncryptMsg = RSATool.Encryption(DecryptMsg, UserTarget.RSAPublic);
            string MsgInfo = string.Format(":GAME:{0}|{1}|{2}|{3}", UserLocal.MachineCode, UserLocal.IP, (int)game_type, EncryptMsg);
            byte[] buff = Encoding.UTF8.GetBytes(MsgInfo);
            SendMsgUdpClient.Send(buff, buff.Length, SendMsgIPEndPoint);
            //插入已发送的通信信息到通信表
            DBSqlite.InsertChatTable(UserTarget.MachineCode, MsgType.Personal, UserLocal.IP, DecryptMsg, MsgStatus.HaveRead, ChatType.GAME);
        }
    }
}
