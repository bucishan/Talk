using System;
using System.Diagnostics;
using System.Drawing;
using Talk.Commons;
using Talk.Commons.Tools;

namespace Talk
{
    /// <summary>
    /// 任务栏托管图标工具类
    /// </summary>
    public static class NotifyIconTool
    {
        private static Icon _Icon_Chat = Properties.Resources.chat;
        private static Icon _Icon_Blank = Properties.Resources.blank;
        private static System.Windows.Forms.NotifyIcon _NotifyIcon;
        private static System.Timers.Timer _TimerFlashing;
        private static System.Timers.Timer _TimerFlashingCtrl;
        private static bool _IconFlashingStatus = false;
        private static bool _IconAlwaysFlashing = true;
        private static int _IconFlashingInterval = 500;
        private static int _IconFlashingNum = 5;

        private static System.Windows.Forms.ContextMenuStrip _Menu;

        /// <summary>
        /// 图标是否一直闪烁
        /// </summary>
        public static bool IconAlwaysFlashing
        {
            get { return _IconAlwaysFlashing; }
            set { _IconAlwaysFlashing = value; }
        }

        /// <summary>
        /// 图标闪烁事件间隔毫秒
        /// </summary>
        public static int IconFlashingInterval
        {
            get { return _IconFlashingInterval; }
            set { _IconFlashingInterval = value; }
        }

        /// <summary>
        /// 图标自动闪烁次数,IconAlwaysFlashing为false时有效
        /// </summary>
        public static int IconFlashingNum
        {
            get { return _IconFlashingNum; }
            set { _IconFlashingNum = value; }
        }

        public static void InitNotifyIconTool()
        {
            //初始化图标闪烁计时器
            _TimerFlashing = new System.Timers.Timer();
            _TimerFlashing.Interval = IconFlashingInterval;
            _TimerFlashing.Elapsed += _TimerFlashing_Elapsed;

            //初始化图标闪烁控制计时器
            _TimerFlashingCtrl = new System.Timers.Timer();
            _TimerFlashingCtrl.Interval = IconFlashingInterval * (IconFlashingNum * 2);
            _TimerFlashingCtrl.Elapsed += _TimerFlashingCtrl_Elapsed;

            //初始化托管程序右键菜单
            _Menu = new System.Windows.Forms.ContextMenuStrip();
            Bitmap IconExit = IconFont.GetImage("\uf08b", 20, Color.Red);
            _Menu.Items.Add("退出", IconExit, _NotifyIconMenu_Click);


            //初始化托管图标
            _NotifyIcon = new System.Windows.Forms.NotifyIcon();
            _NotifyIcon.Icon = _Icon_Chat;
            _NotifyIcon.Visible = true;
            _NotifyIcon.ContextMenuStrip = _Menu;
            _NotifyIcon.Click += _NotifyIcon_Click;

            //添加应用程序退出事件
            System.Windows.Forms.Application.ApplicationExit += Application_ApplicationExit;
        }

        /// <summary>
        /// 托管程序单击事件
        /// </summary>
        private static void _NotifyIcon_Click(object sender, EventArgs e)
        {
            IconFlashingStop();

            if (__Global.FormTalk != null)
            {
                __Global.FormTalk.Show();
            }
        }

        /// <summary>
        /// 右键菜单点击事件
        /// </summary>
        private static void _NotifyIconMenu_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        /// <summary>
        /// 图标闪烁计时器,用于控制图标闪烁间隔
        /// </summary>
        private static void _TimerFlashing_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_NotifyIcon == null) return;
            if (_IconFlashingStatus)
            {
                _NotifyIcon.Icon = _Icon_Chat;
            }
            else
            {
                _NotifyIcon.Icon = _Icon_Blank;
            }
            _IconFlashingStatus = !_IconFlashingStatus;
        }

        /// <summary>
        /// 图标闪烁控制计时器,用于控制自动停止图标闪烁
        /// </summary>
        private static void _TimerFlashingCtrl_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_NotifyIcon == null) return;
            _TimerFlashing.Stop();
            _TimerFlashingCtrl.Stop();

            _IconFlashingStatus = false;
            _NotifyIcon.Icon = _Icon_Chat;
        }
        /// <summary>
        /// 应用程序退出事件
        /// </summary>
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (_NotifyIcon == null) return;
            _NotifyIcon.Visible = false;
        }

        /// <summary>
        /// 开始图标闪烁
        /// </summary>
        public static void IconFlashingStart()
        {
            if (_NotifyIcon == null) return;
            _TimerFlashing.Start();
            if (!_IconAlwaysFlashing)
            {
                _TimerFlashingCtrl.Start();
            }
        }

        /// <summary>
        /// 结束图标闪烁
        /// </summary>
        public static void IconFlashingStop()
        {
            if (_NotifyIcon == null) return;
            _TimerFlashing.Stop();
            _TimerFlashingCtrl.Stop();
            _IconFlashingStatus = false;
            _NotifyIcon.Icon = _Icon_Chat;
        }
    }
}
