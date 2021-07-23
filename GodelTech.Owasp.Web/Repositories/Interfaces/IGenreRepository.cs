using System.Collections.Generic;
using GodelTech.Owasp.Web.Models;

namespace GodelTech.Owasp.Web.Repositories.Interfaces
{
    public interface IGenreRepository : IBaseRepository<Genre>
    {
        public IEnumerable<Genre> Get();
    }
}