using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using UpdateCheck;
using System.IO.Compression;

namespace Update
{
    public partial class Update : Form
    {
        /// <summary>
        /// 更新应用程序路径
        /// </summary>
        private string AppFilePath = string.Empty;

        /// <summary>
        /// 应用程序更新类
        /// </summary>
        SoftUpdate app;

        public Update()
        {
            InitializeComponent();

            this.Height -= 100;

            AppFilePath = Path.Combine(Application.StartupPath, "Talk.exe");
            if (!File.Exists(AppFilePath))
            {
                lblMessage.Text = "未检测到主程序!";
                return;
            }
            app = new SoftUpdate(AppFilePath, "Talk");
            //app = new SoftUpdate(Application.ExecutablePath, "Talk");

            //清理更新临时文件
            ClearUpdateTemp();

            //检查服务端是否有新版本程序
            CheckUpdate();
            TimerUpdate.Enabled = true;
        }

        /// <summary>
        /// 清理更新使用的临时文件
        /// </summary>
        private void ClearUpdateTemp()
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "Update.zip")))
            {
                try
                {
                    File.Delete(Path.Combine(Application.StartupPath, "Update.zip"));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }
            if (Directory.Exists(Path.Combine(Application.StartupPath, "UpdateTemp")))
            {
                try
                {
                    Directory.Delete(Path.Combine(Application.StartupPath, "UpdateTemp"), true);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }
        }

        /// <summary>
        /// 显示错误日志
        /// </summary>
        private void ErrorLogs(string log)
        {
            if (txtLogs.Visible == false)
            {
                this.Height += 100;
                txtLogs.Show();
            }
            txtLogs.AppendText(log);
        }

        public void CheckUpdate()
        {
            app.UpdateFinish += new UpdateState(App_UpdateFinish);
            try
            {
                if (app.IsUpdate)
                {
                    app.Update();
                }
                else
                {
                    lblMessage.Text = "未检测到新版本!";
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                ErrorLogs("检查更新错误:" + ex.Message);
                //MessageBox.Show(ex.Message);
            }
        }

        private void App_UpdateFinish()
        {
            //解压下载后的文件
            string path = app.FinalZipName;
            if (File.Exists(path))
            {
                //后改的 先解压滤波zip植入ini然后再重新压缩
                string dirEcgPath = Application.StartupPath + "\\" + "UpdateTemp";
                if (!Directory.Exists(dirEcgPath))
                {
                    Directory.CreateDirectory(dirEcgPath);
                }

                //开始解压压缩包
                lblMessage.Text = "正在解压更新包";
                ZipFile.ExtractToDirectory(path, dirEcgPath);

                try
                {
                    //复制新文件替换旧文件
                    DirectoryInfo TheFolder = new DirectoryInfo(dirEcgPath);
                    foreach (FileInfo NextFile in TheFolder.GetFiles())
                    {
                        File.Copy(NextFile.FullName, Path.Combine(Application.StartupPath, NextFile.Name), true);
                    }
                    Directory.Delete(dirEcgPath, true);
                    File.Delete(path);

                    lblMessage.Text = "更新完成,将在5秒后自动启动主程序";
                    TimerAutoExit.Enabled = true;
                }
                catch (Exception ex)
                {
                    ErrorLogs("更新错误:" + ex.Message);
                    //清理更新临时文件
                    ClearUpdateTemp();
                    //TimerAutoExit.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 计时器：更新进度
        /// </summary>
        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            int process = UpdateProcess.Process;
            lblUpdateProcess.Text = "下载文件进度:" + process + "%";
            this.BarUpdate.Value = process;
            if (process == 100)
            {
                TimerUpdate.Enabled = false;
            }
        }

        /// <summary>
        /// 计时器：程序自动关闭并启动主程序
        /// </summary>
        private void TimerAutoExit_Tick(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "Talk.exe";
                process.StartInfo.WorkingDirectory = Application.StartupPath;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
            }
            catch (Exception ex)
            {
                ErrorLogs("重启错误:" + ex.Message);
                Application.Exit();
            }
            Application.Exit();
        }
    }
}
