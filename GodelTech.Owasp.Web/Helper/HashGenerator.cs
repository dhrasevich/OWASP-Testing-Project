using System.Security.Cryptography;
using System.Text;

namespace GodelTech.Owasp.Web.Helper
{
    /// <summary>
    /// Help tool to generage passwords and password hash.
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
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(i.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}