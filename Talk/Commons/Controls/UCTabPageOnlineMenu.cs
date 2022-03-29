using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Talk.Entity;
using Talk.DB;
using Talk.Commons.Tools;
using System.Drawing.Drawing2D;

namespace Talk.Commons.Controls
{
    public partial class UCTabPageOnlineMenu : UserControl
    {
        /// <summary>
        /// 菜单项选择委托
        /// </summary>
        /// <param name="user"></param>
        public delegate void ItemSelected(UserInfo user);
        /// <summary>
        /// 菜单项选择事件
        /// </summary>
        public event ItemSelected _ItemSelected;

        /// <summary>
        /// 当前选择的菜单项
        /// </summary>
        private UCMenuItem SelectedItem;

        public UCTabPageOnlineMenu()
        {
            InitializeComponent();

            //注册窗口拖动事件 
            this.tableLayoutPanel1.MouseDown += Win32.Window_MouseDown;

            //SetStyle(ControlStyles.ResizeRedraw, true);
            //SetStyle(ControlStyles.Selectable, true);
        }

        /// <summary>
        /// 添加一个用户项
        /// </summary>
        /// <param name="user">用户参数类</param>
        public void AddMenuItem(UserInfo user)
        {
            this.Invoke(new Action(() =>
            {
                if (p_OnlineMenu.Panel.Controls.Find("item_" + user.MachineCode, true).Count() > 0)
                {
                    p_OnlineMenu.Panel.Controls.RemoveByKey("item_" + user.MachineCode);
                }
                UCMenuItem item = new Controls.UCMenuItem();
                item.ItemUserInfo = user;
                item.Name = "item_" + user.MachineCode;
                item.Margin = new Padding(0);
                item.Width = p_OnlineMenu.Width;
                item.Selected += Item_Selected;

                //读取消息
                if (DBSqlite.CheckTableExist(user.MachineCode))
                {
                    Dictionary<string, object> chatLatestMsg = DBSqlite.SelectChatCountByLatestMsg(user.MachineCode);
                    if (chatLatestMsg.Count > 0)
                    {
                        string DecryptMsg = RSATool.Decryption(chatLatestMsg["msg"].ToString());
                        item.ItemLatestMsg = DecryptMsg;
                        item.ItemLatestMsgTime = chatLatestMsg["msg_datetime"].ToString();
                    }
                }
                p_OnlineMenu.Panel.Controls.Add(item);
                if (user.IP == Tool.GetLocalIP())
                {
                    p_OnlineMenu.Panel.Controls.SetChildIndex(item, 0);
                }
            }));
        }

        /// <summary>
        /// 移除一个用户项
        /// </summary>
        /// <param name="user"></param>
        public void RemoveMenuItem(UserInfo user)
        {
            this.Invoke(new Action(() =>
            {
                if (p_OnlineMenu.Panel.Controls.Find("item_" + user.MachineCode, true).Count() > 0)
                {
                    p_OnlineMenu.Panel.Controls.RemoveByKey("item_" + user.MachineCode);
                }
            }));
        }

        public UCMenuItem GetMenuItem(UserInfo user)
        {
            UCMenuItem item = null;
            this.Invoke(new Action(() =>
            {
                if (p_OnlineMenu.Panel.Controls.Find("item_" + user.MachineCode, true).Count() > 0)
                {
                    item = (UCMenuItem)p_OnlineMenu.Panel.Controls.Find("item_" + user.MachineCode, true)[0];
                }
            }));
            return item;
        }

        /// <summary>
        /// 在线用户菜单Item选择事件
        /// </summary>
        private void Item_Selected(object sender, EventArgs e)
        {
            UCMenuItem item = (UCMenuItem)sender;
            if (SelectedItem == item) return;

            if (SelectedItem != null)
            {
                SelectedItem.ItemSelected = false;
            }
            item.ItemSelected = true;
            SelectedItem = item;

            if (DBSqlite.CheckTableExist(item.ItemUserInfo.MachineCode))
            {
                //更新未读消息为已读
                DBSqlite.UpdateChatToHaveRead(item.ItemUserInfo.MachineCode);

                //更新在线用户菜单项消息为最新消息
                Dictionary<string, object> dic = DBSqlite.SelectChatCountByLatestMsg(item.ItemUserInfo.MachineCode);
                if (dic.Count > 0)
                {
                    string DecryptMsg = RSATool.Decryption(dic["msg"].ToString());
                    item.ItemLatestMsg = DecryptMsg;
                }
            }
            //委托处理事件
            if (_ItemSelected != null)
            {
                _ItemSelected(item.ItemUserInfo);
            }
        }

        /// <summary>
        /// 重绘时刷新菜单项的宽度
        /// </summary>
        private void p_OnlineMenu_Paint(object sender, PaintEventArgs e)
        {
            //调整在线列表项宽度
            foreach (Control ctrl in p_OnlineMenu.Panel.Controls)
            {
                ctrl.Width = p_OnlineMenu.Width;
            }

            //调整搜索框和刷新按钮位置
            txtSearch.Width = p_OnlineMenu.Width - 54;
        }

        #region ----------刷新按钮事件----------

        private bool _IsMouseEnter = false;
        private int _btnRefreshIcon = 0xf021;
        private Color _btnRefreshColor = ColorTranslator.FromHtml("#585858");
        private Color _btnRefreshEnterColor = ColorTranslator.FromHtml("#1A1A1A");
        private Color _btnRefreshBGColor = ColorTranslator.FromHtml("#D3D3D3");
        private Color _btnRefreshEnterBGColor = ColorTranslator.FromHtml("#D1D1D1");

        /// <summary>
        /// 刷新按钮移入
        /// </summary>
        private void btnRefresh_MouseEnter(object sender, EventArgs e)
        {
            _IsMouseEnter = true;
            btnRefresh.Invalidate();
        }
        /// <summary>
        /// 刷新按钮移出
        /// </summary>
        private void btnRefresh_MouseLeave(object sender, EventArgs e)
        {
            _IsMouseEnter = false;
            btnRefresh.Invalidate();
        }
        /// <summary>
        /// 绘制控件样式
        /// </summary>
        private void btnRefresh_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SetGDIHigh();

            //绘制边框圆角矩形的线和背景
            Color BGColor = _IsMouseEnter ? _btnRefreshEnterBGColor : _btnRefreshBGColor;
            RectangleF range = new RectangleF(0, 0, btnRefresh.Width - 1, btnRefresh.Height - 1);
            GraphicsPath gp = Tool.DrawRoundRect(range, 3);
            using (Pen pen = new Pen(BGColor, 1))
            {
                g.DrawPath(pen, gp);
            }
            using (Brush brush = new SolidBrush(BGColor))
            {
                g.FillPath(brush, gp);
            }

            //绘制图标
            Color color = _IsMouseEnter ? _btnRefreshEnterColor : _btnRefreshColor;
            Bitmap bm = IconFont.GetImage(IconFont.GetFont(_btnRefreshIcon), 16, color);
            Rectangle iconRect = btnRefresh.ClientRectangle;
            iconRect.X -= 1;
            iconRect.Inflate(-5, -5);
            g.DrawImage(bm, iconRect);
        }
        #endregion

    }
}
