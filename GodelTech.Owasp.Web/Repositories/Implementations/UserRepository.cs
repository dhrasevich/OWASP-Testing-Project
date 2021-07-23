using System;
using System.Data;
using System.Threading.Tasks;
using GodelTech.Owasp.Web.Data;
using GodelTech.Owasp.Web.Helpers;
using GodelTech.Owasp.Web.Models;
using GodelTech.Owasp.Web.Repositories.Interfaces;

namespace GodelTech.Owasp.Web.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(OwaspContext context) : base(context)
        {
        }

        public async Task<User> Get(string email, string password)
        {
            var sql = $"SELECT * FROM [User] WHERE Email = '{email}' AND Password = '{HashGenerator.CalculateMd5Hash(password)}'";

            return await GetSingleFromSqlRaw(sql);
        }
        //
        // protected override User BuildRecord(IDataReader reader)
        // {
        //     return new User
        //     {
        //         Id = GetFieldValue<int>(reader, nameof(User.Id)),
        //         Email= GetFieldValue<string>(reader, nameof(User.Email)),
        //         Password = GetFieldValue<string>(reader, nameof(User.Password)),
        //         Name = GetFieldValue<string>(reader, nameof(User.Name)),
        //         RegistrationDate = GetFieldValue<DateTime>(reader, nameof(User.RegistrationDate))
        //     };
        // }
    }
}