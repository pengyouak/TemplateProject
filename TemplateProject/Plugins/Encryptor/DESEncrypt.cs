 using System;
using System.IO;
using System.Security.Cryptography;  
using System.Text;
namespace WindCommon.Plugins.Encryptor
{
    /// <summary>
    /// DES����/�����ࡣ
    /// </summary>
    public class DESEncrypt
    {
        public DESEncrypt()
        {
        }

        /// <summary>
        /// DES�����ַ���
        /// </summary>
        /// <param name="encryptString">�����ܵ��ַ���</param>
        /// <param name="encryptKey">������Կ,Ҫ��Ϊ8λ</param>
        /// <returns>���ܳɹ����ؼ��ܺ���ַ�����ʧ�ܷ���Դ��</returns>
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
        /// DES�����ַ���
        /// </summary>
        /// <param name="decryptString">�����ܵ��ַ���</param>
        /// <param name="decryptKey">������Կ,Ҫ��Ϊ8λ,�ͼ�����Կ��ͬ</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ�����ʧ�ܷ�Դ��</returns>
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
        /// ���� 
        /// </summary> 
        public class TripleDESEncrypt
        {
            public static string Encrypt(string aStrString, string aStrKey, CipherMode mode = CipherMode.ECB, string iv = "12345678")
            {
                try
                {
                    var des = new TripleDESCryptoServiceProvider
                    {
                        Key = Encoding.UTF8.GetBytes(aStrKey.Substring(0, 24)),//���ȱ���24���ֽ� 
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
                        Key = Encoding.UTF8.GetBytes(aStrKey.Substring(0, 24)),//���ȱ���24���ֽ� 
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
