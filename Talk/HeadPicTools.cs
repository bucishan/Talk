using Talk.Commons.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Talk
{
    public partial class HeadPicTools : Form
    {
        public delegate void HeadPicSelected(string PicName);
        public event HeadPicSelected HeadPic_Selected;

        private string PicName;

        public HeadPicTools()
        {
            System.Windows.Forms.Application.DoEvents();
            InitializeComponent();
            InitHeadPic();
        }

        void InitHeadPic()
        {
            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, "HeadPic");
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            ImageList imglist = new ImageList();
            List<string> lstName = new List<string>();
            imglist.ImageSize = new Size(64, 64);
            imglist.ColorDepth = ColorDepth.Depth32Bit;

            foreach (FileInfo fileInfo in dirInfo.GetFiles())
            {
                System.Windows.Forms.Application.DoEvents();
                imglist.Images.Add(Image.FromFile(fileInfo.FullName));
                lstName.Add(fileInfo.Name);
            }

            for (int i = 0; i < imglist.Images.Count; i++)
            {
                System.Windows.Forms.Application.DoEvents();
                PictureBox pic = new PictureBox();
                pic.Width = 40;
                pic.Height = 40;
                pic.Cursor = Cursors.Hand;
                pic.BorderStyle = BorderStyle.Fixed3D;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.Image = imglist.Images[i];
                pic.Tag = lstName[i];
                pic.Click += PicHead_Click;
                flowLayoutPanel1.Controls.Add(pic);

            }
        }
        PictureBox SelectPic = null;

        /// <summary>
        /// 头像选择事件
        /// </summary>
        private void PicHead_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            if (SelectPic != null)
            {
                SelectPic.BorderStyle = BorderStyle.Fixed3D;
            }
            SelectPic = pic;
            pic.BorderStyle = BorderStyle.None;
            PicName = pic.Tag != null ? pic.Tag.ToString() : "";

            btnSave.Enabled = true;
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            this.Hide();
        }

        /// <summary>
        /// 确定事件
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SelectPic == null || string.IsNullOrEmpty(PicName))
            {
                return;
            }
            if (HeadPic_Selected != null)
            {
                HeadPic_Selected(PicName);
            }
            btnSave.Enabled = false;
            this.Hide();
        }
        /// <summary>
        /// 窗体不是活动窗体时关闭
        /// </summary>
        private void HeadPicTools_Deactivate(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            this.Hide();
        }
    }
}
