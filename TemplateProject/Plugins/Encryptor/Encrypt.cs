 using System;
using System.Security.Cryptography;
using System.IO;

namespace WindCommon.Plugins.Encryptor
{
	/// <summary>
	/// ��ϣ������
	/// </summary>
	public class Encrypt
	{
        /// <summary>
        /// 32λMD5����
        /// </summary>
        /// <param name="strText">Ҫ�����ַ���</param>
        /// <param name="IsLower">�Ƿ���Сд��ʽ����</param>
        /// <returns></returns>
        public static string MD5Encrypt(string strText, bool IsLower)
        {
            var ret = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strText, "md5");
            if (IsLower)
                return ret.ToLower();
            else
                return ret.ToUpper();
        }

        /// <summary>
        /// 32λSHA1����
        /// </summary>
        /// <param name="strText">Ҫ�����ַ���</param>
        /// <param name="IsLower">�Ƿ���Сд��ʽ����</param>
        /// <returns></returns>
        public static string SHA1Encrypt(string strText, bool IsLower)
        {
            var ret = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strText, "sha1");
            if (IsLower)
                return ret.ToLower();
            else
                return ret.ToUpper();
        }

        /// <summary>
        /// 32λSHA256����
        /// </summary>
        /// <param name="strText">Ҫ�����ַ���</param>
        /// <param name="IsLower">�Ƿ���Сд��ʽ����</param>
        /// <returns></returns>
        public static string SHA256Encrypt(string strText, bool IsLower)
        {
            var ret = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strText, "sha256");
            if (IsLower)
                return ret.ToLower();
            else
                return ret.ToUpper();
        }
        /// <summary>
        /// 32λSHA384����
        /// </summary>
        /// <param name="strText">Ҫ�����ַ���</param>
        /// <param name="IsLower">�Ƿ���Сд��ʽ����</param>
        /// <returns></returns>
        public static string SHA384Encrypt(string strText, bool IsLower)
        {
            var ret = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strText, "sha384");
            if (IsLower)
                return ret.ToLower();
            else
                return ret.ToUpper();
        }
    }
}
