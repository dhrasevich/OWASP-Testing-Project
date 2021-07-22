using GodelTech.Owasp.Web.Models;

namespace GodelTech.Owasp.Web.Services.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}