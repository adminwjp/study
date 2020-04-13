namespace Utility
{
#if !(NET20 || NET30 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
    /// <summary>
    /// 安全 加密扩展 不支持 netstandard 1.0 - 1.2
    /// </summary>
    public static class SecurityExtensions
    {
        /// <summary>
        /// str sha1 加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="defaultEncoding">编码类型</param>
        /// <returns></returns>
        public static string Sha1(this string str, string defaultEncoding = "utf-8")
        {
            return SecurityUtils.Sha1(str,defaultEncoding);
        }
        /// <summary>
        /// str sha1 加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="defaultEncoding">编码类型</param>
        /// <returns></returns>
        public static byte[] Sha1ToByte(this string str, string defaultEncoding = "utf-8")
        {
            return SecurityUtils.Sha1ToByte(str,defaultEncoding);
        }

        /// <summary>
        /// str md5 加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="defaultEncoding">编码类型</param>
        /// <returns></returns>
        public static string Md5(this string str, string defaultEncoding = "utf-8")
        {
            return SecurityUtils.Md5(str,defaultEncoding);
        }

        /// <summary>
        /// str md5 加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="defaultEncoding">编码类型</param>
        /// <returns></returns>
        public static byte[] Md5ToByte(this string str, string defaultEncoding = "utf-8")
        {
           return SecurityUtils.Md5ToByte(str,defaultEncoding);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptString">加密字符串</param>
        /// <param name="key">密匙</param>
        /// <param name="iv">向量</param>
        /// <param name="defaultEncoding">默认编码utf-8</param>
        /// <returns></returns>
        public static byte[] AesEncryptToByte(this string encryptString, string key , string iv , string defaultEncoding = "utf-8")
        {
            return SecurityUtils.AesEncryptToByte(encryptString,key,iv,defaultEncoding);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptString">加密字符串</param>
        /// <param name="key">密匙</param>
        /// <param name="iv">向量</param>
        /// <param name="defaultEncoding">默认编码utf-8</param>
        /// <returns></returns>
        public static string AesEncrypt(this string encryptString, string key , string iv, string defaultEncoding = "utf-8")
        {
             return SecurityUtils.AesEncrypt(encryptString,key,iv,defaultEncoding);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="inputData">解密字节</param>
        /// <param name="key">密匙</param>
        /// <param name="iv">向量</param>
        /// <param name="defaultEncoding">默认编码utf-8</param>
        /// <returns></returns>
        public static byte[] AesDecryptToByte(this byte[] inputData, string key, string iv , string defaultEncoding = "utf-8")
        {
             return SecurityUtils.AesDecryptToByte(inputData,key,iv,defaultEncoding);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptString">解密字符串</param>
        /// <param name="key">密匙</param>
        /// <param name="iv">向量</param>
        /// <param name="defaultEncoding">默认编码utf-8</param>
        /// <returns></returns>
        public static string AesDecrypt(this string decryptString, string key ,string iv, string defaultEncoding = "utf-8")
        {
              return SecurityUtils.AesDecrypt(decryptString,key,iv,defaultEncoding);
        }
#if NET472
        #region sha
        public static byte[] Sha1Cng(this byte[] buffer)
        {
            return SecurityUtils.Sha1Cng(buffer);
        }

        public static byte[] Sha256Cng(this byte[] buffer)
        {
            return SecurityUtils.Sha256Cng(buffer);
        }
        public static byte[] Sha384Cng(this byte[] buffer)
        {
          return SecurityUtils.Sha384Cng(buffer);
        }
        public static byte[] Sha512Cng(this byte[] buffer)
        {
          return SecurityUtils.Sha512Cng(buffer);
        }
        #endregion sha

#endif
        #region sha

        public static byte[] Sha1Managed(this byte[] buffer)
        {
            return SecurityUtils.Sha1Managed(buffer);
        }
        public static byte[] Sha1CryptoServiceProvider(this byte[] buffer)
        {
            return SecurityUtils.Sha1CryptoServiceProvider(buffer);
        }
        public static byte[] Sha256(this byte[] buffer)
        {
            return SecurityUtils.Sha256(buffer);
        }
        public static byte[] Sha256Managed(this byte[] buffer)
        {
            return SecurityUtils.Sha256Managed(buffer);
        }
#if !(NET35)
        public static byte[] Sha256CryptoServiceProvider(this byte[] buffer)
        {
            return SecurityUtils.Sha256CryptoServiceProvider(buffer);
        }
        public static byte[] Sha384(this byte[] buffer)
        {
            return SecurityUtils.Sha384(buffer);
        }

        public static byte[] Sha384CryptoServiceProvider(this byte[] buffer)
        {
            return SecurityUtils.Sha384CryptoServiceProvider(buffer);
        }
        public static byte[] Sha512CryptoServiceProvider(this byte[] buffer)
        {
            return SecurityUtils.Sha512CryptoServiceProvider(buffer);
        }
        public static byte[] Sha512Managed(this byte[] buffer)
        {
            return SecurityUtils.Sha512Managed(buffer);
        }
#endif
        public static byte[] Sha384Managed(this byte[] buffer)
        {
            return SecurityUtils.Sha384Managed(buffer);
        }
        public static byte[] Sha512(this byte[] buffer)
        {
            return SecurityUtils.Sha512(buffer);
        }
     
        #endregion sha 

        #region HASH
        public static byte[] Hash(this byte[] buffer)
        {
            return SecurityUtils.Hash(buffer);
        }
        public static byte[] Hmac(this byte[] buffer)
        {
            return SecurityUtils.Hmac(buffer);
        }
        public static byte[] HmacMD5(this byte[] buffer)
        {
            return SecurityUtils.HmacMD5(buffer);
        }
        public static byte[] HmacSHA1(this byte[] buffer)
        {
            return SecurityUtils.HmacSHA1(buffer);
        }
        public static byte[] HmacSHA256(this byte[] buffer)
        {
            return SecurityUtils.HmacSHA256(buffer);
        }
        public static byte[] HmacSHA384(this byte[] buffer)
        {
            return SecurityUtils.HmacSHA384(buffer);
        }
        public static byte[] HmacSHA512(this byte[] buffer)
        {
            return SecurityUtils.HmacSHA512(buffer);
        }
        #endregion HASH

        public static byte[] Md5CryptoServiceProvider(this byte[] buffer)
        {
            return SecurityUtils.Md5CryptoServiceProvider(buffer);
        }
        public static byte[] RsaEncryptToByte(this string data, string xmlString, string defaultEncoding = "utf-8")
        {
            return SecurityUtils.RsaEncryptToByte(data, xmlString, defaultEncoding);
        }
        public static byte[] RsaDecryptToByte(this string data, string xmlString, string defaultEncoding = "utf-8")
        {
            return SecurityUtils.RsaDecryptToByte(data, xmlString, defaultEncoding);
        }
        public static string RsaEncrypt(this string data, string xmlString, string defaultEncoding = "utf-8")
        {
            return SecurityUtils.RsaEncrypt(data, xmlString, defaultEncoding);
        }
        public static string RsaDecrypt(this string data, string xmlString, string defaultEncoding = "utf-8")
        {
            return SecurityUtils.RsaDecrypt(data, xmlString, defaultEncoding);
        }
    }
#endif
}
