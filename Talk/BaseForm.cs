using Talk.Commons;
using Talk.Commons.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Talk
{
    public partial class BaseForm : Form
    {
        #region 窗体阴影特效
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ClassStyle |= 0x00020000;
                return createParams;
            }
        }
        #endregion
        public BaseForm()
        {
            this.Load += BaseForm_Load;
            InitializeComponent();
            this.Icon = Properties.Resources.chat;

        }
        /// <summary>
        /// 窗体加载事件 设置窗体圆角
        /// </summary>
        private void BaseForm_Load(object sender, EventArgs e)
        {
            SetFormRoundRectRgn(this, 3);	//设置圆角
        }


        #region ----------自定义属性----------


        public enum PositionEnum
        {
            Left,
            Right
        }

        private PositionEnum _BtnPosition = PositionEnum.Left;
        /// <summary>
        /// 红绿灯按钮显示位置
        /// </summary>
        /// <value>The color of the focus.</value>
        [Browsable(true), Description("红绿灯按钮显示位置")]
        [Category("Appearance")]
        public PositionEnum BtnPosition
        {
            get { return _BtnPosition; }
            set
            {
                if (value != _BtnPosition)
                {
                    _BtnPosition = value;
                    SetBtnPosition(value);
                }
            }
        }

        /// <summary>
        /// 设置红绿灯按钮位置
        /// </summary>
        private void SetBtnPosition(PositionEnum position)
        {
            int btnWidth = btnClose.Width;
            int clientWidth = this.ClientRectangle.Width;
            Point InitLeftPoint = new Point(10, 10);
            Point InitRightPoint = new Point(clientWidth - btnWidth - 10, 10);
            switch (position)
            {
                case PositionEnum.Left:
                    {
                        btnClose.Location = InitLeftPoint;
                        InitLeftPoint.X += (btnWidth + 8);
                        btnMin.Location = InitLeftPoint;
                        InitLeftPoint.X += (btnWidth + 8);
                        btnRestore.Location = InitLeftPoint;
                        break;
                    }
                case PositionEnum.Right:
                    {
                        btnClose.Location = InitRightPoint;
                        InitRightPoint.X -= (btnWidth + 8);
                        btnMin.Location = InitRightPoint;
                        InitRightPoint.X -= (btnWidth + 8);
                        btnRestore.Location = InitRightPoint;
                        break;
                    }
            }
        }

        /// <summary>
        /// 置顶红绿灯按钮
        /// </summary>
        public void SetBtnBringToFront()
        {
            btnClose.BringToFront();
            btnMin.BringToFront();
            btnRestore.BringToFront();
        }
        #endregion

        #region ----------调用系统函数改变界面----------
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            SetBtnBringToFront();
        }

        protected override void OnResize(System.EventArgs e)
        {
            SetBtnPosition(this.BtnPosition);   //实时刷新按钮位置
        }

        /// <summary>
        /// 设置窗体的圆角矩形
        /// </summary>
        /// <param name="form">需要设置的窗体</param>
        /// <param name="rgnRadius">圆角矩形的半径</param>
        public static void SetFormRoundRectRgn(Form form, int rgnRadius)
        {
            int hRgn = Win32.CreateRoundRectRgn(0, 0, form.Width + 1, form.Height + 1, rgnRadius, rgnRadius);
            Win32.SetWindowRgn(form.Handle, hRgn, false);
            Win32.DeleteObject(hRgn);
        }
        /// <summary>
        /// 窗体拖动事件
        /// </summary>
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            Win32.ReleaseCapture();
            Win32.SendMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MOVE + Win32.HTCAPTION, 0);
        }

        #endregion

        #region ----------按钮事件----------

        /// <summary>
        /// 红绿灯按钮功能事件
        /// </summary>
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Tag.ToString())
            {
                case "close":
                    {
                        this.Close();
                        break;
                    }
                case "restore":
                    {
                        if (this.WindowState == FormWindowState.Maximized)
                        {
                            this.WindowState = FormWindowState.Normal;
                        }
                        else
                        {
                            this.WindowState = FormWindowState.Maximized;
                        }
                        SetFormRoundRectRgn(this, 3);   //设置圆角

                        break;
                    }
                case "min":
                    {
                        if (this.WindowState != FormWindowState.Minimized)
                        {
                            this.WindowState = FormWindowState.Minimized;
                        }
                        break;
                    }
            }
        }
        #endregion

    }
}
