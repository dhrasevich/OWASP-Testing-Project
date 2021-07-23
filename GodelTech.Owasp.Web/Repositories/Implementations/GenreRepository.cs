using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GodelTech.Owasp.Web.Data;
using GodelTech.Owasp.Web.Models;
using GodelTech.Owasp.Web.Repositories.Interfaces;

namespace GodelTech.Owasp.Web.Repositories.Implementations
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(OwaspContext context) : base(context)
        {
        }
        //
        // public IEnumerable<Genre> Get()
        // {
        //     var sql = $"SELECT * FROM Genre";
        //
        //     using var connection = new SqlConnection(ConnectionString);
        //     using var cmd = new SqlCommand(sql, connection);
        //
        //     connection.Open();
        //
        //     var reader = cmd.ExecuteReader();
        //     var records = GetRecords(reader);
        //     return records;
        // }
        //
        // protected override Genre BuildRecord(IDataReader reader)
        // {
        //     return new Genre
        //     {
        //         GenreId = GetFieldValue<int>(reader, nameof(Genre.GenreId)),
        //         Name = GetFieldValue<string>(reader, nameof(Genre.Name)),
        //         Description = GetFieldValue<string>(reader, nameof(Genre.Description))
        //     };
        // }
        public IEnumerable<Genre> Get()
        {
            throw new System.NotImplementedException();
        }
    }
}