using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Talk.Commons.UDP;
using Talk.Entity;
using Talk.Commons.Tools;
using Talk.DB;
using Gobangs;

namespace Talk.Commons.Controls
{
    public partial class UCChat : UserControl
    {
        /// <summary>
        /// 目标用户信息
        /// </summary>
        public UserInfo UserTarget;

        /// <summary>
        /// 个人用户信息
        /// </summary>
        private UserInfo UserLocal;

        /// <summary>
        /// 更多菜单
        /// </summary>
        private ToolStripDropDown MoreDropDown = new ToolStripDropDown();

        /// <summary>
        /// 按钮标记枚举
        /// </summary>
        private enum btnType
        {
            btnSetting,
            btnEmoj,
            btnFile,
            btnMore
        }

        public UCChat()
        {
            InitializeComponent();

            //初始个人信息
            UserLocal = Tool.GetUserInfo();

            //Size变更 控件重绘
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, true);

            //初始化按钮Tag
            btnSetting.Tag = btnType.btnSetting;
            btnEmoj.Tag = btnType.btnEmoj;
            btnFile.Tag = btnType.btnFile;
            btnMore.Tag = btnType.btnMore;

            //注册窗口拖动事件 
            this.p_Header.MouseDown += Win32.Window_MouseDown;

            //初始化更多下拉菜单
            Bitmap bmGame = IconFont.GetImage("\uf12e", 25, Color.Black);
            MoreDropDown.ImageScalingSize = new Size(30, 30);
            MoreDropDown.LayoutStyle = ToolStripLayoutStyle.Table;//设置布局
            MoreDropDown.Items.Add("五子棋", bmGame, btnMoreDropDown_Click);
            ((TableLayoutSettings)MoreDropDown.LayoutSettings).ColumnCount = 5;//设置每行显示数目
        }

        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = null;
            using (Pen pen = new Pen(ColorTranslator.FromHtml("#DFDFDF"), 1))
            {
                //绘制顶部边线
                g = p_Header.CreateGraphics();
                Tool.SetGDIHigh(g);
                Point h_pointX = new Point(p_Header.ClientRectangle.X, p_Header.ClientRectangle.Y + p_Header.ClientSize.Height - 2);
                Point h_pointY = new Point(p_Header.ClientRectangle.X + p_Header.ClientSize.Width, p_Header.ClientRectangle.Y + p_Header.ClientSize.Height - 2);
                g.DrawLine(pen, h_pointX, h_pointY);

                //绘制底部边线
                g = p_Footer.CreateGraphics();
                Tool.SetGDIHigh(g);
                Point f_pointX = p_Footer.ClientRectangle.Location;
                Point f_pointY = new Point(p_Footer.ClientRectangle.Width, p_Footer.ClientRectangle.Y);
                g.DrawLine(pen, f_pointX, f_pointY);
            }
        }

        //private Control _EnterCtrl = null;
        private bool _IsMouseEnter = false;
        private Color _btnSettingColor = ColorTranslator.FromHtml("#7F7F7F");
        private Color _btnSettingEnterColor = ColorTranslator.FromHtml("#1A1A1A");
        /// <summary>
        /// 绘制设置按钮
        /// </summary>
        private void Btn_Paint(object sender, PaintEventArgs e)
        {
            Control btnCtrl = (Control)sender;
            btnType _btnType = (btnType)btnCtrl.Tag;
            Graphics g = e.Graphics;
            g.SetGDIHigh();
            //绘制图标
            Color color = _IsMouseEnter ? _btnSettingEnterColor : _btnSettingColor;
            Rectangle iconRect = btnCtrl.ClientRectangle;
            iconRect.Inflate(-2, -2);
            int _Icon = 0xf141;

            switch (_btnType)
            {
                case btnType.btnSetting:
                    {
                        _Icon = 0xf141;
                        break;
                    }
                case btnType.btnEmoj:
                    {
                        _Icon = 0xf118;
                        break;
                    }
                case btnType.btnFile:
                    {
                        _Icon = 0xf114;
                        break;
                    }
                case btnType.btnMore:
                    {
                        _Icon = 0xf055;
                        break;
                    }
                default:
                    return;
            }
            Bitmap bm = IconFont.GetImage(IconFont.GetFont(_Icon), 20, color);
            g.DrawImage(bm, iconRect);
        }
        /// <summary>
        /// 刷新按钮移入
        /// </summary>
        private void Btn_MouseEnter(object sender, EventArgs e)
        {
            Control btnCtrl = (Control)sender;
            _IsMouseEnter = true;
            btnCtrl.Invalidate();
        }
        /// <summary>
        /// 刷新按钮移出
        /// </summary>
        private void Btn_MouseLeave(object sender, EventArgs e)
        {
            Control btnCtrl = (Control)sender;
            _IsMouseEnter = false;
            btnCtrl.Invalidate();
        }

        /// <summary>
        /// 初始化目标用户信息
        /// </summary>
        public void InitTargetUserInfo(UserInfo Target)
        {
            UserTarget = Target;
            lblUserName.Text = string.Format("{0} [{1}]", UserTarget.UserName, UserTarget.ComputerName);

            //初始化通信表
            DBSqlite.CreateChatTable(Target.MachineCode);

            DataTable dtChatHundred = DBSqlite.SelectChatInfoByHundred(Target.MachineCode);
            if (dtChatHundred != null && dtChatHundred.Rows.Count > 0)
            {
                foreach (DataRow row in dtChatHundred.Rows)
                {
                    int chat_type = Convert.ToInt32(row["chat_type"]);
                    int chat_status = Convert.ToInt32(row["chat_status"]);
                    int msg_type = Convert.ToInt32(row["msg_type"]);
                    string EncryptMsg = row["msg"].ToString();
                    string DecryptMsg = RSATool.Decryption(EncryptMsg);
                    DateTime msg_datetime = Tool.GetDateTime(row["msg_datetime"].ToString());

                    //输出通信消息
                    OutputMessage((ChatType)chat_type, (ChatStatus)chat_status, (MsgType)msg_type, DecryptMsg);

                    //switch ((MsgType)msg_type)
                    //{
                    //    case MsgType.Personal:
                    //        {
                    //            OutputMessage((ChatType)chat_type,MsgType.Personal, DecryptMsg);
                    //            break;
                    //        }
                    //    case MsgType.Other:
                    //        {
                    //            OutputMessage((ChatType)chat_typeMsgType.Other, DecryptMsg);
                    //            break;
                    //        }
                    //}

                }
                txtMsgOutput.ScrollToCaret();
            }
        }

        /// <summary>
        /// 输出消息
        /// </summary>
        public void OutputMessage(ChatType _ChatType, ChatStatus _ChatStatus, MsgType _MsgType, string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    AddChat(_ChatType, _ChatStatus, _MsgType, msg);
                }));
            }
            else
            {
                AddChat(_ChatType, _ChatStatus, _MsgType, msg);
            }
            txtMsgOutput.ScrollToCaret();
        }

        private void AddChat(ChatType _ChatType, ChatStatus _ChatStatus, MsgType _MsgType, string msg)
        {
            //System.Windows.Forms.Application.DoEvents();
            string _HeadPic = string.Empty;
            switch (_MsgType)
            {
                case MsgType.Personal:
                    _HeadPic = UserLocal.HeadPic;
                    break;
                case MsgType.Other:
                    _HeadPic = UserTarget.HeadPic;
                    break;
            }

            UCChatItem chatItem = new UCChatItem(_MsgType, _HeadPic, msg);
            chatItem._ChatType = _ChatType;
            chatItem._ChatStatus = _ChatStatus;
            chatItem.Margin = new Padding(0);
            chatItem.Width = txtMsgOutput.Width;
            if (_ChatType == ChatType.GAME && _ChatStatus == ChatStatus.Effective && _MsgType == MsgType.Other)
            {
                chatItem.Chat_Confirm += ChatItem_Chat_Confirm;
            }
            chatItem.ShowMsg();
            txtMsgOutput.Panel.Controls.Add(chatItem);
        }

        /// <summary>
        /// 控件Size变更时刷新聊天记录布局
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            foreach (UCChatItem item in txtMsgOutput.Panel.Controls)
            {
                item.Width = txtMsgOutput.Width;
                item.ResizeLayout();
            }
        }
        /// <summary>
        /// 发送消息方法
        /// </summary>
        private void SendMsg()
        {
            if (UserTarget == null) return;
            string msg = txtMsgInput.Text.Trim();

            if (string.IsNullOrEmpty(msg)) return;

            UdpSendMessage UdpSend = new UDP.UdpSendMessage(UserTarget);
            UdpSend.SendMessage(msg);

            txtMsgInput.Clear();
            OutputMessage(ChatType.MESG, ChatStatus.Invalid, MsgType.Personal, msg);
        }

        /// <summary>
        /// 清楚Enter自定义换行
        /// </summary>
        private void txtMsgInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (!e.Control && e.KeyCode == Keys.Enter)
            {
                txtMsgInput.Clear();
            }
        }
        /// <summary>
        /// 发送和换行事件
        /// </summary>
        private void txtMsgInput_KeyDown(object sender, KeyEventArgs e)
        {
            //消息发送
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Control)
                {
                    insert("");
                }
                else
                {
                    SendMsg();
                    txtMsgInput.Focus();
                    txtMsgInput.SelectAll();
                }
            }
        }

        /// <summary>
        /// 插入指定的字符串到RichTextBox控件的光标处
        /// </summary>
        /// <param name="inserted">被插入的字符串</param>
        private void insert(string inserted)
        {
            //if (txtMsgInput.SelectionLength > 0)
            //{
            //    //如果有选中的内容，则将光标移动到被选中内容之后
            //    txtMsgInput.SelectionStart = txtMsgInput.SelectionStart + txtMsgInput.SelectionLength;
            //}
            txtMsgInput.SelectedText = inserted;
        }

        /// <summary>
        /// 插入指定的前缀和后缀字符串到RichTextBox控件的光标处或者选中内容的两侧
        /// </summary>
        /// <param name="prefix">前缀字符串</param>
        /// <param name="suffix">后缀字符串</param>
        private void insert(string prefix, string suffix)
        {
            txtMsgInput.SelectedText = prefix + txtMsgInput.SelectedText + suffix;
        }

        /// <summary>
        /// 消息Confirm事件
        /// </summary>
        private void ChatItem_Chat_Confirm(object sender, bool ConfirmStatus)
        {
            UCChatItem chat = (UCChatItem)sender;
            DBSqlite.UpdateChatTypeToSpecify(UserTarget.MachineCode, ChatType.GAME, ChatStatus.Invalid);
            chat.HideConfirm();

            UdpSendMessage UdpSend = new UdpSendMessage(UserTarget);
            if (ConfirmStatus)
            {
                UdpSend.SendGameMessage(GameType.Agree, "Yes 现在开始！");
                OutputMessage(ChatType.GAME, ChatStatus.Invalid, MsgType.Personal, "Yes 现在开始！");
                Gobang _Gobang = new Gobangs.Gobang();
                _Gobang.OpenNetworkConnect(UserTarget.IP);
                _Gobang.Show();
            }
            else
            {
                UdpSend.SendGameMessage(GameType.DisAgree, "No 我不想玩！");
                OutputMessage(ChatType.GAME, ChatStatus.Invalid, MsgType.Personal, "No 我不想玩！");
            }
        }

        /// <summary>
        /// 表情
        /// </summary>
        private void btnEmoj_Click(object sender, EventArgs e)
        {
            //NotifyIconTool.IconFlashingStart();
        }

        /// <summary>
        /// 更多功能
        /// </summary>
        private void btnMore_Click(object sender, EventArgs e)
        {
            //获取当前按钮在屏幕上的位置           
            Point pt = new Point(0, -MoreDropDown.Height);
            MoreDropDown.Show(btnMore, pt);
        }

        /// <summary>
        /// 更多功能下拉菜单项点击事件
        /// </summary>
        private void btnMoreDropDown_Click(object sender, EventArgs e)
        {
            switch (sender.ToString())
            {
                case "五子棋":
                    {
                        if (__Global.IsGameIn)
                        {
                            MessageBox.Show("您正在进行游戏,无法同时进行多场游戏,请先退出游戏");
                            return;
                        }
                        __Global.Game_Gobang = new Gobangs.Gobang();
                        __Global.Game_Gobang.OpenNetworkServer();
                        __Global.Game_Gobang.SetGameStatus("服务已开启,正在等待对方回应！");
                        UdpSendMessage UdpSend = new UdpSendMessage(UserTarget);
                        UdpSend.SendGameMessage(GameType.Request, "Hi 玩五子棋嘛？");
                        OutputMessage(ChatType.GAME, ChatStatus.Invalid, MsgType.Personal, "Hi 玩五子棋嘛？");
                        __Global.Game_Gobang.Show();
                    }
                    break;
            }
        }


    }
}
