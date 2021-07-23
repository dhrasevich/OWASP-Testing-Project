using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GodelTech.Owasp.Web.Data;
using GodelTech.Owasp.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GodelTech.Owasp.Web.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        private readonly OwaspContext _context;

        protected BaseRepository(OwaspContext context)
        {
            _context = context;
        }

        public Task<T> GetSingleFromSqlRaw(string sql)
        {
            var result = _context.Set<T>().FromSqlRaw(sql);
            return Task.FromResult(result.FirstOrDefault());
        }

        public Task<IQueryable<T>> GetAllFromSqlRaw(string sql)
        {
            return Task.FromResult(_context.Set<T>().FromSqlRaw(sql));
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        //
        // protected IEnumerable<T> GetRecords(IDataReader reader)
        // {
        //     var records = new List<T>();
        //
        //     while (reader.Read())
        //     {
        //         var record = BuildRecord(reader);
        //         records.Add(record);
        //     }
        //
        //     return records;
        // }

        // protected abstract T BuildRecord(IDataReader reader);
        //
        // protected static TF GetFieldValue<TF>(IDataReader reader, string columnName, TF defaultValue = default(TF))
        // {
        //     var obj = reader[columnName];
        //
        //     if (obj == null || obj == DBNull.Value)
        //         return defaultValue;
        //
        //     return (TF)obj;
        // }
    }
}