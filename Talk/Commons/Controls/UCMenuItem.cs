using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Talk.Entity;
using Talk.Commons.Tools;
using Talk.DB;

namespace Talk.Commons.Controls
{
    public partial class UCMenuItem : UserControl
    {
        public event EventHandler Selected;
        public UCMenuItem()
        {
            InitializeComponent();

            SetStyle(ControlStyles.Selectable, false);
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        #region ----------自定义属性----------

        private bool _ItemEnter = false;
        private Color _ItemBackColor = ColorTranslator.FromHtml("#E5E5E7");
        private Color _ItemSelectedBackColor = ColorTranslator.FromHtml("#D8D8D9");

        private UserInfo _ItemUserInfo;
        private string _ItemLatestMsg;
        private string _ItemLatestMsgTime;
        private bool _ItemSelected;

        /// <summary>
        /// 用户信息
        /// </summary>
        [Browsable(false)]
        public UserInfo ItemUserInfo
        {
            get
            {
                return _ItemUserInfo;
            }
            set
            {
                _ItemUserInfo = value;
                if (value != null)
                {
                    this.lblNickName.Text = value.UserName;
                    this.lblIPAddress.Text = value.IP;
                    this.picHead.ImageLocation = Tool.GetHeadPic(value.HeadPic);
                }
            }
        }

        /// <summary>
        /// 选项选中状态
        /// </summary>
        [Browsable(true), Description("选项选中状态")]
        [Category("Appearance")]
        public bool ItemSelected
        {
            get { return _ItemSelected; }
            set
            {
                _ItemSelected = value;
                Color BGColor = _ItemSelected ? _ItemSelectedBackColor : _ItemBackColor;
                this.BackColor = BGColor;
            }
        }
        /// <summary>
        /// 最新消息
        /// </summary>
        [Browsable(false), Description("最新消息")]
        public string ItemLatestMsg
        {
            get { return _ItemLatestMsg; }
            set
            {
                _ItemLatestMsg = value;
                int chatCount = DBSqlite.SelectChatCountByUnRead(_ItemUserInfo.MachineCode);
                string chatCountStr = chatCount > 0 ? "[{0}条] {1}" : "{1}";
                lblLatestMsg.Text = string.Format(chatCountStr, chatCount, value);
            }
        }

        /// <summary>
        /// 最新消息收取时间
        /// </summary>
        [Browsable(false), Description("最新消息收取时间")]
        public string ItemLatestMsgTime
        {
            get { return _ItemLatestMsgTime; }
            set
            {
                _ItemLatestMsgTime = value;
                //lblLatestMsgTime.Visible = !string.IsNullOrEmpty(value);
                DateTime date = Tool.GetDateTime(value);
                lblLatestMsgTime.Text = date.ToString("t");
            }
        }

        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color ItemBackColor
        {
            get { return _ItemBackColor; }
            set { _ItemBackColor = value; }
        }
        /// <summary>
        /// 选中背景颜色
        /// </summary>
        public Color ItemSelectedBackColor
        {
            get { return _ItemSelectedBackColor; }
            set { _ItemSelectedBackColor = value; }
        }

        #endregion

        #region ----------UI处理----------
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    Graphics g = e.Graphics;
        //    g.SetGDIHigh();

        //    //绘制背景颜色
        //    //Color BGColor = _ItemEnter ? _ItemSelectedBackColor : (_ItemSelected ? _ItemSelectedBackColor : _ItemBackColor);
        //    //this.BackColor = BGColor;

        //    //绘制下边线
        //    //using (Pen pen = new Pen(ColorTranslator.FromHtml("#EBEBEB"), 1))
        //    //{
        //    //    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
        //    //    g.DrawLine(pen, new Point(0, this.Height - 1), new Point(this.Width, this.Height - 1));
        //    //}
        //}

        /// <summary>
        /// 鼠标移入
        /// </summary>
        private void UCMenuItem_MouseEnter(object sender, EventArgs e)
        {
            //判断鼠标是否还在本控件的矩形区域内  
            if (!_ItemSelected && this.RectangleToScreen(this.ClientRectangle).Contains(Control.MousePosition))
            {
                _ItemEnter = true;
                this.BackColor = _ItemSelectedBackColor;
            }
        }
        /// <summary>
        /// 鼠标移出
        /// </summary>
        private void UCMenuItem_MouseLeave(object sender, EventArgs e)
        {
            if (!_ItemSelected)
            {
                _ItemEnter = false;
                this.BackColor = _ItemBackColor;
            }
        }
        #endregion

        private void Item_Click(object sender, EventArgs e)
        {
            if (this.Selected != null)
            {
                Selected(this, new EventArgs());
            }
        }
    }
}
