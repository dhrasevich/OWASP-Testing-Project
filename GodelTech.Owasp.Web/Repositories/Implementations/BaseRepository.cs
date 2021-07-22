using System;
using System.Collections.Generic;
using System.Data;
using GodelTech.Owasp.Web.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GodelTech.Owasp.Web.Repositories.Implementations
{
    public abstract class BaseRepository<T> : IBaseRepository
    {
        protected readonly string ConnectionString;
        private IConfiguration Configuration { get; }

        protected BaseRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("owasp");
        }

        protected IEnumerable<T> GetRecords(IDataReader reader)
        {
            var records = new List<T>();

            while (reader.Read())
            {
                var record = BuildRecord(reader);
                records.Add(record);
            }

            return records;
        }

        protected abstract T BuildRecord(IDataReader reader);

        protected static TF GetFieldValue<TF>(IDataReader reader, string columnName, TF defaultValue = default(TF))
        {
            var obj = reader[columnName];

            if (obj == null || obj == DBNull.Value)
                return defaultValue;

            return (TF)obj;
        }
    }
}