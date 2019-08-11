using Owasp.Sqli.Repositories;
using System.Web.Mvc;

namespace GodelTech.Owasp.Web.Controllers
{
    public class AlbumsController : Controller
    {
        AlbumRepository repository;

        public AlbumsController()
        {
            repository = new AlbumRepository();
        }

        public ActionResult Details(string id)
        {
            var repository = new AlbumRepository();
            var model = repository.Get(id);
            return View(model);
        }

        public ActionResult List(string searchKey)
        {
            var model = repository.GetList(searchKey);
            return View(model);
        }
    }
}