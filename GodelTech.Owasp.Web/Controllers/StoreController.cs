using GodelTech.Owasp.Web.Repositories;
using System.Web.Mvc;

namespace GodelTech.Owasp.Web.Controllers
{
    public class StoreController : Controller
    {
        AlbumRepository repository;

        public StoreController()
        {
            repository = new AlbumRepository();
        }

        public ActionResult Album(string id)
        {
            var repository = new AlbumRepository();
            var model = repository.Get(id);
            return View(model);
        }

        public ActionResult Search(string searchKey)
        {
            var model = repository.GetList(searchKey);
            return View("Albums", model);
        }

        public ActionResult Albums(int id)
        {
            var repository = new AlbumRepository();
            var model = repository.GetList(id);
            return View(model);
        }
    }
}