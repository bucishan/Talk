using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Talk.Commons.Controls
{
    public partial class UCTabBlock : UserControl
    {
        public UCTabBlock()
        {
            InitializeComponent();
        }

        private bool _IsMouseEnter = false;
        private int _TabIcon = 0xf2ba;
        private int _TabSelectedIcon = 0xf2b9;
        private Color _TabColor = ColorTranslator.FromHtml("#6D6D6D");
        private Color _TabSelectedColor = ColorTranslator.FromHtml("#07C160");
        private int _TabFontSize = 22;
        private bool _TabSelected = false;

        /// <summary>
        /// 选项卡图标文本
        /// </summary>
        [Browsable(true), DefaultValue(typeof(int), "0xf2c0"), Description("选项卡图标文本")]
        [Category("Appearance")]
        public int TabIcon
        {
            get { return _TabIcon; }
            set
            {
                _TabIcon = value;
            }
        }

        /// <summary>
        /// 选项卡选中图标文本
        /// </summary>
        [Browsable(true), Description("选项卡选中图标文本")]
        [Category("Appearance")]
        public int TabSelectedIcon
        {
            get
            {
                return _TabSelectedIcon;
            }
            set
            {
                _TabSelectedIcon = value;
            }
        }

        /// <summary>
        /// 选项卡图标颜色
        /// </summary>
        [Browsable(true), Description("选项卡图标颜色")]
        [Category("Appearance")]
        public Color TabColor
        {
            get
            {
                return _TabColor;
            }
            set
            {
                _TabColor = value;
            }
        }
        /// <summary>
        /// 选项卡选中图标颜色
        /// </summary>
        [Browsable(true), Description("选项卡选中图标颜色")]
        [Category("Appearance")]
        public Color TabSelectedColor
        {
            get
            {
                return _TabSelectedColor;
            }
            set
            {
                _TabSelectedColor = value;
            }
        }
        /// <summary>
        /// 选项卡图标字体大小
        /// </summary>
        [Browsable(true), Description("选项卡图标字体大小")]
        [Category("Appearance")]
        public int TabFontSize
        {
            get
            {
                return _TabFontSize;
            }

            set
            {
                _TabFontSize = value;
            }
        }

        /// <summary>
        /// 选项卡选中状态
        /// </summary>
        [Browsable(true), Description("选项卡选中状态")]
        [Category("Appearance")]
        public bool TabSelected
        {
            get
            {
                return _TabSelected;
            }

            set
            {
                _TabSelected = value;
                SetIcon();
            }
        }

        /// <summary>
        /// 设置图标
        /// </summary>
        private void SetIcon()
        {
            if (_TabFontSize <= 0 || _TabSelectedIcon <= 0 || _TabSelectedColor == null ||
                _TabIcon <= 0 || _TabColor == null)
            {
                return;
            }
            if (_TabSelected)
            {
                IconFont.SetIcon(this.icon, IconFont.GetFont(_TabSelectedIcon), _TabFontSize, _TabSelectedColor);
            }
            else
            {
                Color color = _IsMouseEnter ? _TabSelectedColor : _TabColor;
                IconFont.SetIcon(this.icon, IconFont.GetFont(_TabIcon), _TabFontSize, color);
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            SetIcon();
        }

        /// <summary>
        /// 鼠标移入
        /// </summary>
        private void UC_MouseEnter(object sender, EventArgs e)
        {
            _IsMouseEnter = true;
            if (!_TabSelected)
            {
                SetIcon();
            }
        }
        /// <summary>
        /// 鼠标移出
        /// </summary>
        private void UC_MouseLeave(object sender, EventArgs e)
        {
            _IsMouseEnter = false;
            if (!_TabSelected)
            {
                SetIcon();
            }
        }

        private void icon_Click(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        /// <summary>
        /// 触发Click事件
        /// </summary>
        public void PerformClick()
        {
            icon_Click(icon, new EventArgs());
        }
    }
}
