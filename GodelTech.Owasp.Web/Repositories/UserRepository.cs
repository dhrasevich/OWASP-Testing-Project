using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using GodelTech.Owasp.Web.Helper;
using GodelTech.Owasp.Web.Models;

namespace GodelTech.Owasp.Web.Repositories
{
    public class UserRepository
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Owasp.MusicStore"].ConnectionString;

        public User Get(string email, string password)
        {
            var sql = $"SELECT * FROM [User] WHERE Email = '{email}' AND Password = '{HashGenerator.CalculateMd5Hash(password)}'";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var cmd = new SqlCommand(sql, connection))
                {
                    var reader = cmd.ExecuteReader();
                    var record = GetRecords(reader).SingleOrDefault();
                    return record;
                }
            }
        }

        private static IEnumerable<User> GetRecords(IDataReader reader)
        {
            var records = new List<User>();

            while (reader.Read())
            {
                var record = Build(reader);
                records.Add(record);
            }

            return records;
        }

        private static User Build(IDataReader reader)
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

        private static T GetFieldValue<T>(IDataReader reader, string columnName, T defaultValue = default(T))
        {
            var obj = reader[columnName];

            if (obj == null || obj == DBNull.Value)
                return defaultValue;

            return (T)obj;
        }
    }
}