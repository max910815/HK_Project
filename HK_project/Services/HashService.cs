using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using HK_project.Interface;

namespace HK_Product.Services
{
    public class HashService : IHashService
    {
        public string MD5Hash(string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            StringBuilder sb;

            using (MD5 md5 = MD5.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(rawString);

                //進行MD5雜湊加密
                byte[] encryption = md5.ComputeHash(byteArray);

                sb = new StringBuilder();

                for (int i = 0; i < encryption.Length; i++)
                {
                    //或"X2" /"x2" format each one as a hexadecimal string
                    //X (十六進位格式規範)
                    //https://learn.microsoft.com/zh-tw/dotnet/standard/base-types/standard-numeric-format-strings#hexadecimal-format-specifier-x
                    sb.Append(encryption[i].ToString("x2"));
                }


                //Aggregate方法將運算的結果作為字符串返回。所以加密字串的串接方式使用了字串相加(concatenation)的方式
                //字串相加(concatenation)對於需要連接大量字串的情況下,其效率低於StringBuilder
                string hex = encryption.Aggregate(string.Empty, (current, bt) => current + bt.ToString("x2"));
            }

            return sb.ToString();
        }
        public string MD5HashBase64(string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            string result = "";
            using (MD5 md5 = MD5.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(rawString);
                //進行MD5雜湊加密
                byte[] encryption = md5.ComputeHash(byteArray);

                result = Convert.ToBase64String(encryption);
            }

            return result;
        }

        //SHA1演算法雜湊大小是160 位元
        public string SHA1Hash(string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            StringBuilder sb;

            using (SHA1 sha1 = SHA1.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(rawString);

                byte[] encryption = sha1.ComputeHash(byteArray);


                sb = new StringBuilder();
                /*
                for (int i = 0; i < encryption.Length; i++)
                {
                    sb.Append(encryption[i].ToString("x2"));
                }
                */

                foreach (byte bt in encryption)
                {
                    sb.Append(bt.ToString("x2"));
                }
            }

            return sb.ToString(); ;
        }

        //SHA256演算法雜湊大小是256位元 : https://docs.microsoft.com/zh-tw/dotnet/api/system.security.cryptography.sha256?view=netframework-4.8


        //SHA384演算法雜湊大小是384位元 : https://docs.microsoft.com/zh-tw/dotnet/api/system.security.cryptography.sha384?view=netframework-4.8


        //SHA512演算法雜湊大小為512位元 : https://docs.microsoft.com/zh-tw/dotnet/api/system.security.cryptography.sha512?view=netframework-4.8
        public string SHA512Hash(string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            StringBuilder sb;

            using (SHA512 sha512 = SHA512.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(rawString);

                byte[] encryption = sha512.ComputeHash(byteArray);


                sb = new StringBuilder();

                //for (int i = 0; i < encryption.Length; i++)
                //{
                //    sb.Append(encryption[i].ToString("x2"));
                //}

                foreach (byte bt in encryption)
                {
                    sb.Append(bt.ToString("x2"));
                }
            }

            return sb.ToString(); ;
        }
    }
}
