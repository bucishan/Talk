using Talk.Commons.Controls;
using Talk.Commons.Tools;
using Talk.Commons.UDP;
using Talk.Entity;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Talk
{
    public partial class MChat : BaseForm
    {

        public MChat()
        {
            CheckForIllegalCrossThreadCalls = false;//在多线程程序中，新创建的线程不能访问UI线程创建的窗口控件，如果需要访问窗口中的控件，可以在窗口构造函数中将CheckForIllegalCrossThreadCalls设置为 false
            InitializeComponent();
            SetBtnBringToFront();

            //注册窗口拖动事件 
            this.p_ChatContainer.MouseDown += Win32.Window_MouseDown;
        }

        private void Chat_Load(object sender, EventArgs e)
        {

            //初始化选项卡标签
            InitTabBlock();

            //监听消息（广播和聊天）将lvFriend, lbUserCount作为参数传递，起到一个同步ListView和label的作用
            UdpMsgListen startUdpThread = new UdpMsgListen(this.TabPageOnlineMenu, this.p_ChatContainer);
            Thread tStartUdpThread = new Thread(new ThreadStart(startUdpThread.StartUdpThread));
            //设置为后台线程
            tStartUdpThread.IsBackground = true;
            tStartUdpThread.Start();

            //第一次登录时发送广播消息，查看在线用户
            UdpBoardCast boardCast = new UdpBoardCast();
            boardCast.BoardCast();
        }

        /// <summary>
        /// 初始化Tab标签
        /// </summary>
        private void InitTabBlock()
        {
            //初始化选项卡布局
            InitTabBlockLayout();

            //初始化选项卡标记
            btnOnlineMenu.Tag = "online";
            btnSelf.Tag = "self";
            btnMusic.Tag = "music";
            btnOnlineMenu.PerformClick();
            TabPageOnlineMenu._ItemSelected += TabPageOnlineMenu_ItemSelected;
        }

        /// <summary>
        /// 选项卡标签布局
        /// </summary>
        private void InitTabBlockLayout()
        {
            if (p_TabBlock.Controls.Count <= 0) return;

            int ClientWidth = p_TabBlock.ClientSize.Width;
            int ClientHeight = p_TabBlock.ClientSize.Height;
            int ChildCount = p_TabBlock.Controls.Count;
            int ChildWidth = ClientWidth / ChildCount;
            int count = 0;
            foreach (Control ctrl in p_TabBlock.Controls)
            {
                ctrl.Margin = new Padding(10);
                ctrl.Width = ChildWidth;
                ctrl.Height = ClientHeight;
                ctrl.Location = new Point(count * ChildWidth, 0);
                count++;
            }
        }
        /// <summary>
        /// 在线用户单项选择事件
        /// </summary>
        /// <param name="user">选择的用户信息</param>
        private void TabPageOnlineMenu_ItemSelected(UserInfo user)
        {
            UCChat chat = new Commons.Controls.UCChat();
            chat.Name = "chat_" + user.MachineCode;
            chat.Dock = DockStyle.Fill;
            p_ChatContainer.Controls.Clear();
            p_ChatContainer.Controls.Add(chat);
            chat.InitTargetUserInfo(user);
        }

        /// <summary>
        /// 选项卡切换事件
        /// </summary>
        private void btnTabBlock_Click(object sender, EventArgs e)
        {
            UCTabBlock btnTab = (UCTabBlock)sender;
            string btnTag = btnTab.Tag.ToString();
            foreach (UCTabBlock ctrl in p_TabBlock.Controls)
            {
                ctrl.TabSelected = (ctrl == btnTab);
            }
            switch (btnTag)
            {
                case "online":
                    {
                        TabPageOnlineMenu.Dock = DockStyle.Fill;
                        TabPageOnlineMenu.BringToFront();
                        break;
                    }
                case "self":
                    {
                        TabPagePersonalInfo.Dock = DockStyle.Fill;
                        TabPagePersonalInfo.BringToFront();
                        break;
                    }
                case "music":
                    {
                        TabPageMusic.Dock = DockStyle.Fill;
                        TabPageMusic.BringToFront();
                        break;
                    }
            }
        }
        /// <summary>
        /// 窗口绘制
        /// </summary>
        private void MChat_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g_TabBlock = p_TabBlock.CreateGraphics();
            //Graphics g_Tab = p_TabPage.CreateGraphics();

            //using (Pen pen = new Pen(ColorTranslator.FromHtml("#EBEBEB"), 1))
            //{
            //    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            //    g_TabBlock.DrawLine(pen, new Point(0, 1), new Point(p_TabBlock.Width, 1));
            //    //g_Tab.DrawLine(pen, new Point(p_TabBlock.Width-1 , 0), new Point(p_TabBlock.Width-1 , this.ClientSize.Height));
            //}
        }


        /// <summary>
        /// 关闭窗体前发送退出信息
        /// </summary>
        private void MChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        /// <summary>
        /// 绘制软件Logo
        /// </summary>
        private void p_ChatContainer_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SetGDIHigh();
            g.Clear(p_ChatContainer.BackColor);

            int X = p_ChatContainer.ClientSize.Width / 2 - 50;
            int Y = p_ChatContainer.ClientSize.Height / 2 - 50;

            Rectangle rect = new Rectangle(X, Y, X + 100, Y + 100);
            Bitmap bm = Tool.GetLogo(100, 100);

            g.DrawImage(bm, new Point(X, Y));
        }

        private void p_ChatContainer_Resize(object sender, EventArgs e)
        {
            p_ChatContainer.Invalidate();
        }
    }
}
