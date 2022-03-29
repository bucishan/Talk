using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Talk.Commons.UDP;
using Talk.Entity;
using Talk.Commons.Tools;

namespace Talk.Commons.Controls
{
    public partial class UCTabPagePersonalInfo : UserControl
    {

        protected HeadPicTools headPic = null;          //头像选择窗口
        protected string HeadPicName;                   //头像名称

        public UCTabPagePersonalInfo()
        {
            InitializeComponent();
            //注册窗口拖动事件 
            this.tableLayoutPanel1.MouseDown += Win32.Window_MouseDown;
            InitUserInfo();
        }

        /// <summary>
        /// 初始化界面个人信息
        /// </summary>
        private void InitUserInfo()
        {
            UserInfo info = Tool.GetUserInfo();
            this.txtUserName.Text = info.UserName;
            this.txtUserGroup.Text = info.UserGroup;
            this.HeadPicName = info.HeadPic;
            SetHeadPic(info.HeadPic);

        }

        private void HeadPic_Selected(string PicName)
        {
            HeadPicName = PicName;
            SetHeadPic(PicName);
        }

        protected void SetHeadPic(string PicName)
        {
            string PicPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "HeadPic", PicName);
            if (File.Exists(PicPath))
            {
                this.picHeadPic.ImageLocation = PicPath;
            }

        }

        /// <summary>
        /// 样式重绘
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //调整头像框位置居中
            Point picPoint = new Point((this.panel1.Width - this.picHeadPic.Width) / 2, this.picHeadPic.Location.Y);
            this.picHeadPic.Location = picPoint;
        }

        /// <summary>
        /// 验证保存
        /// </summary>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (this.txtUserName.Text == "")
            {
                //显示错误errorProvider
                errorProvider1.SetError(txtUserName, "请输入用户名");
            }
            else if (this.txtUserGroup.Text == "")
            {
                errorProvider1.SetError(txtUserGroup, "请输入项目组名");
            }
            else
            {
                errorProvider1.Dispose();

                UserInfo info = new UserInfo();
                info.HeadPic = HeadPicName;
                info.UserName = txtUserName.Text;
                info.UserGroup = txtUserGroup.Text;
                Tool.SetUserInfo(info);

                UdpBoardCast CUpdate = new UdpBoardCast();
                CUpdate.BoardCast();

                MessageBox.Show("保存成功");
            }
        }
        /// <summary>
        /// 取消保存
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            InitUserInfo();
        }


        /// <summary>
        /// 选择头像
        /// </summary>
        private void picHeadPic_Click(object sender, EventArgs e)
        {
            if (headPic == null)
            {
                headPic = new Talk.HeadPicTools();       //实例化头像选择窗口
                headPic.HeadPic_Selected += HeadPic_Selected;
            }
            if (headPic != null && headPic.Visible)
            {
                headPic.Hide();
                return;
            }
            headPic.StartPosition = FormStartPosition.Manual;
            headPic.FormBorderStyle = FormBorderStyle.None;
            int FormX = picHeadPic.Location.X - (headPic.Width / 2) + (picHeadPic.Width / 2);
            Point formPoint = new Point(FormX, picHeadPic.Location.Y + picHeadPic.Height + 40);
            headPic.Location = PointToScreen(formPoint);
            headPic.Show();
        }

    }
}
