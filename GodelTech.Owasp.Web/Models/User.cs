using System;

namespace GodelTech.Owasp.Web.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        
        /// <summary>
        /// A3 - Sensitive Data Exposure - Critical vulnerability, this logs out the user's password as a string!
        /// </summary>
        /// <returns>The string value of all the properties on the user.</returns>
        public override string ToString()
        {
            return
                $"Id: {Id} Name: {Name}  Email: {Email} RegistrationDate: {RegistrationDate} Password: {Password}";
        }

        /// <summary>
        /// A3 - Sensitive Data Exposure - This safely doesn't log the password out when called.
        /// </summary>
        /// <returns>A string representation of all the properties except password.</returns>
        public string SafeToString()
        {
            return
                $"Id: {Id}  Name: {Name}  Email: {Email} RegistrationDate: {RegistrationDate} Password: ******";

        }
    }
}