using Talk.Commons.Tools;
using Talk.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Talk.Commons.UDP
{
    public class UdpBoardCast
    {
        UdpClient bcUdpClient = new UdpClient();
        //绑定一个ip地址和端口号用来给别人传输信息
        IPEndPoint bcIPEndPoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 2425);

        public string localIP = string.Empty;

        //获取本机IP，如果是vista或windows7，取InterNetwork对应的地址


        //发送自己的信息到广播地址
        public void BoardCast()
        {
            //获取用户信息
            UserInfo info = Tool.GetUserInfo();
            //发送的内容
            string computerInfo = ":USER:" + info.MachineCode + ":" + info.UserName + ":" + info.ComputerName +
                ":" + info.IP + ":" + info.UserGroup + ":" + info.HeadPic + ":" + info.RSAPublic;
            //转化为字节流
            byte[] buff = Encoding.UTF8.GetBytes(computerInfo);
            //发送
            bcUdpClient.Send(buff, buff.Length, bcIPEndPoint);
        }

        //用户退出时，发送消息至广播地址
        public void UserQuit()
        {
            //获取用户信息
            UserInfo info = Tool.GetUserInfo();
            string quitInfo = ":QUIT:" + info.MachineCode;
            byte[] bufQuit = Encoding.UTF8.GetBytes(quitInfo);

            bcUdpClient.Send(bufQuit, bufQuit.Length, bcIPEndPoint);
        }

        //收到别人上线的通知时，回复对方，以便对方将自己加入在线用户列表，参数为自己的ip地址
        public void BCReply(string ipReply)
        {
            IPEndPoint EPReply = new IPEndPoint(IPAddress.Parse(ipReply), 2425);

            //获取用户信息
            UserInfo info = Tool.GetUserInfo();
            //发送的内容
            string computerInfo = ":REPY:" + info.MachineCode + ":" + info.UserName + ":" + info.ComputerName +
                ":" + info.IP + ":" + info.UserGroup + ":" + info.HeadPic + ":" + info.RSAPublic;

            byte[] buff = Encoding.UTF8.GetBytes(computerInfo);
            bcUdpClient.Send(buff, buff.Length, EPReply);
        }
    }
}
