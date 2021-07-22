using System.Collections.Generic;
using GodelTech.Owasp.Web.Models;

namespace GodelTech.Owasp.Web.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        public IEnumerable<Genre> Get();
    }
}