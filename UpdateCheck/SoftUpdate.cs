using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Xml;

namespace UpdateCheck
{
    /// <summary>  
    /// 更新完成触发的事件  
    /// </summary>  
    public delegate void UpdateState();
    /// <summary>  
    /// 程序更新  
    /// </summary> 
    public class SoftUpdate
    {
        /// <summary>
        /// 更新包URL
        /// </summary>
        private string packageUrl;

        /// <summary>
        /// 更新配置文件URL
        /// </summary>
        private const string updateUrl = "http://shanli.ren/update.xml";//升级配置的XML文件地址  

        #region 构造函数
        public SoftUpdate() { }

        /// <summary>  
        /// 程序更新  
        /// </summary>  
        /// <param name="file">要更新的文件</param>  
        public SoftUpdate(string file, string softName)
        {
            this.LoadFile = file;
            this.SoftName = softName;
        }
        #endregion

        #region 属性
        private string loadFile;
        private string newVerson;
        private string updateDescription;
        private string softName;
        private bool isUpdate;

        /// <summary>  
        /// 或取是否需要更新  
        /// </summary>  
        public bool IsUpdate
        {
            get
            {
                checkUpdate();
                return isUpdate;
            }
        }

        /// <summary>  
        /// 要检查更新的文件  
        /// </summary>  
        public string LoadFile
        {
            get { return loadFile; }
            set { loadFile = value; }
        }

        /// <summary>  
        /// 程序集新版本  
        /// </summary>  
        public string NewVerson
        {
            get { return newVerson; }
        }

        /// <summary>  
        /// 更新描述 
        /// </summary>  
        public string UpdateDescription
        {
            get { return updateDescription; }
        }
        /// <summary>  
        /// 升级的名称  
        /// </summary>  
        public string SoftName
        {
            get { return softName; }
            set { softName = value; }
        }

        private string _finalZipName = string.Empty;

        public string FinalZipName
        {
            get { return _finalZipName; }
            set { _finalZipName = value; }
        }

        #endregion

        /// <summary>  
        /// 更新完成时触发的事件  
        /// </summary>  
        public event UpdateState UpdateFinish;
        private void isFinish()
        {
            if (UpdateFinish != null)
            {
                UpdateFinish();
            }
        }

        /// <summary>  
        /// 下载更新  
        /// </summary>  
        public void Update()
        {
            try
            {
                if (!isUpdate)
                    return;
                WebClient wc = new WebClient();
                string filename = Path.Combine(Path.GetDirectoryName(loadFile),Path.GetFileName(packageUrl));
                //string exten = packageUrl.Substring(packageUrl.LastIndexOf("."));
                //if (loadFile.IndexOf(@"\") == -1)
                //    filename = Path.GetFileNameWithoutExtension(loadFile) + exten;
                //else
                //    filename = Path.GetDirectoryName(loadFile) + "\\" + Path.GetFileNameWithoutExtension(loadFile) + exten;

                FinalZipName = filename;
                //wc.DownloadFile(download, filename);
                wc.DownloadFileAsync(new Uri(packageUrl), filename);
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                wc.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(wc_DownloadFileCompleted);
                //wc.Dispose();

            }
            catch
            {
                throw new Exception("更新出现错误，网络连接失败！");
            }
        }

        /// <summary>
        /// 事件：下载完成
        /// </summary>
        void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            (sender as WebClient).Dispose();
            isFinish();
        }

        /// <summary>
        /// 事件：下载进度
        /// </summary>
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(e.ProgressPercentage);
            UpdateProcess.Process = e.ProgressPercentage;
            //Thread.Sleep(100);
        }
        /// <summary>
        /// 验证是否可以连接外网
        /// </summary>
        /// <returns></returns>
        public bool CheckNetwork()
        {
            bool connectStatus = false;
            Ping ping = new Ping();
            PingReply reply = null;
            try
            {
                reply = ping.Send("shanli.ren", 1000);
            }
            catch (Exception ex)
            {
                connectStatus = false;
            }
            finally
            {
                if (reply == null || (reply != null && reply.Status != IPStatus.Success))
                {
                    connectStatus = false;
                }
                else if (reply.Status == IPStatus.Success)
                {
                    connectStatus = true;
                }
            }
            return connectStatus;
        }
        /// <summary>  
        /// 检查是否需要更新  
        /// </summary>  
        public void checkUpdate()
        {
            try
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(updateUrl);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);
                XmlNode list = xmlDoc.SelectSingleNode("Update");
                foreach (XmlNode node in list)
                {
                    if (node.Name == "Soft" && node.Attributes["Name"].Value.ToLower() == SoftName.ToLower())
                    {
                        foreach (XmlNode xml in node)
                        {
                            //最新版本
                            if (xml.Name == "Verson")
                                newVerson = xml.InnerText;
                            //更新包地址
                            if (xml.Name == "Package")
                                packageUrl = xml.InnerText;
                            //更新包描述
                            if (xml.Name == "Description")
                                updateDescription = xml.InnerText;
                        }
                    }
                }
                //查询版本导致进程占用
                Version ver = new Version(newVerson);
                Version verson;//= Assembly.LoadFrom(loadFile).GetName().Version;

                //通过此方法读取可以解决dll被占用问题
                byte[] fileData = File.ReadAllBytes(loadFile);
                Assembly asm = Assembly.Load(fileData);
                verson = asm.GetName().Version;

                int tm = verson.CompareTo(ver);

                if (tm >= 0)
                    isUpdate = false;
                else
                    isUpdate = true;
            }
            catch (Exception ex)
            {
                throw new Exception("更新出现错误，请确认网络连接无误后重试！");
            }
        }

        /// <summary>  
        /// 获取要更新的文件  
        /// </summary>  
        /// <returns></returns>  
        public override string ToString()
        {
            return this.loadFile;
        }
    }
}
