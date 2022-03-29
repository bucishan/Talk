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
    public partial class UCTextBox : UserControl
    {
        public UCTextBox()
        {
            InitializeComponent();
            SetStyle(
               ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.SupportsTransparentBackColor |
               ControlStyles.UserMouse |
               ControlStyles.Selectable |
               ControlStyles.StandardClick, true);
        }

        private Rectangle _BtnClearRect;
        private Color _BoxBackColor = ColorTranslator.FromHtml("#FFFFFF");
        private Color _BoxFocusBackColor = ColorTranslator.FromHtml("#DAD9D7");
        private Color _BoxBorderColor = ColorTranslator.FromHtml("#DAD9D7");
        private Color _BoxIconColor = ColorTranslator.FromHtml("#666666");
        private bool _BoxFocus = false;
        private bool _BoxShowSearchIcon = false;

        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color BoxBackColor
        {
            get
            {
                return _BoxBackColor;
            }
            set
            {
                _BoxBackColor = value;
            }
        }

        /// <summary>
        /// 文本框获取焦点背景颜色
        /// </summary>
        public Color BoxFocusBackColor
        {
            get
            {
                return _BoxFocusBackColor;
            }
            set
            {
                _BoxFocusBackColor = value;
            }
        }
        /// <summary>
        /// 图标颜色
        /// </summary>
        public Color BoxIconColor
        {
            get
            {
                return _BoxIconColor;
            }

            set
            {
                _BoxIconColor = value;
            }
        }
        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color BoxBorderColor
        {
            get
            {
                return _BoxBorderColor;
            }

            set
            {
                _BoxBorderColor = value;
            }
        }

        /// <summary>
        /// 是否编辑
        /// </summary>
        public bool BoxFocus
        {
            get
            {
                return _BoxFocus;
            }
            set
            {
                _BoxFocus = value;
            }
        }

        /// <summary>
        /// 是否显示搜索图标
        /// </summary>
        public bool BoxShowSearchIcon
        {
            get
            {
                return _BoxShowSearchIcon;
            }

            set
            {
                _BoxShowSearchIcon = value;
                Invalidate();
            }
        }

        public override string Text
        {
            get
            {
                return this.txtInput.Text;
            }
            set
            {
                this.txtInput.Text = value;
            }
        }

        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_BoxFocus && _BtnClearRect != null && _BtnClearRect.Contains(new Point(e.X, e.Y)))
                {
                    //txtInput.
                    txtInput.Clear();
                    _BoxFocus = false;
                    Invalidate();
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Tool.SetGDIHigh(g);

            //调整输入框控件位置 和宽度
            int pointX = _BoxShowSearchIcon ? this.ClientRectangle.X + this.ClientSize.Height : this.ClientRectangle.X + 5;
            Point ctrlPoint = new Point(pointX, (this.ClientSize.Height + 1 - txtInput.Height) / 2);
            txtInput.Location = ctrlPoint;
            txtInput.Width = _BoxShowSearchIcon ? this.ClientSize.Width - (this.ClientSize.Height * 2) : this.ClientSize.Width - this.ClientSize.Height - 5;

            //设置文本框背景颜色不支持透明
            Color BGColor = _BoxFocus ? _BoxFocusBackColor : _BoxBackColor;
            this.txtInput.BackColor = BGColor;
            //this.iconClear.Visible = _BoxFocus;

            //绘制边框圆角矩形的线
            RectangleF rect = new RectangleF(0, 0, this.Width - 1, this.Height - 1);
            GraphicsPath gp = Tool.DrawRoundRect(rect, 3);
            using (Pen pen = new Pen(_BoxBorderColor, 1))
            {
                g.DrawPath(pen, gp);
            }
            //填充选中背景
            using (Brush brush = new SolidBrush(BGColor))
            {
                g.FillPath(brush, gp);
            }

            //初始化搜索图标
            if (_BoxShowSearchIcon)
            {
                Bitmap bt_iconSearch = IconFont.GetImage("\uf002", 30, _BoxIconColor);
                Rectangle iconSearchRect = new Rectangle(0, 0, this.ClientSize.Height, this.ClientSize.Height);
                iconSearchRect.Inflate(-6, -6);
                g.DrawImage(bt_iconSearch, iconSearchRect);
            }

            //绘制清空按钮
            if (_BoxFocus)
            {
                int X = this.ClientSize.Width - this.ClientSize.Height;
                int Y = this.ClientRectangle.Y;
                int width = this.ClientSize.Height;
                int height = this.ClientSize.Height;
                _BtnClearRect = new Rectangle(X, Y, width, height);
                _BtnClearRect.Inflate(-6, -6);

                Bitmap bt_iconClear = IconFont.GetImage("\uf00d", 30, _BoxIconColor);

                using (Brush brush = new SolidBrush(_BoxBorderColor))
                {
                    g.FillEllipse(brush, _BtnClearRect);
                }
                Rectangle iconRect = _BtnClearRect;
                iconRect.Inflate(-2, -2);
                g.DrawImage(bt_iconClear, iconRect);
            }

        }

        private void txtInput_Enter(object sender, EventArgs e)
        {
            _BoxFocus = true;
            Invalidate();
        }

        private void txtInput_Leave(object sender, EventArgs e)
        {
            _BoxFocus = false;
            Invalidate();
        }
    }
}
