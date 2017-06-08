using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Encryption
{
    public class Encryptor
    {
        //默认密匙,不定期更换
        //如：leadertech
        public static string CodeKey = "123";


        /// <summary>
        /// DES加密
        /// </summary>
        public static string EncryptDES(string encryptString)
        {
            if (string.IsNullOrEmpty(encryptString) || string.IsNullOrEmpty(CodeKey))
                throw new ArgumentNullException();
            return EncryptDES(encryptString, CodeKey);
        }


        /// <summary>
        /// DES解密
        /// </summary>
        public static string DecryptDES(string decryptString)
        {
            if (string.IsNullOrEmpty(decryptString) || string.IsNullOrEmpty(CodeKey))
                throw new ArgumentNullException();
            return DecryptDES(decryptString, CodeKey);
        }


        //默认密钥向量
        private static byte[] Keys = { 0xEF, 0xAB, 0x56, 0x78, 0x90, 0x34, 0xCD, 0x12 };


        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                if (string.IsNullOrEmpty(encryptString) || string.IsNullOrEmpty(encryptKey))
                    throw new ArgumentNullException();


                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                string str = Convert.ToBase64String(mStream.ToArray());
                return str;
            }
            catch
            {
                return encryptString;
            }
        }


        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                if (string.IsNullOrEmpty(decryptString) || string.IsNullOrEmpty(decryptKey))
                    throw new ArgumentNullException();


                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
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
        /// 使用MD5算法进行加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string EncryptMd5(string encryptString)
        {
            if (string.IsNullOrEmpty(encryptString) || string.IsNullOrEmpty(CodeKey))
                throw new ArgumentNullException();


            //创建MD5对象
            MD5 md5 = MD5.Create();
            //将字符串转成字节数组
            byte[] bs = Encoding.UTF8.GetBytes(encryptString);
            //加密
            byte[] bs2 = md5.ComputeHash(bs);
            //将数组转换成字符串
            StringBuilder sb = new StringBuilder();
            foreach (var tmp in bs2)
            {
                //0-255=>00-ff
                sb.Append(tmp.ToString("x2").ToLower());
            }
            return sb.ToString();
        }


        public static string EncryptAES(string encryptString, string encryptKey)
        {
            if (string.IsNullOrEmpty(encryptString) || string.IsNullOrEmpty(encryptKey))
                throw new ArgumentNullException();
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(encryptString);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(encryptKey);


            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
            string result = Convert.ToBase64String(bytesEncrypted);
            return result;
        }


        public static string EncryptAES(string encryptString)
        {
            if (string.IsNullOrEmpty(encryptString) || string.IsNullOrEmpty(CodeKey))
                throw new ArgumentNullException();
            return EncryptAES(encryptString, CodeKey);
        }


        public static string DecryptAES(string decryptString, string decryptKey)
        {
            if (string.IsNullOrEmpty(decryptString) || string.IsNullOrEmpty(decryptKey))
                throw new ArgumentNullException();
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(decryptString);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(decryptKey);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);
            string result = Encoding.UTF8.GetString(bytesDecrypted);
            return result;
        }


        public static string DecryptAES(string decryptString)
        {
            if (string.IsNullOrEmpty(decryptString) || string.IsNullOrEmpty(CodeKey))
                throw new ArgumentNullException();
            return DecryptAES(decryptString, CodeKey);
        }

        public static string EncryptSHA256(string plainString)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(plainString), 0, Encoding.ASCII.GetByteCount(plainString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }


        #region privateMethod
        private static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;


            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };


            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;


                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);


                    AES.Mode = CipherMode.CBC;


                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }


            return encryptedBytes;
        }


        private static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;


            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };


            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;


                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);


                    AES.Mode = CipherMode.CBC;


                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }


            return decryptedBytes;
        }
        #endregion
    }
}




