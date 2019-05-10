 using System;
using System.Security.Cryptography;
using System.IO;

namespace WindCommon.Plugins.Encryptor
{
	/// <summary>
	/// 哈希加密类
	/// </summary>
	public class Encrypt
	{
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="strText">要加密字符串</param>
        /// <param name="IsLower">是否以小写方式返回</param>
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
        /// 32位SHA1加密
        /// </summary>
        /// <param name="strText">要加密字符串</param>
        /// <param name="IsLower">是否以小写方式返回</param>
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
        /// 32位SHA256加密
        /// </summary>
        /// <param name="strText">要加密字符串</param>
        /// <param name="IsLower">是否以小写方式返回</param>
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
        /// 32位SHA384加密
        /// </summary>
        /// <param name="strText">要加密字符串</param>
        /// <param name="IsLower">是否以小写方式返回</param>
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
