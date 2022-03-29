using Talk.Commons.Tools;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Talk
{
    /// <summary>
    /// RSA非对称性加密工具类
    /// </summary>
    public static class RSATool
    {

        #region ----------获取Rsa字符串或对象----------
        /// <summary>
        /// 获取加密所使用的key，RSA算法是一种非对称密码算法，所谓非对称，就是指该算法需要一对密钥，使用其中一个加密，则需要用另一个才能解密。
        /// </summary>
        public static void InitRSAKeys()
        {
            if (File.Exists("PublicKey.xml") && File.Exists("PrivateKey.xml"))
            {
                return;
            }
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
            string PublicKey = rsa.ToXmlString(false); // 获取公匙，用于加密
            string PrivateKey = rsa.ToXmlString(true); // 获取公匙和私匙，用于解密

            // 私匙中含有公匙，公匙是根据私匙进行计算得来的。
            using (StreamWriter streamWriter = new StreamWriter("PublicKey.xml"))
            {
                streamWriter.Write(rsa.ToXmlString(false));// 将公匙保存到运行目录下的PublicKey
            }
            using (StreamWriter streamWriter = new StreamWriter("PrivateKey.xml"))
            {
                streamWriter.Write(rsa.ToXmlString(true)); // 将公匙&私匙保存到运行目录下的PrivateKey
            }
        }

        /// <summary>
        /// 获取公钥字符串
        /// </summary>
        /// <returns></returns>
        public static string GetPublicKeyStr()
        {
            if (Tool.IsDesignMode()) return "";
            string publicKeyStr = string.Empty;
            string keyPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "PublicKey.xml");
            using (StreamReader streamReader = new StreamReader(keyPath)) // 读取运行目录下的PublicKey.xml
            {
                publicKeyStr = streamReader.ReadToEnd(); // 将公匙载入进RSA实例中
            }
            return publicKeyStr;
        }

        /// <summary>
        /// 获取公钥RSACryptoServiceProvider对象
        /// </summary>
        /// <returns></returns>
        public static RSACryptoServiceProvider GetPublicKeyObj()
        {
            string publicKeyStr = GetPublicKeyStr();
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKeyStr);
                return rsa;
            }
        }

        /// <summary>
        /// 获取私钥字符串
        /// </summary>
        /// <returns></returns>
        public static string GetPrivateKeyStr()
        {
            if (Tool.IsDesignMode()) return "";
            string privateKeyStr = string.Empty;
            string keyPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "PrivateKey.xml");
            using (StreamReader streamReader = new StreamReader(keyPath))
            {
                privateKeyStr = streamReader.ReadToEnd();
            }
            return privateKeyStr;
        }
        /// <summary>
        /// 获取私钥RSACryptoServiceProvider对象
        /// </summary>
        /// <returns></returns>
        public static RSACryptoServiceProvider GetPrivateKeyObj()
        {
            string privateKeyStr = GetPrivateKeyStr();
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKeyStr);
                return rsa;
            }
        }

        #endregion

        #region ----------使用RSA加密----------

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str">需要加密的明文</param>
        /// <returns></returns>
        public static string Encryption(string DecryptStr)
        {
            string PublicKey = GetPublicKeyStr();
            return Encryption(DecryptStr, PublicKey);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="DecryptStr">需要加密的明文</param>
        /// <param name="PublicKey">RSA公钥字符串</param>
        /// <returns></returns>
        public static string Encryption(string DecryptStr, string PublicKey)
        {
            if (string.IsNullOrEmpty(DecryptStr))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(PublicKey))
            {
                throw new ArgumentException("Invalid Public Key");
            }

            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                var inputBytes = Encoding.UTF8.GetBytes(DecryptStr);//有含义的字符串转化为字节流
                rsaProvider.FromXmlString(PublicKey);//载入公钥
                int bufferSize = (rsaProvider.KeySize / 8) - 11;//单块最大长度
                var buffer = new byte[bufferSize];
                using (MemoryStream inputStream = new MemoryStream(inputBytes),
                     outputStream = new MemoryStream())
                {
                    while (true)
                    { //分段加密
                        int readSize = inputStream.Read(buffer, 0, bufferSize);
                        if (readSize <= 0)
                        {
                            break;
                        }

                        var temp = new byte[readSize];
                        Array.Copy(buffer, 0, temp, 0, readSize);
                        var encryptedBytes = rsaProvider.Encrypt(temp, false);
                        outputStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                    }
                    return Convert.ToBase64String(outputStream.ToArray());//转化为字节流方便传输
                }
            }
        }

        #endregion

        #region ----------使用RSA解密----------

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="EncryptStr">需要解密的加密字符串</param>
        /// <returns></returns>
        public static string Decryption(string EncryptStr)
        {
            string PrivateKey = GetPrivateKeyStr();
            return Decryption(EncryptStr, PrivateKey);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="EncryptStr">需要解密的加密字符串</param>
        /// <param name="PrivateKey">RSA私钥字符串</param>
        /// <returns></returns>
        public static string Decryption(string EncryptStr, string PrivateKey)
        {
            if (string.IsNullOrEmpty(EncryptStr))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(PrivateKey))
            {
                throw new ArgumentException("Invalid Private Key");
            }

            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                var inputBytes = Convert.FromBase64String(EncryptStr);
                rsaProvider.FromXmlString(PrivateKey);
                int bufferSize = rsaProvider.KeySize / 8;
                var buffer = new byte[bufferSize];
                using (MemoryStream inputStream = new MemoryStream(inputBytes),
                     outputStream = new MemoryStream())
                {
                    while (true)
                    {
                        int readSize = inputStream.Read(buffer, 0, bufferSize);
                        if (readSize <= 0)
                        {
                            break;
                        }

                        var temp = new byte[readSize];
                        Array.Copy(buffer, 0, temp, 0, readSize);
                        var rawBytes = rsaProvider.Decrypt(temp, false);
                        outputStream.Write(rawBytes, 0, rawBytes.Length);
                    }
                    return Encoding.UTF8.GetString(outputStream.ToArray());
                }
            }
        }
        #endregion

    }
}
