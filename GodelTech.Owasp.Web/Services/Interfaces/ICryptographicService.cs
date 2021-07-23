namespace GodelTech.Owasp.Web.Services.Interfaces
{
    public interface ICryptographicService
    {
        string HashPassword(string password, string salt);
    }
}