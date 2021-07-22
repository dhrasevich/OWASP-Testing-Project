using System.Collections.Generic;
using GodelTech.Owasp.Web.Models;

namespace GodelTech.Owasp.Web.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public User Get(string email, string password);
        public User GetById(int id);
    }
}