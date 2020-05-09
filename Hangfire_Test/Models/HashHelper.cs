using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Hangfire_Test.Models
{
    /// <summary>
    /// 哈希 帮助类
    /// </summary>
    public class HashHelper
    {
        public static byte[] ToSHA1(string Value)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(Value));
                string result = "new byte[] { " + string.Join(",", hash.Select(x => "0x" + x.ToString("x2")).ToArray()) + " } ";
                return hash;
            }
        }
    }
}