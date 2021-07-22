using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using GodelTech.Owasp.Web.Helpers;
using GodelTech.Owasp.Web.Models;
using GodelTech.Owasp.Web.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GodelTech.Owasp.Web.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public User Get(string email, string password)
        {
            var sql = $"SELECT * FROM [User] WHERE Email = '{email}' AND Password = '{HashGenerator.CalculateMd5Hash(password)}'";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            using var cmd = new SqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();
            var record = GetRecords(reader).SingleOrDefault();
            return record;
        }

        public User GetById(int id)
        {
            var sql = $"SELECT * FROM [User] WHERE Id = {id}";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            using var cmd = new SqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();
            var record = GetRecords(reader).SingleOrDefault();
            return record;
        }

        protected override User BuildRecord(IDataReader reader)
        {
            return new User
            {
                Id = GetFieldValue<int>(reader, nameof(User.Id)),
                Email= GetFieldValue<string>(reader, nameof(User.Email)),
                Password = GetFieldValue<string>(reader, nameof(User.Password)),
                Name = GetFieldValue<string>(reader, nameof(User.Name)),
                RegistrationDate = GetFieldValue<DateTime>(reader, nameof(User.RegistrationDate))
            };
        }
    }
}