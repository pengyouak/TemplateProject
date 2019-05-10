 using System;
using System.IO;
using System.Security.Cryptography;  
using System.Text;
namespace WindCommon.Plugins.Encryptor
{
    /// <summary>
    /// DES加密/解密类。
    /// </summary>
    public class DESEncrypt
    {
        public DESEncrypt()
        {
        }

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public string EncryptDES(string encryptString, string encryptKey, string iv="12345678")
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Encoding.UTF8.GetBytes(iv);
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public string DecryptDES(string decryptString, string decryptKey, string iv = "12345678")
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = Encoding.UTF8.GetBytes(iv);
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        /// <summary> 
        /// 完整 
        /// </summary> 
        public class TripleDESEncrypt
        {
            public static string Encrypt(string aStrString, string aStrKey, CipherMode mode = CipherMode.ECB, string iv = "12345678")
            {
                try
                {
                    var des = new TripleDESCryptoServiceProvider
                    {
                        Key = Encoding.UTF8.GetBytes(aStrKey.Substring(0, 24)),//长度必须24个字节 
                        Mode = mode,
                        Padding = PaddingMode.PKCS7
                    };
                    if (mode == CipherMode.CBC)
                    {
                        des.IV = Encoding.UTF8.GetBytes(iv);
                    }
                    var desEncrypt = des.CreateEncryptor();
                    byte[] buffer = Encoding.UTF8.GetBytes(aStrString);
                    return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
                }
                catch
                {
                    return string.Empty;
                }
            }

            public static string Decrypt(string aStrString, string aStrKey, CipherMode mode = CipherMode.ECB, string iv = "12345678")
            {
                try
                {
                    var des = new TripleDESCryptoServiceProvider
                    {
                        Key = Encoding.UTF8.GetBytes(aStrKey.Substring(0, 24)),//长度必须24个字节 
                        Mode = mode,
                        Padding = PaddingMode.PKCS7
                    };
                    if (mode == CipherMode.CBC)
                    {
                        des.IV = Encoding.UTF8.GetBytes(iv);
                    }
                    var desDecrypt = des.CreateDecryptor();
                    var result = "";
                    byte[] buffer = Convert.FromBase64String(aStrString);
                    result = Encoding.UTF8.GetString(desDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
                    return result;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
    }
}
