using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace Talk.Commons.Tools
{
    /// <summary>
    /// 获取机器码
    /// </summary>
    public static partial class Tool
    {
        private static string _MachineCode { get; set; }

        /// <summary>
        /// 计算机唯一标识
        /// </summary>
        public static string MachineCode
        {
            get
            {
                if (string.IsNullOrEmpty(_MachineCode))
                {
                    _MachineCode = GetHash("UUID >> " + UUID() + "\r\nCPU >> " + CpuId() + "\r\nBIOS >> " +
                    BiosId() + "\r\nBASE >> " + BaseId() + "\r\nDisk >> " + DiskId() + "\r\nVideo >> " +
                    VideoId() + "\r\nMAC >> " + MacId());
                }
                return _MachineCode;
            }
        }

        /// <summary>
        /// 计算哈希值字符串
        /// </summary>
        public static string GetHash(string str)
        {
            byte[] buffer = Encoding.Default.GetBytes(str);

            if (buffer == null || buffer.Length < 1)
                return "";

            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();

            foreach (var b in hash)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
        /// <summary>
        /// 获取硬件标识符
        /// </summary>
        /// <param name="wmiClass"></param>
        /// <param name="wmiProperty"></param>
        /// <param name="wmiMustBeTrue"></param>
        /// <returns></returns>
        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc =
        new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 获取硬件标识符
        /// </summary>
        /// <param name="wmiClass"></param>
        /// <param name="wmiProperty"></param>
        /// <returns></returns>
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc =
        new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取UUID
        /// </summary>
        /// <returns></returns>
        public static string UUID()
        {
            string code = null;
            SelectQuery query = new SelectQuery("select * from Win32_ComputerSystemProduct");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (var item in searcher.Get())
                {
                    using (item)
                        code = item["UUID"].ToString();
                }
            }
            return code;
        }

        /// <summary>
        /// 获取CPUID
        /// </summary>
        /// <returns></returns>
        public static string CpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }
        /// <summary>
        /// 获取BIOSID
        /// </summary>
        /// <returns></returns>
        public static string BiosId()
        {
            return identifier("Win32_BIOS", "Manufacturer")
            + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
            + identifier("Win32_BIOS", "IdentificationCode")
            + identifier("Win32_BIOS", "SerialNumber")
            + identifier("Win32_BIOS", "ReleaseDate")
            + identifier("Win32_BIOS", "Version");
        }
        /// <summary>
        /// 获取硬盘ID
        /// </summary>
        /// <returns></returns>
        public static string DiskId()
        {
            return identifier("Win32_DiskDrive", "Model")
            + identifier("Win32_DiskDrive", "Manufacturer")
            + identifier("Win32_DiskDrive", "Signature")
            + identifier("Win32_DiskDrive", "TotalHeads");
        }
        /// <summary>
        /// 获取主板ID
        /// </summary>
        /// <returns></returns>
        public static string BaseId()
        {
            return identifier("Win32_BaseBoard", "Model")
            + identifier("Win32_BaseBoard", "Manufacturer")
            + identifier("Win32_BaseBoard", "Name")
            + identifier("Win32_BaseBoard", "SerialNumber");
        }
        /// <summary>
        /// 获取主视频控制器ID
        /// </summary>
        /// <returns></returns>
        public static string VideoId()
        {
            return identifier("Win32_VideoController", "DriverVersion")
            + identifier("Win32_VideoController", "Name");
        }
        /// <summary>
        /// 获取网卡ID
        /// </summary>
        /// <returns></returns>
        public static string MacId()
        {
            return identifier("Win32_NetworkAdapterConfiguration",
                "MACAddress", "IPEnabled");
        }
    }
}
