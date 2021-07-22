using System.Security.Cryptography;
using System.Text;

namespace GodelTech.Owasp.Web.Helpers
{
    /// <summary>
    /// Help tool to generate passwords and password hash.
    /// </summary>
    public static class HashGenerator
    {
        /// <summary>
        /// Calculates hash for password.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CalculateMd5Hash(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(i.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}