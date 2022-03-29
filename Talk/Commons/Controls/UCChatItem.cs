using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Talk.Commons.Tools;
using System.Drawing.Drawing2D;

namespace Talk.Commons.Controls
{
    public partial class UCChatItem : UserControl
    {
        //Confirm 的事件和委托
        public delegate void Confirm(object sender, bool ConfirmStatus);
        public event Confirm Chat_Confirm;
        /// <summary>
        /// 消息类别 【自己还是其他】
        /// </summary>
        private MsgType _ChatMsgType;
        private string _HeadPic;
        private string _Msg;
        /// <summary>
        /// 消息类型【纯消息还是游戏】
        /// </summary>
        public ChatType _ChatType = ChatType.MESG;
        /// <summary>
        /// 游戏请求消息状态
        /// </summary>
        public ChatStatus _ChatStatus = ChatStatus.Invalid;


        public UCChatItem()
        {
            InitializeComponent();
            SetStyle(ControlStyles.Selectable, false);
            this.txtMsg.MouseWheel += TxtMsg_MouseWheel;
        }

        public UCChatItem(MsgType msgType, string headPic, string msg)
        {
            _ChatMsgType = msgType;
            _HeadPic = headPic;
            _Msg = msg;

            InitializeComponent();
            SetStyle(ControlStyles.Selectable, false);
            this.txtMsg.MouseWheel += TxtMsg_MouseWheel;
        }



        private int GetTxtWidth(string Text)
        {
            string[] Texts = { Text };
            int MaxWidth = 0;

            if (Text.Contains("\n"))
            {
                Texts = Text.Split('\n');
            }

            using (Graphics graphics = CreateGraphics())
            {
                foreach (var item in Texts)
                {
                    SizeF sizeF = graphics.MeasureString(Text, new Font("宋体", 10));
                    if (sizeF.Width > MaxWidth)
                    {
                        MaxWidth = (int)sizeF.Width;
                    }
                }
            }
            return MaxWidth;
        }

        public void ShowMsg()
        {
            ResizeLayout();
            this.txtMsg.Text = _Msg;
            this.picHead.ImageLocation = Tool.GetHeadPic(_HeadPic);
        }

        /// <summary>
        /// 调整大小和布局
        /// </summary>
        public void ResizeLayout()
        {
            if (string.IsNullOrEmpty(_Msg)) return;

            int _PointMarginX = 20;
            int _PointMarginY = 10;
            int _CtrlSpace = 10;
            int _MsgMinWidth = 50;
            int _MsgMaxWidth = this.Width / 2;
            int _MsgRealWidth = GetTxtWidth(_Msg);
            int _MsgWidth = _MsgRealWidth > _MsgMaxWidth ? _MsgMaxWidth : _MsgRealWidth < _MsgMinWidth ? _MsgMinWidth : _MsgRealWidth;

            this.txtMsg.Width = _MsgWidth;

            switch (_ChatMsgType)
            {
                case MsgType.Personal:
                    {
                        this.picHead.Location = new Point(this.Width - picHead.Width - _PointMarginX, _PointMarginY);
                        this.txtMsg.Location = new Point(picHead.Location.X - _MsgWidth - _CtrlSpace - 5, _PointMarginY + 5);
                    }
                    break;
                case MsgType.Other:
                    {
                        this.picHead.Location = new Point(_PointMarginX, _PointMarginY);
                        this.txtMsg.Location = new Point(picHead.Location.X + picHead.Width + _CtrlSpace + 5, _PointMarginY + 5);
                    }
                    break;
                default:
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SetGDIHigh();

            //背景颜色
            Color BGColor = _ChatMsgType == MsgType.Personal ? ColorTranslator.FromHtml("#98E165") : Color.White;
            //边线颜色
            //Color BDColor = _ChatMsgType == MsgType.Other ? ColorTranslator.FromHtml("#E5E5E5") : ColorTranslator.FromHtml("#98E165");

            txtMsg.BackColor = BGColor;
            //绘制边框圆角矩形的线
            Rectangle rect = new Rectangle(txtMsg.Location, txtMsg.ClientSize);

            //判断消息是否为游戏请求 并且非自己发送
            if (_ChatType == ChatType.GAME && _ChatMsgType == MsgType.Other)
            {
                if (_ChatStatus == ChatStatus.Effective)
                {
                    IconFont.SetIcon(btnAgree, "\uf00c", 12, Color.Red);
                    IconFont.SetIcon(btnDisAgree, "\uf00d", 12, Color.Green);
                    panelConfirm.Visible = true;
                    panelConfirm.Location = new Point(rect.X, rect.Bottom);
                    rect.Size = new Size(rect.Width, rect.Height + panelConfirm.Height);
                }
            }

            rect.Inflate(5, 5);
            GraphicsPath gp = Tool.DrawRoundRect(rect, 3);

            //生成绘制箭头曲线
            RectangleF rectTriangle = new RectangleF();
            bool isLeft = _ChatMsgType == MsgType.Other;
            float TriangleX = _ChatMsgType == MsgType.Personal ? rect.Right : rect.X - 5;
            rectTriangle.Location = new PointF(TriangleX, rect.Y + 12.5f);
            rectTriangle.Size = new SizeF(5, 10);

            GraphicsPath gpTriangle = Tool.DrawTriangleRect(rectTriangle, isLeft);
            gp.AddPath(gpTriangle, false);

            //填充选中背景
            using (Brush brush = new SolidBrush(BGColor))
            {
                g.FillPath(brush, gp);
            }

            //绘制边线
            //using (Pen pen = new Pen(BDColor, 1))
            //{
            //    g.DrawPath(pen, gp);
            //}
        }

        /// <summary>
        /// Confirm点击事件
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Control btn = (Control)sender;
            string btnTag = btn.Tag.ToString();
            bool ConfirmStatus = btnTag == "yes";
            if (Chat_Confirm != null)
            {
                Chat_Confirm(this, ConfirmStatus);
            }
        }

        /// <summary>
        /// 隐藏确认框
        /// </summary>
        public void HideConfirm()
        {
            _ChatStatus = ChatStatus.Invalid;
            this.panelConfirm.Visible = false;
        }
        /// <summary>
        /// 滚动事件
        /// </summary>
        private void TxtMsg_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs h = e as HandledMouseEventArgs;
            if (h != null)
            {
                h.Handled = true;
            }
        }

        /// <summary>
        /// 消息框内容大小变更时自动调整高度
        /// </summary>
        private void txtMsg_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            txtMsg.Height = e.NewRectangle.Height;
            int ConfirmHeight = 0;
            if (_ChatType == ChatType.GAME && _ChatStatus == ChatStatus.Effective && _ChatMsgType == MsgType.Other)
            {
                ConfirmHeight = panelConfirm.Height;
            }
            this.Height = txtMsg.Height + 30 + ConfirmHeight;
        }
    }
}
