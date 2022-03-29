using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Talk.Commons.Tools;

namespace Talk.Commons.Controls
{
    public partial class UCTabPageMusic : UserControl
    {
        public UCTabPageMusic()
        {
            InitializeComponent();
            //注册窗口拖动事件 
            this.tableLayoutPanel1.MouseDown += Win32.Window_MouseDown;
        }
    }
}
