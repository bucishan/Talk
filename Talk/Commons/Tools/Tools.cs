using Talk.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace Talk.Commons.Tools
{
    public static partial class Tool
    {
        /// <summary>
        /// 在进程中查找是否已经有实例在运行
        /// </summary>
        /// <returns></returns>
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //遍历与当前进程名称相同的进程列表
            foreach (Process process in processes)
            {
                //如果实例已经存在则忽略当前进程
                if (process.Id != current.Id)
                {
                    //保证要打开的进程同已经存在的进程来自同一文件路径
                    if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", @"\") == current.MainModule.FileName)
                    {
                        //返回已经存在的进程
                        return process;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 封装弹出提示框
        /// 方便后期维护成自定义弹出框
        /// </summary>
        /// <param name="msg">消息</param>
        public static void Message(string msg)
        {
            MessageBox.Show(msg);
        }
        /// <summary>
        /// 设置GDI高质量模式抗锯齿
        /// </summary>
        /// <param name="g">The g.</param>
        public static void SetGDIHigh(this Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }

        /// <summary>
        /// 获取本机IP，如果是vista或windows7，取InterNetwork对应的地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            string localIP = string.Empty;
            try
            {
                foreach (IPAddress _ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (_ipAddress.AddressFamily.ToString() == "InterNetwork")
                    {
                        localIP = _ipAddress.ToString();
                        break;
                    }
                    else
                    {
                        localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return localIP;
        }

        /// <summary>
        /// 获取UserInfo
        /// </summary>
        /// <returns></returns>
        public static UserInfo GetUserInfo()
        {
            UserInfo info = new Entity.UserInfo();

            if (!File.Exists(@"user.config"))
            {
                info.IP = GetLocalIP();
                info.HeadPic = "001.png";
                info.MachineCode = Tool.MachineCode;
                info.UserName = System.Environment.UserName;
                info.UserGroup = System.Environment.UserDomainName;
                info.ComputerName = System.Environment.UserName;
                info.RSAPublic = RSATool.GetPublicKeyStr();
                SetUserInfo(info);
            }
            else
            {
                //读取存储个人信息文件内容
                FileStream fsRead = new FileStream(@"user.config", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fsRead);
                string userInfo = sr.ReadLine();
                string[] txtInfo = userInfo.Split(':');
                sr.Close();
                fsRead.Close();

                info.IP = GetLocalIP();
                info.HeadPic = txtInfo[0];
                info.UserName = txtInfo[1];
                info.UserGroup = txtInfo[2];
                info.MachineCode = Tool.MachineCode;
                info.ComputerName = System.Environment.UserName;
                info.RSAPublic = RSATool.GetPublicKeyStr();
            }
            return info;
        }

        /// <summary>
        /// 设置UserInfo
        /// </summary>
        /// <param name="info">实体类对象</param>
        public static void SetUserInfo(UserInfo info)
        {
            //获取登录计算机的用户名                              与当前用户关联的网络域名
            //System.Environment.UserName + ":" + System.Environment.UserDomainName;

            FileStream fsWrite = new FileStream(@"user.config", FileMode.Create, FileAccess.Write);
            string date = string.Format("{0}:{1}:{2}", info.HeadPic, info.UserName, info.UserGroup);
            StreamWriter sw = new StreamWriter(fsWrite);
            sw.Write(date);
            sw.Close();
            fsWrite.Close();
        }

        public static string GetHeadPic(string headPicName)
        {
            string PicPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "HeadPic", headPicName);
            return PicPath;
            //if (File.Exists(PicPath))
            //{
            //    this.picHead.ImageLocation = PicPath;
            //}
        }

        /// <summary>
        /// 带毫秒的字符转换成时间（DateTime）格式
        /// 可处理格式：[2014-10-10 10:10:10,666 或 2014-10-10 10:10:10 666 或 2014-10-10 10:10:10.666]
        /// </summary>
        public static DateTime GetDateTime(string dateTime)
        {
            string[] strArr = dateTime.Split(new char[] { '-', ' ', ':', ',', '.' });
            DateTime dt = new DateTime(int.Parse(strArr[0]),
                int.Parse(strArr[1]),
                int.Parse(strArr[2]),
                int.Parse(strArr[3]),
                int.Parse(strArr[4]),
                int.Parse(strArr[5]),
                int.Parse(strArr[6]));
            return dt;
        }

        /// <summary>
        /// 获取长日期时间带毫秒格式
        /// </summary>
        /// <returns></returns>
        public static string GetLongDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff");
        }

        /// <summary>
        /// 获取圆角边线路径
        /// </summary>
        /// <param name="rect">矩形区域</param>
        /// <param name="cRadius">圆角角度</param>
        public static GraphicsPath DrawRoundRect(RectangleF rect, int cRadius)
        {
            GraphicsPath myPath = new GraphicsPath();
            myPath.StartFigure();
            myPath.AddArc(new RectangleF(new PointF(rect.X, rect.Y), new Size(2 * cRadius, 2 * cRadius)), 180, 90);
            myPath.AddLine(new PointF(rect.X + cRadius, rect.Y), new PointF(rect.Right - cRadius, rect.Y));
            myPath.AddArc(new RectangleF(new PointF(rect.Right - 2 * cRadius, rect.Y), new Size(2 * cRadius, 2 * cRadius)), 270, 90);
            myPath.AddLine(new PointF(rect.Right, rect.Y + cRadius), new PointF(rect.Right, rect.Bottom - cRadius));
            myPath.AddArc(new RectangleF(new PointF(rect.Right - 2 * cRadius, rect.Bottom - 2 * cRadius), new Size(2 * cRadius, 2 * cRadius)), 0, 90);
            myPath.AddLine(new PointF(rect.Right - cRadius, rect.Bottom), new PointF(rect.X + cRadius, rect.Bottom));
            myPath.AddArc(new RectangleF(new PointF(rect.X, rect.Bottom - 2 * cRadius), new Size(2 * cRadius, 2 * cRadius)), 90, 90);
            myPath.AddLine(new PointF(rect.X, rect.Bottom - cRadius), new PointF(rect.X, rect.Y + cRadius));
            myPath.CloseFigure();
            return myPath;
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="rect">矩形区域</param>
        /// <returns></returns>
        public static GraphicsPath DrawTriangleRect(RectangleF rect, bool isLeft = false)
        {
            GraphicsPath myPath = new GraphicsPath();
            myPath.StartFigure();
            if (isLeft)
            {
                myPath.AddLine(new PointF(rect.Right, rect.Y), new PointF(rect.X, rect.Bottom - (rect.Height / 2)));
                myPath.AddLine(new PointF(rect.X, rect.Bottom - (rect.Height / 2)), new PointF(rect.Right, rect.Bottom));
                myPath.AddLine(new PointF(rect.Right, rect.Bottom), new PointF(rect.Right, rect.Y));
            }
            else
            {
                myPath.AddLine(new PointF(rect.X, rect.Y), new PointF(rect.Right, rect.Bottom - (rect.Height / 2)));
                myPath.AddLine(new PointF(rect.Right, rect.Bottom - (rect.Height / 2)), new PointF(rect.X, rect.Bottom));
                myPath.AddLine(new PointF(rect.X, rect.Bottom), new PointF(rect.X, rect.Y));
            }
            myPath.CloseFigure();
            return myPath;
        }

        /// <summary>
        /// 获取logo图像
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap GetLogo(int width, int height)
        {
            return ResizeImage(Properties.Resources.logo, 100, 100);
        }
        /// <summary>
        /// Resize图片
        /// </summary>
        /// <param name="bmp">原始Bitmap</param>
        /// <param name="newW">新的宽度</param>
        /// <param name="newH">新的高度</param>
        /// <returns>处理以后的Bitmap</returns>
        public static Bitmap ResizeImage(Bitmap bmp, int newW, int newH)
        {
            try
            {
                Bitmap b = new Bitmap(newW, newH);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.SetGDIHigh();
                    g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                }
                return b;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 判断是否处于IDE设计模式
        /// </summary>
        /// <returns></returns>
        public static bool IsDesignMode()
        {
            bool returnFlag = false;
#if DEBUG
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                returnFlag = true;
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
            {
                returnFlag = true;
            }
#endif
            return returnFlag;
        }
    }
}
