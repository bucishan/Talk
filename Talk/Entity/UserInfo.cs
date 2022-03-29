using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Talk.Entity
{
    public class UserInfo
    {
        private string _HeadPic;
        private string _UserName;
        private string _IP;
        private string _ComputerName;
        private string _UserGroup;
        private string _MachineCode;
        private string _RSAPublic;

        /// <summary>
        /// 头像字符串调用标记
        /// </summary>
        public string HeadPic
        {
            get
            {
                return _HeadPic;
            }
            set
            {
                _HeadPic = value;
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return _UserName;
            }

            set
            {
                _UserName = value;
            }
        }
        /// <summary>
        /// 用户IP
        /// </summary>
        public string IP
        {
            get
            {
                return _IP;
            }

            set
            {
                _IP = value;
            }
        }
        /// <summary>
        /// 用户计算机名称
        /// </summary>
        public string ComputerName
        {
            get
            {
                return _ComputerName;
            }

            set
            {
                _ComputerName = value;
            }
        }
        /// <summary>
        /// 用户组
        /// </summary>
        public string UserGroup
        {
            get
            {
                return _UserGroup;
            }

            set
            {
                _UserGroup = value;
            }
        }
        /// <summary>
        /// 机器码唯一标识
        /// </summary>
        public string MachineCode
        {
            get
            {
                return _MachineCode;
            }

            set
            {
                _MachineCode = value;
            }
        }

        /// <summary>
        /// RSA公钥加密对象
        /// </summary>
        public string RSAPublic
        {
            get
            {
                return _RSAPublic;
            }
            set
            {
                _RSAPublic = value;
            }
        }
    }
}
