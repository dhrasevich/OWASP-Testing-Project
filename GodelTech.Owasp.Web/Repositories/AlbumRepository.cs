using GodelTech.Owasp.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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
            var sql = "SELECT * FROM Album WHERE name like '%" + searchKey + "%'";

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

        public int AddIfNotExist(IEnumerable<AlbumWithImage> albums)
        {
            if (albums == null || !albums.Any())
            {
                throw new ArgumentNullException(nameof(albums));
            }

            var albumsValues = string.Join(",", albums.Select(x => $"({x.ArtistId}, {x.GenreId}, {x.Title}, {x.Price}, {x.AlbumArtUrl})"));

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("MERGE INTO Album AS Target");
            stringBuilder.AppendLine($"USING (VALUES {albumsValues})");
            stringBuilder.AppendLine("AS Source (GenerId, ArtistId, Title, Price, AlbumArtUrl)");
            stringBuilder.AppendLine("ON Target.GenerId = Source.GenerId");
            stringBuilder.AppendLine("AND Target.ArtistId = Source.ArtistId");
            stringBuilder.AppendLine("AND Target.Title = Source.Title");
            stringBuilder.AppendLine("AND Target.Price = Source.Price");
            stringBuilder.AppendLine("AND Target.AlbumArtUrl = Source.AlbumArtUrl");
            stringBuilder.AppendLine("WHEN NOT MATCHED BY TARGET THEN");
            stringBuilder.AppendLine("INSERT (GenerId, ArtistId, Title, Price, AlbumArtUrl)");
            stringBuilder.AppendLine("VALUES (source.GenerId, source.ArtistId, source.Title, source.Price, source.AlbumArtUrl)");

            var sql = stringBuilder.ToString();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    return cmd.ExecuteNonQuery();
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