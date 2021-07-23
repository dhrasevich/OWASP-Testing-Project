using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GodelTech.Owasp.Web.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class, new()
    {
        Task<T> GetSingleFromSqlRaw(string sql);
        Task<IQueryable<T>> GetAllFromSqlRaw(string sql);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task<T> Add(T entity);
    }
}