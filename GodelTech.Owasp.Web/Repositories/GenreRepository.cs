using GodelTech.Owasp.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GodelTech.Owasp.Web.Repositories
{
    public class GenreRepository
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Owasp.MusicStore"].ConnectionString;

        public IEnumerable<Genre> Get()
        {
            var sql = $"SELECT * FROM Genre";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    var records = GetRecords(reader);
                    return records;
                }
            }
        }

        private static IEnumerable<Genre> GetRecords(IDataReader reader)
        {
            var records = new List<Genre>();

            while (reader.Read())
            {
                var record = BuildGenre(reader);
                records.Add(record);
            }

            return records;
        }

        private static Genre BuildGenre(IDataReader reader)
        {
            return new Genre
            {
                GenreId = GetFieldValue<int>(reader, nameof(Genre.GenreId)),
                Name = GetFieldValue<string>(reader, nameof(Genre.Name)),
                Description = GetFieldValue<string>(reader, nameof(Genre.Description))
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