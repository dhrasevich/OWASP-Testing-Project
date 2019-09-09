using GodelTech.Owasp.Web.Repositories;
using System.Web.Mvc;

namespace GodelTech.Owasp.Web.Controllers
{
    public class GenreController : Controller
    {
        public ActionResult Menu()
        {
            var repository = new GenreRepository();
            var model = repository.Get();
            return PartialView(model);
        }
    }
}