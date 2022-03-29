using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Talk
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new Gobangs.Gobang());


            ////验证更新
            //if (CheckUpdate())
            //{
            //    return;
            //}

            Process instance = Commons.Tools.Tool.RunningInstance();
            if (instance == null) //没有实例在运行
            {
                //1.初始化唯一机器码
                string MachineCode = Commons.Tools.Tool.MachineCode;

                //2.初始化RSA加密密钥
                RSATool.InitRSAKeys();

                //3.初始化任务栏程序托管图标工具类
                NotifyIconTool.InitNotifyIconTool();

                //4.初始化音效播放工具类
                SoundTool.InitSoundTool();

                //5.初始化主窗口全局变量
                __Global.FormTalk = new MChat();

                //6.运行程序
                Application.Run(__Global.FormTalk);

                //7.监听应用程序退出事件
                Application.ApplicationExit += Application_ApplicationExit;

            }
            else //已经有一个实例在运行
            {
                //显示窗口
                Commons.Tools.Win32.ShowWindowAsync(instance.MainWindowHandle, 1);
                //窗口显示在最前端
                Commons.Tools.Win32.SetForegroundWindow(instance.MainWindowHandle);
            }

        }

        /// <summary>
        /// 应用程序退出时发送下线消息
        /// </summary>
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Commons.UDP.UdpBoardCast boardCast = new Commons.UDP.UdpBoardCast();
            boardCast.UserQuit();
        }

        /// <summary>
        /// 验证更新
        /// </summary>
        /// <returns></returns>
        public static bool CheckUpdate()
        {
            bool result = false;
            UpdateCheck.SoftUpdate app = new UpdateCheck.SoftUpdate(Application.ExecutablePath, "Talk");
            try
            {
                if (app.CheckNetwork())
                {
                    if (app.IsUpdate && MessageBox.Show("检查到新版本，是否更新？", "版本检查", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Application.Exit();
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        process.StartInfo.FileName = "Update.exe";
                        process.StartInfo.WorkingDirectory = Application.StartupPath;
                        process.StartInfo.CreateNoWindow = true;
                        process.Start();

                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
            }
            return result;
        }

    }
}
