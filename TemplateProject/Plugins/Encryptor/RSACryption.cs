using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;

namespace TemplateProject.Plugins.Encryptor
{
    /// <summary> 
    /// RSA加密解密及RSA签名和验证
    /// </summary> 
    public class RSACryption
    {
        public RSACryption()
        {
        }


        #region RSA 加密解密 

        #region RSA 的密钥产生 

        /// <summary>
        /// RSA 的密钥产生 产生私钥 和公钥 
        /// </summary>
        /// <param name="xmlKeys"></param>
        /// <param name="xmlPublicKey"></param>
        public void RSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            xmlKeys = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }
        #endregion

        #region RSA的加密函数 
        //############################################################################## 
        //RSA 方式加密 
        //说明KEY必须是XML的行式,返回的是字符串 
        //在有一点需要说明！！该加密方式有 长度 限制的！！ 
        //############################################################################## 

        //RSA的加密函数  string
        public string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {

            byte[] PlainTextBArray;
            byte[] CypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            PlainTextBArray = (new UnicodeEncoding()).GetBytes(m_strEncryptString);
            CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
            Result = Convert.ToBase64String(CypherTextBArray);
            return Result;

        }
        //RSA的加密函数 byte[]
        public string RSAEncrypt(string xmlPublicKey, byte[] EncryptString)
        {

            byte[] CypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            CypherTextBArray = rsa.Encrypt(EncryptString, false);
            Result = Convert.ToBase64String(CypherTextBArray);
            return Result;

        }
        #endregion

        #region RSA的解密函数 
        //RSA的解密函数  string
        public string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {
            byte[] PlainTextBArray;
            byte[] DypherTextBArray;
            string Result;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            PlainTextBArray = Convert.FromBase64String(m_strDecryptString);
            DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
            Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
            return Result;

        }

        //RSA的解密函数  byte
        public string RSADecrypt(string xmlPrivateKey, byte[] DecryptString)
        {
            byte[] DypherTextBArray;
            string Result;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            DypherTextBArray = rsa.Decrypt(DecryptString, false);
            Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
            return Result;

        }
        #endregion

        #endregion

        #region RSA数字签名 

        #region 获取Hash描述表 
        //获取Hash描述表 
        public bool GetHash(string m_strSource, ref byte[] HashData)
        {
            //从字符串中取得Hash描述 
            byte[] Buffer;
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            HashData = MD5.ComputeHash(Buffer);

            return true;
        }

        //获取Hash描述表 
        public bool GetHash(string m_strSource, ref string strHashData)
        {

            //从字符串中取得Hash描述 
            byte[] Buffer;
            byte[] HashData;
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            HashData = MD5.ComputeHash(Buffer);

            strHashData = Convert.ToBase64String(HashData);
            return true;

        }

        //获取Hash描述表 
        public bool GetHash(System.IO.FileStream objFile, ref byte[] HashData)
        {

            //从文件中取得Hash描述 
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            HashData = MD5.ComputeHash(objFile);
            objFile.Close();

            return true;

        }

        //获取Hash描述表 
        public bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {

            //从文件中取得Hash描述 
            byte[] HashData;
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            HashData = MD5.ComputeHash(objFile);
            objFile.Close();

            strHashData = Convert.ToBase64String(HashData);

            return true;

        }
        #endregion

        #region RSA签名 
        //RSA签名 
        public bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //设置签名的算法为MD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            return true;
        }

        //RSA签名 
        public bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref string m_strEncryptedSignatureData)
        {
            byte[] EncryptedSignatureData;

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //设置签名的算法为MD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            m_strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);

            return true;
        }

        //RSA签名 
        public bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            byte[] HashbyteSignature;

            HashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //设置签名的算法为MD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            return true;
        }

        //RSA签名 
        public bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref string m_strEncryptedSignatureData)
        {
            byte[] HashbyteSignature;
            byte[] EncryptedSignatureData;

            HashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //设置签名的算法为MD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            m_strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);

            return true;
        }
        #endregion

        #region RSA 签名验证 

        public bool SignatureDeformatter(string p_strKeyPublic, byte[] HashbyteDeformatter, byte[] DeformatterData)
        {
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //指定解密的时候HASH算法为MD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, byte[] DeformatterData)
        {
            byte[] HashbyteDeformatter;

            HashbyteDeformatter = Convert.FromBase64String(p_strHashbyteDeformatter);

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //指定解密的时候HASH算法为MD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string p_strKeyPublic, byte[] HashbyteDeformatter, string p_strDeformatterData)
        {
            byte[] DeformatterData;

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //指定解密的时候HASH算法为MD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            DeformatterData = Convert.FromBase64String(p_strDeformatterData);

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, string p_strDeformatterData)
        {

            byte[] DeformatterData;
            byte[] HashbyteDeformatter;

            HashbyteDeformatter = Convert.FromBase64String(p_strHashbyteDeformatter);
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //指定解密的时候HASH算法为MD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            DeformatterData = Convert.FromBase64String(p_strDeformatterData);

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion
        #endregion
    }

    /// <summary>
    /// RSA操作类
    /// </summary>
    public class RSA
    {
        private RSACryptoServiceProvider rsa;

        /// <summary>
        /// 导出XML格式密钥对，如果convertToPublic含私钥的RSA将只返回公钥，仅含公钥的RSA不受影响
        /// </summary>
        public string ToXML(bool convertToPublic = false)
        {
            return rsa.ToXmlString(!rsa.PublicOnly && !convertToPublic);
        }
        /// <summary>
        /// 导出PEM PKCS#1格式密钥对，如果convertToPublic含私钥的RSA将只返回公钥，仅含公钥的RSA不受影响
        /// </summary>
        public string ToPEM_PKCS1(bool convertToPublic = false)
        {
            return RSA_PEM.ToPEM(rsa, convertToPublic, false);
        }
        /// <summary>
        /// 导出PEM PKCS#8格式密钥对，如果convertToPublic含私钥的RSA将只返回公钥，仅含公钥的RSA不受影响
        /// </summary>
        public string ToPEM_PKCS8(bool convertToPublic = false)
        {
            return RSA_PEM.ToPEM(rsa, convertToPublic, true);
        }

        /// <summary>
        /// 加密字符串（utf-8），出错抛异常
        /// </summary>
        public string Encode(string str)
        {
            return RSA_Unit.Base64EncodeBytes(Encode(Encoding.UTF8.GetBytes(str)));
        }
        /// <summary>
        /// 加密数据，出错抛异常
        /// </summary>
        public byte[] Encode(byte[] data)
        {
            int blockLen = rsa.KeySize / 8 - 11;
            if (data.Length <= blockLen)
            {
                return rsa.Encrypt(data, false);
            }

            using (var dataStream = new MemoryStream(data))
            using (var enStream = new MemoryStream())
            {
                Byte[] buffer = new Byte[blockLen];
                int len = dataStream.Read(buffer, 0, blockLen);

                while (len > 0)
                {
                    Byte[] block = new Byte[len];
                    Array.Copy(buffer, 0, block, 0, len);

                    Byte[] enBlock = rsa.Encrypt(block, false);
                    enStream.Write(enBlock, 0, enBlock.Length);

                    len = dataStream.Read(buffer, 0, blockLen);
                }

                return enStream.ToArray();
            }
        }
        /// <summary>
        /// 解密字符串（utf-8），解密异常返回null
        /// </summary>
        public string DecodeOrNull(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return null;
            }
            var byts = RSA_Unit.Base64DecodeBytes(str);
            if (byts == null)
            {
                return null;
            }
            var val = DecodeOrNull(byts);
            if (val == null)
            {
                return null;
            }
            return Encoding.UTF8.GetString(val);
        }
        /// <summary>
        /// 解密数据，解密异常返回null
        /// </summary>
        public byte[] DecodeOrNull(byte[] data)
        {
            try
            {
                int blockLen = rsa.KeySize / 8;
                if (data.Length <= blockLen)
                {
                    return rsa.Decrypt(data, false);
                }

                using (var dataStream = new MemoryStream(data))
                using (var deStream = new MemoryStream())
                {
                    Byte[] buffer = new Byte[blockLen];
                    int len = dataStream.Read(buffer, 0, blockLen);

                    while (len > 0)
                    {
                        Byte[] block = new Byte[len];
                        Array.Copy(buffer, 0, block, 0, len);

                        Byte[] deBlock = rsa.Decrypt(block, false);
                        deStream.Write(deBlock, 0, deBlock.Length);

                        len = dataStream.Read(buffer, 0, blockLen);
                    }

                    return deStream.ToArray();
                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 对str进行签名，并指定hash算法（如：SHA256）
        /// </summary>
        public string Sign(string hash, string str)
        {
            return RSA_Unit.Base64EncodeBytes(Sign(hash, Encoding.UTF8.GetBytes(str)));
        }
        /// <summary>
        /// 对data进行签名，并指定hash算法（如：SHA256）
        /// </summary>
        public byte[] Sign(string hash, byte[] data)
        {
            return rsa.SignData(data, hash);
        }
        /// <summary>
        /// 验证字符串str的签名是否是sgin，并指定hash算法（如：SHA256）
        /// </summary>
        public bool Verify(string hash, string sgin, string str)
        {
            var byts = RSA_Unit.Base64DecodeBytes(sgin);
            if (byts == null)
            {
                return false;
            }
            return Verify(hash, byts, Encoding.UTF8.GetBytes(str));
        }
        /// <summary>
        /// 验证data的签名是否是sgin，并指定hash算法（如：SHA256）
        /// </summary>
        public bool Verify(string hash, byte[] sgin, byte[] data)
        {
            try
            {
                return rsa.VerifyData(data, hash, sgin);
            }
            catch
            {
                return false;
            }
        }

        private void _init()
        {
            var rsaParams = new CspParameters();
            rsaParams.Flags = CspProviderFlags.UseMachineKeyStore;
            rsa = new RSACryptoServiceProvider(rsaParams);
        }

        /// <summary>
        /// 用指定密钥大小创建一个新的RSA，出错抛异常
        /// </summary>
        public RSA(int keySize)
        {
            _init();

            var rsaParams = new CspParameters();
            rsaParams.Flags = CspProviderFlags.UseMachineKeyStore;
            rsa = new RSACryptoServiceProvider(keySize, rsaParams);
        }
        /// <summary>
        /// 通过指定的密钥，创建一个RSA，xml内可以只包含一个公钥或私钥，或都包含，出错抛异常
        /// </summary>
        public RSA(string xml)
        {
            _init();

            rsa.FromXmlString(xml);
        }
        /// <summary>
        /// 通过一个pem文件创建RSA，pem为公钥或私钥，出错抛异常
        /// </summary>
        public RSA(string pem, bool noop)
        {
            _init();

            rsa = RSA_PEM.FromPEM(pem);
        }
    }

    /// <summary>
	/// RSA PEM格式秘钥对的解析和导出
	/// </summary>
	public class RSA_PEM
    {
        static private Regex _PEMCode = new Regex(@"--+.+?--+|\s+");
        static private byte[] _SeqOID = new byte[] { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
        static private byte[] _Ver = new byte[] { 0x02, 0x01, 0x00 };

        /// <summary>
        /// 用PEM格式密钥对创建RSA，支持PKCS#1、PKCS#8格式的PEM
        /// </summary>
        public static RSACryptoServiceProvider FromPEM(string pem)
        {
            var rsaParams = new CspParameters();
            rsaParams.Flags = CspProviderFlags.UseMachineKeyStore;
            var rsa = new RSACryptoServiceProvider(rsaParams);

            var param = new RSAParameters();

            var base64 = _PEMCode.Replace(pem, "");
            var data = RSA_Unit.Base64DecodeBytes(base64);
            if (data == null)
            {
                throw new Exception("PEM内容无效");
            }
            var idx = 0;

            //读取长度
            Func<byte, int> readLen = (first) =>
            {
                if (data[idx] == first)
                {
                    idx++;
                    if (data[idx] == 0x81)
                    {
                        idx++;
                        return data[idx++];
                    }
                    else if (data[idx] == 0x82)
                    {
                        idx++;
                        return (((int)data[idx++]) << 8) + data[idx++];
                    }
                    else if (data[idx] < 0x80)
                    {
                        return data[idx++];
                    }
                }
                throw new Exception("PEM未能提取到数据");
            };
            //读取块数据
            Func<byte[]> readBlock = () =>
            {
                var len = readLen(0x02);
                if (data[idx] == 0x00)
                {
                    idx++;
                    len--;
                }
                var val = data.sub(idx, len);
                idx += len;
                return val;
            };
            //比较data从idx位置开始是否是byts内容
            Func<byte[], bool> eq = (byts) =>
            {
                for (var i = 0; i < byts.Length; i++, idx++)
                {
                    if (idx >= data.Length)
                    {
                        return false;
                    }
                    if (byts[i] != data[idx])
                    {
                        return false;
                    }
                }
                return true;
            };

            if (pem.Contains("PUBLIC KEY"))
            {
                /****使用公钥****/
                //读取数据总长度
                readLen(0x30);
                if (!eq(_SeqOID))
                {
                    throw new Exception("PEM未知格式");
                }
                //读取1长度
                readLen(0x03);
                idx++;//跳过0x00
                      //读取2长度
                readLen(0x30);

                //Modulus
                param.Modulus = readBlock();

                //Exponent
                param.Exponent = readBlock();
            }
            else if (pem.Contains("PRIVATE KEY"))
            {
                /****使用私钥****/
                //读取数据总长度
                readLen(0x30);

                //读取版本号
                if (!eq(_Ver))
                {
                    throw new Exception("PEM未知版本");
                }

                //检测PKCS8
                var idx2 = idx;
                if (eq(_SeqOID))
                {
                    //读取1长度
                    readLen(0x04);
                    //读取2长度
                    readLen(0x30);

                    //读取版本号
                    if (!eq(_Ver))
                    {
                        throw new Exception("PEM版本无效");
                    }
                }
                else
                {
                    idx = idx2;
                }

                //读取数据
                param.Modulus = readBlock();
                param.Exponent = readBlock();
                param.D = readBlock();
                param.P = readBlock();
                param.Q = readBlock();
                param.DP = readBlock();
                param.DQ = readBlock();
                param.InverseQ = readBlock();
            }
            else
            {
                throw new Exception("pem需要BEGIN END标头");
            }

            rsa.ImportParameters(param);
            return rsa;
        }

        /// <summary>
        /// 将RSA中的密钥对转换成PEM格式，usePKCS8=false时返回PKCS#1格式，否则返回PKCS#8格式，如果convertToPublic含私钥的RSA将只返回公钥，仅含公钥的RSA不受影响
        /// </summary>
        public static string ToPEM(RSACryptoServiceProvider rsa, bool convertToPublic, bool usePKCS8)
        {
            //https://www.jianshu.com/p/25803dd9527d
            //https://www.cnblogs.com/ylz8401/p/8443819.html
            //https://blog.csdn.net/jiayanhui2877/article/details/47187077
            //https://blog.csdn.net/xuanshao_/article/details/51679824
            //https://blog.csdn.net/xuanshao_/article/details/51672547

            var ms = new MemoryStream();
            //写入一个长度字节码
            Action<int> writeLenByte = (len) =>
            {
                if (len < 0x80)
                {
                    ms.WriteByte((byte)len);
                }
                else if (len <= 0xff)
                {
                    ms.WriteByte(0x81);
                    ms.WriteByte((byte)len);
                }
                else
                {
                    ms.WriteByte(0x82);
                    ms.WriteByte((byte)(len >> 8 & 0xff));
                    ms.WriteByte((byte)(len & 0xff));
                }
            };
            //写入一块数据
            Action<byte[]> writeBlock = (byts) =>
            {
                var addZero = (byts[0] >> 4) >= 0x8;
                ms.WriteByte(0x02);
                var len = byts.Length + (addZero ? 1 : 0);
                writeLenByte(len);

                if (addZero)
                {
                    ms.WriteByte(0x00);
                }
                ms.Write(byts, 0, byts.Length);
            };
            //根据后续内容长度写入长度数据
            Func<int, byte[], byte[]> writeLen = (index, byts) =>
            {
                var len = byts.Length - index;

                ms.SetLength(0);
                ms.Write(byts, 0, index);
                writeLenByte(len);
                ms.Write(byts, index, len);

                return ms.ToArray();
            };


            if (rsa.PublicOnly || convertToPublic)
            {
                /****生成公钥****/
                var param = rsa.ExportParameters(false);


                //写入总字节数，不含本段长度，额外需要24字节的头，后续计算好填入
                ms.WriteByte(0x30);
                var index1 = (int)ms.Length;

                //固定内容
                // encoded OID sequence for PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
                ms.writeAll(_SeqOID);

                //从0x00开始的后续长度
                ms.WriteByte(0x03);
                var index2 = (int)ms.Length;
                ms.WriteByte(0x00);

                //后续内容长度
                ms.WriteByte(0x30);
                var index3 = (int)ms.Length;

                //写入Modulus
                writeBlock(param.Modulus);

                //写入Exponent
                writeBlock(param.Exponent);


                //计算空缺的长度
                var byts = ms.ToArray();

                byts = writeLen(index3, byts);
                byts = writeLen(index2, byts);
                byts = writeLen(index1, byts);


                return "-----BEGIN PUBLIC KEY-----\n" + RSA_Unit.TextBreak(RSA_Unit.Base64EncodeBytes(byts), 64) + "\n-----END PUBLIC KEY-----";
            }
            else
            {
                /****生成私钥****/
                var param = rsa.ExportParameters(true);

                //写入总字节数，后续写入
                ms.WriteByte(0x30);
                int index1 = (int)ms.Length;

                //写入版本号
                ms.writeAll(_Ver);

                //PKCS8 多一段数据
                int index2 = -1, index3 = -1;
                if (usePKCS8)
                {
                    //固定内容
                    ms.writeAll(_SeqOID);

                    //后续内容长度
                    ms.WriteByte(0x04);
                    index2 = (int)ms.Length;

                    //后续内容长度
                    ms.WriteByte(0x30);
                    index3 = (int)ms.Length;

                    //写入版本号
                    ms.writeAll(_Ver);
                }

                //写入数据
                writeBlock(param.Modulus);
                writeBlock(param.Exponent);
                writeBlock(param.D);
                writeBlock(param.P);
                writeBlock(param.Q);
                writeBlock(param.DP);
                writeBlock(param.DQ);
                writeBlock(param.InverseQ);


                //计算空缺的长度
                var byts = ms.ToArray();

                if (index2 != -1)
                {
                    byts = writeLen(index3, byts);
                    byts = writeLen(index2, byts);
                }
                byts = writeLen(index1, byts);


                var flag = " PRIVATE KEY";
                if (!usePKCS8)
                {
                    flag = " RSA" + flag;
                }
                return "-----BEGIN" + flag + "-----\n" + RSA_Unit.TextBreak(RSA_Unit.Base64EncodeBytes(byts), 64) + "\n-----END" + flag + "-----";
            }
        }
    }

    /// <summary>
	/// 封装的一些通用方法
	/// </summary>
	public class RSA_Unit
    {
        static public string Base64EncodeBytes(byte[] byts)
        {
            return Convert.ToBase64String(byts);
        }
        static public byte[] Base64DecodeBytes(string str)
        {
            try
            {
                return Convert.FromBase64String(str);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 把字符串按每行多少个字断行
        /// </summary>
        static public string TextBreak(string text, int line)
        {
            var idx = 0;
            var len = text.Length;
            var str = new StringBuilder();
            while (idx < len)
            {
                if (idx > 0)
                {
                    str.Append('\n');
                }
                if (idx + line >= len)
                {
                    str.Append(text.Substring(idx));
                }
                else
                {
                    str.Append(text.Substring(idx, line));
                }
                idx += line;
            }
            return str.ToString();
        }
    }

    static public class Extensions
    {
        /// <summary>
        /// 从数组start开始到指定长度复制一份
        /// </summary>
        static public T[] sub<T>(this T[] arr, int start, int count)
        {
            T[] val = new T[count];
            for (var i = 0; i < count; i++)
            {
                val[i] = arr[start + i];
            }
            return val;
        }
        static public void writeAll(this Stream stream, byte[] byts)
        {
            stream.Write(byts, 0, byts.Length);
        }
    }
}
