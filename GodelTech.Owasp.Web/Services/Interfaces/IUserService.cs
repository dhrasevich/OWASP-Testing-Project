using System.Threading.Tasks;
using GodelTech.Owasp.Web.Models;

namespace GodelTech.Owasp.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    }
}