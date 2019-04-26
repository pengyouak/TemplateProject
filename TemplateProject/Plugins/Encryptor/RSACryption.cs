using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;

namespace TemplateProject.Plugins.Encryptor
{
    /// <summary> 
    /// RSA���ܽ��ܼ�RSAǩ������֤
    /// </summary> 
    public class RSACryption
    {
        public RSACryption()
        {
        }


        #region RSA ���ܽ��� 

        #region RSA ����Կ���� 

        /// <summary>
        /// RSA ����Կ���� ����˽Կ �͹�Կ 
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

        #region RSA�ļ��ܺ��� 
        //############################################################################## 
        //RSA ��ʽ���� 
        //˵��KEY������XML����ʽ,���ص����ַ��� 
        //����һ����Ҫ˵�������ü��ܷ�ʽ�� ���� ���Ƶģ��� 
        //############################################################################## 

        //RSA�ļ��ܺ���  string
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
        //RSA�ļ��ܺ��� byte[]
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

        #region RSA�Ľ��ܺ��� 
        //RSA�Ľ��ܺ���  string
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

        //RSA�Ľ��ܺ���  byte
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

        #region RSA����ǩ�� 

        #region ��ȡHash������ 
        //��ȡHash������ 
        public bool GetHash(string m_strSource, ref byte[] HashData)
        {
            //���ַ�����ȡ��Hash���� 
            byte[] Buffer;
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            HashData = MD5.ComputeHash(Buffer);

            return true;
        }

        //��ȡHash������ 
        public bool GetHash(string m_strSource, ref string strHashData)
        {

            //���ַ�����ȡ��Hash���� 
            byte[] Buffer;
            byte[] HashData;
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            HashData = MD5.ComputeHash(Buffer);

            strHashData = Convert.ToBase64String(HashData);
            return true;

        }

        //��ȡHash������ 
        public bool GetHash(System.IO.FileStream objFile, ref byte[] HashData)
        {

            //���ļ���ȡ��Hash���� 
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            HashData = MD5.ComputeHash(objFile);
            objFile.Close();

            return true;

        }

        //��ȡHash������ 
        public bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {

            //���ļ���ȡ��Hash���� 
            byte[] HashData;
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            HashData = MD5.ComputeHash(objFile);
            objFile.Close();

            strHashData = Convert.ToBase64String(HashData);

            return true;

        }
        #endregion

        #region RSAǩ�� 
        //RSAǩ�� 
        public bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //����ǩ�����㷨ΪMD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            return true;
        }

        //RSAǩ�� 
        public bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref string m_strEncryptedSignatureData)
        {
            byte[] EncryptedSignatureData;

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //����ǩ�����㷨ΪMD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            m_strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);

            return true;
        }

        //RSAǩ�� 
        public bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            byte[] HashbyteSignature;

            HashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //����ǩ�����㷨ΪMD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            return true;
        }

        //RSAǩ�� 
        public bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref string m_strEncryptedSignatureData)
        {
            byte[] HashbyteSignature;
            byte[] EncryptedSignatureData;

            HashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //����ǩ�����㷨ΪMD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            m_strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);

            return true;
        }
        #endregion

        #region RSA ǩ����֤ 

        public bool SignatureDeformatter(string p_strKeyPublic, byte[] HashbyteDeformatter, byte[] DeformatterData)
        {
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
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
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
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
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
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
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
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
    /// RSA������
    /// </summary>
    public class RSA
    {
        private RSACryptoServiceProvider rsa;

        /// <summary>
        /// ����XML��ʽ��Կ�ԣ����convertToPublic��˽Կ��RSA��ֻ���ع�Կ��������Կ��RSA����Ӱ��
        /// </summary>
        public string ToXML(bool convertToPublic = false)
        {
            return rsa.ToXmlString(!rsa.PublicOnly && !convertToPublic);
        }
        /// <summary>
        /// ����PEM PKCS#1��ʽ��Կ�ԣ����convertToPublic��˽Կ��RSA��ֻ���ع�Կ��������Կ��RSA����Ӱ��
        /// </summary>
        public string ToPEM_PKCS1(bool convertToPublic = false)
        {
            return RSA_PEM.ToPEM(rsa, convertToPublic, false);
        }
        /// <summary>
        /// ����PEM PKCS#8��ʽ��Կ�ԣ����convertToPublic��˽Կ��RSA��ֻ���ع�Կ��������Կ��RSA����Ӱ��
        /// </summary>
        public string ToPEM_PKCS8(bool convertToPublic = false)
        {
            return RSA_PEM.ToPEM(rsa, convertToPublic, true);
        }

        /// <summary>
        /// �����ַ�����utf-8�����������쳣
        /// </summary>
        public string Encode(string str)
        {
            return RSA_Unit.Base64EncodeBytes(Encode(Encoding.UTF8.GetBytes(str)));
        }
        /// <summary>
        /// �������ݣ��������쳣
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
        /// �����ַ�����utf-8���������쳣����null
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
        /// �������ݣ������쳣����null
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
        /// ��str����ǩ������ָ��hash�㷨���磺SHA256��
        /// </summary>
        public string Sign(string hash, string str)
        {
            return RSA_Unit.Base64EncodeBytes(Sign(hash, Encoding.UTF8.GetBytes(str)));
        }
        /// <summary>
        /// ��data����ǩ������ָ��hash�㷨���磺SHA256��
        /// </summary>
        public byte[] Sign(string hash, byte[] data)
        {
            return rsa.SignData(data, hash);
        }
        /// <summary>
        /// ��֤�ַ���str��ǩ���Ƿ���sgin����ָ��hash�㷨���磺SHA256��
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
        /// ��֤data��ǩ���Ƿ���sgin����ָ��hash�㷨���磺SHA256��
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
        /// ��ָ����Կ��С����һ���µ�RSA���������쳣
        /// </summary>
        public RSA(int keySize)
        {
            _init();

            var rsaParams = new CspParameters();
            rsaParams.Flags = CspProviderFlags.UseMachineKeyStore;
            rsa = new RSACryptoServiceProvider(keySize, rsaParams);
        }
        /// <summary>
        /// ͨ��ָ������Կ������һ��RSA��xml�ڿ���ֻ����һ����Կ��˽Կ���򶼰������������쳣
        /// </summary>
        public RSA(string xml)
        {
            _init();

            rsa.FromXmlString(xml);
        }
        /// <summary>
        /// ͨ��һ��pem�ļ�����RSA��pemΪ��Կ��˽Կ���������쳣
        /// </summary>
        public RSA(string pem, bool noop)
        {
            _init();

            rsa = RSA_PEM.FromPEM(pem);
        }
    }

    /// <summary>
	/// RSA PEM��ʽ��Կ�ԵĽ����͵���
	/// </summary>
	public class RSA_PEM
    {
        static private Regex _PEMCode = new Regex(@"--+.+?--+|\s+");
        static private byte[] _SeqOID = new byte[] { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
        static private byte[] _Ver = new byte[] { 0x02, 0x01, 0x00 };

        /// <summary>
        /// ��PEM��ʽ��Կ�Դ���RSA��֧��PKCS#1��PKCS#8��ʽ��PEM
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
                throw new Exception("PEM������Ч");
            }
            var idx = 0;

            //��ȡ����
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
                throw new Exception("PEMδ����ȡ������");
            };
            //��ȡ������
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
            //�Ƚ�data��idxλ�ÿ�ʼ�Ƿ���byts����
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
                /****ʹ�ù�Կ****/
                //��ȡ�����ܳ���
                readLen(0x30);
                if (!eq(_SeqOID))
                {
                    throw new Exception("PEMδ֪��ʽ");
                }
                //��ȡ1����
                readLen(0x03);
                idx++;//����0x00
                      //��ȡ2����
                readLen(0x30);

                //Modulus
                param.Modulus = readBlock();

                //Exponent
                param.Exponent = readBlock();
            }
            else if (pem.Contains("PRIVATE KEY"))
            {
                /****ʹ��˽Կ****/
                //��ȡ�����ܳ���
                readLen(0x30);

                //��ȡ�汾��
                if (!eq(_Ver))
                {
                    throw new Exception("PEMδ֪�汾");
                }

                //���PKCS8
                var idx2 = idx;
                if (eq(_SeqOID))
                {
                    //��ȡ1����
                    readLen(0x04);
                    //��ȡ2����
                    readLen(0x30);

                    //��ȡ�汾��
                    if (!eq(_Ver))
                    {
                        throw new Exception("PEM�汾��Ч");
                    }
                }
                else
                {
                    idx = idx2;
                }

                //��ȡ����
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
                throw new Exception("pem��ҪBEGIN END��ͷ");
            }

            rsa.ImportParameters(param);
            return rsa;
        }

        /// <summary>
        /// ��RSA�е���Կ��ת����PEM��ʽ��usePKCS8=falseʱ����PKCS#1��ʽ�����򷵻�PKCS#8��ʽ�����convertToPublic��˽Կ��RSA��ֻ���ع�Կ��������Կ��RSA����Ӱ��
        /// </summary>
        public static string ToPEM(RSACryptoServiceProvider rsa, bool convertToPublic, bool usePKCS8)
        {
            //https://www.jianshu.com/p/25803dd9527d
            //https://www.cnblogs.com/ylz8401/p/8443819.html
            //https://blog.csdn.net/jiayanhui2877/article/details/47187077
            //https://blog.csdn.net/xuanshao_/article/details/51679824
            //https://blog.csdn.net/xuanshao_/article/details/51672547

            var ms = new MemoryStream();
            //д��һ�������ֽ���
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
            //д��һ������
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
            //���ݺ������ݳ���д�볤������
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
                /****���ɹ�Կ****/
                var param = rsa.ExportParameters(false);


                //д�����ֽ������������γ��ȣ�������Ҫ24�ֽڵ�ͷ���������������
                ms.WriteByte(0x30);
                var index1 = (int)ms.Length;

                //�̶�����
                // encoded OID sequence for PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
                ms.writeAll(_SeqOID);

                //��0x00��ʼ�ĺ�������
                ms.WriteByte(0x03);
                var index2 = (int)ms.Length;
                ms.WriteByte(0x00);

                //�������ݳ���
                ms.WriteByte(0x30);
                var index3 = (int)ms.Length;

                //д��Modulus
                writeBlock(param.Modulus);

                //д��Exponent
                writeBlock(param.Exponent);


                //�����ȱ�ĳ���
                var byts = ms.ToArray();

                byts = writeLen(index3, byts);
                byts = writeLen(index2, byts);
                byts = writeLen(index1, byts);


                return "-----BEGIN PUBLIC KEY-----\n" + RSA_Unit.TextBreak(RSA_Unit.Base64EncodeBytes(byts), 64) + "\n-----END PUBLIC KEY-----";
            }
            else
            {
                /****����˽Կ****/
                var param = rsa.ExportParameters(true);

                //д�����ֽ���������д��
                ms.WriteByte(0x30);
                int index1 = (int)ms.Length;

                //д��汾��
                ms.writeAll(_Ver);

                //PKCS8 ��һ������
                int index2 = -1, index3 = -1;
                if (usePKCS8)
                {
                    //�̶�����
                    ms.writeAll(_SeqOID);

                    //�������ݳ���
                    ms.WriteByte(0x04);
                    index2 = (int)ms.Length;

                    //�������ݳ���
                    ms.WriteByte(0x30);
                    index3 = (int)ms.Length;

                    //д��汾��
                    ms.writeAll(_Ver);
                }

                //д������
                writeBlock(param.Modulus);
                writeBlock(param.Exponent);
                writeBlock(param.D);
                writeBlock(param.P);
                writeBlock(param.Q);
                writeBlock(param.DP);
                writeBlock(param.DQ);
                writeBlock(param.InverseQ);


                //�����ȱ�ĳ���
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
	/// ��װ��һЩͨ�÷���
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
        /// ���ַ�����ÿ�ж��ٸ��ֶ���
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
        /// ������start��ʼ��ָ�����ȸ���һ��
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
