using System.Collections.Generic;
using GodelTech.Owasp.Web.Models;

namespace GodelTech.Owasp.Web.Repositories.Interfaces
{
    public interface IAlbumRepository
    {
        public Album Get(string id);
        public IEnumerable<Album> GetList(string searchKey);
        public IEnumerable<Album> GetList(int genreId);
        public IEnumerable<Album> GetList(int skip, int take);
        public int AddIfNotExist(IEnumerable<Album> albums);
    }
}