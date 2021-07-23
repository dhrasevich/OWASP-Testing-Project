using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GodelTech.Owasp.Web.Data;
using GodelTech.Owasp.Web.Models;
using GodelTech.Owasp.Web.Repositories.Interfaces;

namespace GodelTech.Owasp.Web.Repositories.Implementations
{
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(OwaspContext context) : base(context)
        {
        }

        /// <summary>
        /// A1 - Injection - This is an example of a SQL Injection attack! OWASP #1 - DO NOT
        /// EVER USE THIS IN PRODUCTION CODE. In this case any arbitrary values entered
        /// by our user will get posted to SQL Exposing us to not only SQL Injection by Stored XSS.
        /// </summary>
        /// <param name="id">Album ID</param>
        /// <returns>Album record</returns>
        public async Task<Album> Get(string id)
        {
            // id is straight from the URI
            var sql = @"SELECT * FROM Album WHERE AlbumId = " + id;

            var record = await GetSingleFromSqlRaw(sql);

            // using var connection = new SqlConnection(ConnectionString);
            // using var cmd = new SqlCommand(sql, connection);
            //
            // connection.Open();
            //
            // var reader = cmd.ExecuteReader();
            // var record = GetRecords(reader).SingleOrDefault();
            return record;
        }

        public IEnumerable<Album> GetList(string searchKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Album> GetList(int genreId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Album> GetList(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public int AddIfNotExist(IEnumerable<Album> albums)
        {
            throw new NotImplementedException();
        }
        //
        // /// <summary>
        // /// A1 - Injection - Parameterized Queries.
        // /// </summary>
        // /// <param name="genreId">Genre ID</param>
        // /// <returns>List of Albums of selected Genre</returns>
        // public IEnumerable<Album> GetList(int genreId)
        // {
        //     const string sql = @"SELECT * FROM Album
        //                 INNER JOIN Artist on Artist.ArtistId = Album.ArtistId
        //                 WHERE GenreId = @GenreId";
        //
        //     using var connection = new SqlConnection(ConnectionString);
        //     using var cmd = new SqlCommand(sql, connection);
        //
        //     cmd.Parameters.AddWithValue("@GenreId", genreId);
        //     // OR
        //     // cmd.Parameters.Add(new SqlParameter(nameof(Album.GenreId), genreId));
        //
        //     connection.Open();
        //
        //     var reader = cmd.ExecuteReader();
        //     var records = GetRecords(reader);
        //     return records;
        // }
        //
        // public IEnumerable<Album> GetList(string searchKey)
        // {
        //     var sql = @"SELECT * FROM Album 
        //                 INNER JOIN Artist on Artist.ArtistId = Album.ArtistId
        //                 WHERE Title like '%" + searchKey + "%'";
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
        // public IEnumerable<Album> GetList(int skip, int take)
        // {
        //     const string sql = @"SELECT * FROM Album
        //                 INNER JOIN Artist on Artist.ArtistId = Album.ArtistId
        //                 ORDER BY CURRENT_TIMESTAMP
        //                 OFFSET @Skip ROWS
        //                 FETCH NEXT @Take ROWS ONLY";
        //
        //     using var connection = new SqlConnection(ConnectionString);
        //     using var cmd = new SqlCommand(sql, connection);
        //
        //     cmd.Parameters.AddWithValue("@Skip", skip);
        //     cmd.Parameters.AddWithValue("@Take", take);
        //
        //     connection.Open();
        //
        //     var reader = cmd.ExecuteReader();
        //     var records = GetRecords(reader);
        //     return records;
        // }
        //
        // public int AddIfNotExist(IEnumerable<Album> albums)
        // {
        //     var enumerable = albums.ToList();
        //
        //     if (albums == null || !enumerable.Any())
        //     {
        //         throw new ArgumentNullException(nameof(albums));
        //     }
        //
        //     var albumsValues = string.Join(",", enumerable.Select(x => $"({x.ArtistId}, {x.GenreId}, '{x.Title}', {x.Price}, '{x.AlbumArtUrl}')"));
        //
        //     var stringBuilder = new StringBuilder();
        //     stringBuilder.AppendLine("MERGE INTO Album AS Target");
        //     stringBuilder.AppendLine($"USING (VALUES {albumsValues})");
        //     stringBuilder.AppendLine("AS Source (GenreId, ArtistId, Title, Price, AlbumArtUrl)");
        //     stringBuilder.AppendLine("ON Target.GenreId = Source.GenreId");
        //     stringBuilder.AppendLine("AND Target.ArtistId = Source.ArtistId");
        //     stringBuilder.AppendLine("AND Target.Title = Source.Title");
        //     stringBuilder.AppendLine("AND Target.Price = Source.Price");
        //     stringBuilder.AppendLine("AND Target.AlbumArtUrl = Source.AlbumArtUrl");
        //     stringBuilder.AppendLine("WHEN NOT MATCHED BY TARGET THEN");
        //     stringBuilder.AppendLine("INSERT (GenreId, ArtistId, Title, Price, AlbumArtUrl)");
        //     stringBuilder.AppendLine("VALUES (source.GenreId, source.ArtistId, source.Title, source.Price, source.AlbumArtUrl);");
        //
        //     var sql = stringBuilder.ToString();
        //
        //     using var connection = new SqlConnection(ConnectionString);
        //     using var cmd = new SqlCommand(sql, connection);
        //
        //     connection.Open();
        //
        //     return cmd.ExecuteNonQuery();
        // }
        //
        // protected override Album BuildRecord(IDataReader reader)
        // {
        //     return new Album
        //     {
        //         AlbumId = GetFieldValue<int>(reader, nameof(Album.AlbumId)),
        //         GenreId = GetFieldValue<int>(reader, nameof(Album.GenreId)),
        //         ArtistId = GetFieldValue<int>(reader, nameof(Album.ArtistId)),
        //         Title = GetFieldValue<string>(reader, nameof(Album.Title)),
        //         Price = GetFieldValue<decimal>(reader, nameof(Album.Price)),
        //         Artist = BuildArtist(reader)
        //     };
        // }
        //
        // private static Artist BuildArtist(IDataReader reader)
        // {
        //     return new Artist
        //     {
        //         ArtistId = GetFieldValue<int>(reader, nameof(Artist.ArtistId)),
        //         Name = GetFieldValue<string>(reader, nameof(Artist.Name))
        //     };
        // }
    }
}