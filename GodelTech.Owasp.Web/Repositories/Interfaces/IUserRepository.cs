using System.Threading.Tasks;
using GodelTech.Owasp.Web.Models;

namespace GodelTech.Owasp.Web.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User> Get(string email, string password);
    }
}