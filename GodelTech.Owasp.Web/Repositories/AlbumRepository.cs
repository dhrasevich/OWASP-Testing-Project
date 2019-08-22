using GodelTech.Owasp.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GodelTech.Owasp.Web.Repositories
{
    public class AlbumRepository
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Owasp.MusicStore"].ConnectionString;

        public Album Get(string id)
        {
            // id is straight from the URI
            var sql = "SELECT * FROM Album WHERE AlbumId = " + id;

            using (var connection = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    var record = GetRecords(reader).SingleOrDefault();
                    return record;
                }
            }
        }

        public IEnumerable<Album> GetList(string searchKey)
        {
            var sql = @"SELECT * FROM Album 
                        INNER JOIN Artist on Artist.ArtistId = Album.ArtistId
                        WHERE Title like '%" + searchKey + "%'";

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

        public IEnumerable<Album> GetList(int genreId)
        {
            var sql = @"SELECT * FROM Album
                        INNER JOIN Artist on Artist.ArtistId = Album.ArtistId
                        WHERE GenreId = @GenreId";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@GenreId", genreId);

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    var records = GetRecords(reader);
                    return records;
                }
            }
        }

        private static IEnumerable<Album> GetRecords(IDataReader reader)
        {
            var records = new List<Album>();

            while (reader.Read())
            {
                var record = BuildAlbum(reader);
                records.Add(record);
            }

            return records;
        }

        private static Album BuildAlbum(IDataReader reader)
        {
            return new Album
            {
                AlbumId = GetFieldValue<int>(reader, nameof(Album.AlbumId)),
                GenreId = GetFieldValue<int>(reader, nameof(Album.GenreId)),
                ArtistId = GetFieldValue<int>(reader, nameof(Album.ArtistId)),
                Title = GetFieldValue<string>(reader, nameof(Album.Title)),
                Price = GetFieldValue<decimal>(reader, nameof(Album.Price)),
                Artist = BuildArtist(reader)
            };
        }

        private static Artist BuildArtist(IDataReader reader)
        {
            return new Artist
            {
                ArtistId = GetFieldValue<int>(reader, nameof(Artist.ArtistId)),
                Name = GetFieldValue<string>(reader, nameof(Artist.Name))
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