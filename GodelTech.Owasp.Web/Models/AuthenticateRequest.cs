using System.ComponentModel.DataAnnotations;

namespace GodelTech.Owasp.Web.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}