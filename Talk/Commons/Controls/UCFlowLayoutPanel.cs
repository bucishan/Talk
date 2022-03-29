using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Talk.Commons.Tools;

namespace Talk.Commons.Controls
{
    /// <summary>
    /// 自定义带滚动条容器
    /// 只支持垂直滚动条
    /// </summary>
    public partial class UCFlowLayoutPanel : UserControl
    {
        public UCFlowLayoutPanel()
        {
            InitializeComponent();

            this.cPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(Panel_OnMouseWheel);
            this.cPanel.MouseEnter += Control_MouseEnter;
            this.cPanel.MouseLeave += Control_MouseLeave;

            this.cScrollBar.MouseEnter += cScrollBar_MouseEnter;
            this.cScrollBar.MouseLeave += cScrollBar_MouseLeave;
            this.cScrollBar.MouseLeave += Control_MouseLeave;

            //SetStyle(
            //   ControlStyles.SupportsTransparentBackColor |
            //   ControlStyles.Opaque |
            //   ControlStyles.UserPaint |
            //   ControlStyles.AllPaintingInWmPaint |
            //   ControlStyles.OptimizedDoubleBuffer |
            //   ControlStyles.ResizeRedraw |
            //   ControlStyles.SupportsTransparentBackColor |
            //   ControlStyles.UserMouse |
            //   ControlStyles.Selectable |
            //   ControlStyles.StandardClick, true);
            SetStyle(ControlStyles.Selectable, true);
        }
        /// <summary>
        /// 重写CreateParams方法
        /// 解决控件过多加载闪烁问题
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;//用双缓冲绘制窗口的所有子控件
                return cp;
            }
        }
        private int VS_Maximum;
        private int VS_Minimum;
        private int VS_LargeChange;
        private int VS_SmallChange;
        private bool VS_Visible;
        private int VS_Value;

        /// <summary>
        /// 获取Panel的滚动条信息
        /// </summary>
        private void GetPanelScrollInfo()
        {
            VScrollProperties VScrollInfo = this.cPanel.VerticalScroll;
            VS_Maximum = VScrollInfo.Maximum;
            VS_Minimum = VScrollInfo.Minimum;
            VS_LargeChange = VScrollInfo.LargeChange;
            VS_SmallChange = VScrollInfo.SmallChange;
            VS_Visible = VScrollInfo.Visible;
            VS_Value = VScrollInfo.Value;
        }

        //私有字段
        private bool _ScrollBarMouseDown = false;           //记录鼠标是否按下
        private bool _ScrollBarMouseEnter = false;          //记录鼠标是否移入
        private int MousePonitY = 0;        //记录鼠标从按下到滑动的差值

        //共有属性
        private bool _ScrollBarHoverShow = true;      //设置是否鼠标移入才显示滚动条
        private Color _ScrollBarColor = ColorTranslator.FromHtml("#CBC8C6");    //滚动条颜色
        private Color _ScrollBarEnterColor = ColorTranslator.FromHtml("#B4B1B0");       //滚动条鼠标浮动颜色
        private int _ScrollBarWidth = 10;

        [Browsable(false), Description("当前控件容器")]
        public FlowLayoutPanel Panel
        {
            get
            {
                return this.cPanel;
            }
        }

        [Browsable(true), DefaultValue(false), Description("滚动条鼠标浮动显示")]
        [Category("Appearance")]
        public bool ScrollBarHoverShow
        {
            get
            {
                return _ScrollBarHoverShow;
            }

            set
            {
                _ScrollBarHoverShow = value;
            }
        }

        [Browsable(false), DefaultValue(typeof(Color), "Control"), Description("滚动条颜色")]
        [Category("Appearance")]
        public Color ScrollBarColor
        {
            get
            {
                return _ScrollBarColor;
            }

            set
            {
                _ScrollBarColor = value;
            }
        }

        [Browsable(false), DefaultValue(typeof(Color), "Control"), Description("滚动条鼠标浮动颜色")]
        [Category("Appearance")]
        public Color ScrollBarEnterColor
        {
            get
            {
                return _ScrollBarEnterColor;
            }
            set
            {
                _ScrollBarEnterColor = value;
            }
        }

        [Browsable(false), DefaultValue(10), Description("滚动条宽度")]
        [Category("Appearance")]
        public int ScrollBarWidth
        {
            get
            {
                return _ScrollBarWidth;
            }

            set
            {
                _ScrollBarWidth = value;
            }
        }

        /// <summary>
        /// 设置滚动条到最底部
        /// </summary>
        public void ScrollToCaret()
        {
            GetPanelScrollInfo();
            if (VS_Visible)
            {
                float unusdeRange = this.Height - cScrollBar.Height;
                int PointY = cScrollBar.Location.Y + (int)unusdeRange;
                //计算有效滑动区域
                PointY = PointY < 0 ? 0 : (PointY + cScrollBar.Height > this.Height) ? this.Height - cScrollBar.Height : PointY;

                //处理Panel滑动位置
                float fPerc = (float)PointY / unusdeRange;
                float moveSize = fPerc * (VS_Maximum - VS_LargeChange);
                cPanel.AutoScrollPosition = new Point(0, (int)moveSize);

                //处理自定义滚动条滑动位置
                this.cScrollBar.Location = new Point(Width - ScrollBarWidth, PointY);
            }
        }

        #region ----------滚动条相关事件----------

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            InitPaint();
        }

        public void InitPaint()
        {
            GetPanelScrollInfo();

            //调整控件位置
            this.cPanel.Width = VS_Visible ? this.Width + 20 : this.Width;
            cScrollBar.Visible = VS_Visible;

            //初始化自定义滚动条高度
            float BarHeight = (float)this.Height * ((float)VS_LargeChange / (float)VS_Maximum);
            this.cScrollBar.Width = ScrollBarWidth;
            this.cScrollBar.Height = Convert.ToInt32(BarHeight);
            this.cScrollBar.BringToFront();

            float unusdeRange = this.Height - cScrollBar.Height;
            float usedRange = (VS_Maximum - VS_Minimum) - VS_LargeChange;
            float fPerc = (float)VS_Value / usedRange;
            float moveSize = fPerc * unusdeRange;
            this.cScrollBar.Location = new Point(this.Width - ScrollBarWidth, (int)moveSize);

            Color _BarColor = _ScrollBarMouseEnter ? ScrollBarEnterColor : ScrollBarColor;
            this.cScrollBar.BackColor = _BarColor;

        }

        /// <summary>
        /// 滚动条绘制
        /// </summary>
        private void cScrollBar_Paint(object sender, PaintEventArgs e)
        {
            InitPaint();
        }

        private void cScrollBar_MouseUp(object sender, MouseEventArgs e)
        {
            _ScrollBarMouseDown = false;
            MousePonitY = 0;
        }

        private void cScrollBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _ScrollBarMouseDown = true;
                MousePonitY = e.Y;
            }
        }

        private void cScrollBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _ScrollBarMouseDown)
            {
                GetPanelScrollInfo();

                int PointY = cScrollBar.Location.Y + (e.Y - MousePonitY);
                //计算有效滑动区域
                PointY = PointY < 0 ? 0 : (PointY + cScrollBar.Height > this.Height) ? this.Height - cScrollBar.Height : PointY;

                //处理自定义滚动条滑动位置
                this.cScrollBar.Location = new Point(Width - ScrollBarWidth, PointY);

                //处理Panel滑动位置
                float unusdeRange = this.Height - cScrollBar.Height;
                float fPerc = (float)PointY / unusdeRange;
                float moveSize = fPerc * (VS_Maximum - VS_LargeChange);
                cPanel.AutoScrollPosition = new Point(0, (int)moveSize);
            }
        }

        private void cScrollBar_MouseEnter(object sender, EventArgs e)
        {
            _ScrollBarMouseEnter = true;
            this.cScrollBar.BackColor = ScrollBarEnterColor;

        }

        private void cScrollBar_MouseLeave(object sender, EventArgs e)
        {
            _ScrollBarMouseEnter = false;
            this.cScrollBar.BackColor = ScrollBarColor;
        }

        #endregion

        #region ----------Panel滚动事件----------

        private void cPanel_Scroll(object sender, ScrollEventArgs e)
        {
        }

        /// <summary>
        /// Panel滚轮事件
        /// </summary>
        private void Panel_OnMouseWheel(object sender, MouseEventArgs e)
        {
            this.cScrollBar.Focus();
            GetPanelScrollInfo();
            float unusdeRange = this.Height - cScrollBar.Height;
            float usedRange = (VS_Maximum - VS_Minimum) - VS_LargeChange;
            float fPerc = (float)VS_Value / usedRange;
            float moveSize = fPerc * unusdeRange;
            this.cScrollBar.Location = new Point(Width - ScrollBarWidth, (int)moveSize);
        }

        /// <summary>
        /// 用于滚动条鼠标脱离隐藏的鼠标事件 通用
        /// </summary>
        private void Control_MouseEnter(object sender, EventArgs e)
        {
            if (!ScrollBarHoverShow) return;

            if (!Tool.IsDesignMode() && VS_Visible)
            {
                this.cScrollBar.Visible = true;
            }
        }
        private void Control_MouseLeave(object sender, EventArgs e)
        {
            if (!ScrollBarHoverShow) return;

            if (!Tool.IsDesignMode())
            {
                Point p = Control.MousePosition;
                Rectangle rect = this.cPanel.ClientRectangle;
                rect.Inflate(-3, -3);
                if (!RectangleToScreen(rect).Contains(p))
                {
                    this.cScrollBar.Visible = false;
                }
            }
        }

        /// <summary>
        /// 控件添加或移除时 为控件注册鼠标事件
        /// </summary>
        private void cPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.MouseEnter += Control_MouseEnter;
            e.Control.MouseLeave += Control_MouseLeave;

            //调整控件位置
            GetPanelScrollInfo();
            this.cPanel.Width = VS_Visible ? this.Width + 20 : this.Width;
            cScrollBar.Visible = VS_Visible;
        }

        private void cPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            //调整控件位置
            GetPanelScrollInfo();
            this.cPanel.Width = VS_Visible ? this.Width + 20 : this.Width;
            cScrollBar.Visible = VS_Visible;
        }
        #endregion

    }
}
