using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Dragon.BlackProject.Common
{
    public class MD5Encrypt
    {
        public static string Encrypt(string source, int length = 32)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;

            // 使用 using 确保资源释放
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(source);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // 优化：直接指定 StringBuilder 容量，避免扩容
                StringBuilder sb = new StringBuilder(32);
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                string result = sb.ToString();

                if (length == 16)
                {
                    // 16位MD5通常是指取32位MD5的中间16位（第9位到第24位）
                    return result.Substring(8, 16);
                }

                return result;
            }
        }

        public static string AbstractFile(string fileName)
        {
            // 增加文件存在性检查，防止报错
            if (!File.Exists(fileName)) return string.Empty;

            // 使用 File.OpenRead 简化代码，它等同于 new FileStream(..., FileMode.Open, FileAccess.Read, FileShare.Read)
            using (FileStream file = File.OpenRead(fileName))
            {
                return AbstractFile(file);
            }
        }

        public static string AbstractFile(Stream stream)
        {
            // 【优化点】使用 MD5.Create() 替代 MD5CryptoServiceProvider
            // 并且使用 using 块确保资源释放
            using (MD5 md5 = MD5.Create())
            {
                byte[] retVal = md5.ComputeHash(stream);

                // 优化 StringBuilder 容量，MD5 固定 32 个字符
                StringBuilder sb = new StringBuilder(32);
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }


    }
}
