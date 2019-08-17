using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using GodelTech.Owasp.Web.Models.ViewModel;
using GodelTech.Owasp.Web.Repositories;

namespace GodelTech.Owasp.Web.Controllers
{
    public class AccountController : Controller
    {
        UserRepository userRepository;

        public AccountController()
        {
            userRepository = new UserRepository();
        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }

        //
        // POST: /Account/SignIn
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = userRepository.Get(model.Email, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
                
            FormsAuthentication.SetAuthCookie(user.Name, true);
            System.Web.HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            System.Web.HttpContext.Current.User = new GenericPrincipal(new FormsIdentity(FormsAuthentication.Decrypt(authCookie.Value)), null);

            return RedirectToAction("Index","Home");
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignOut()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            return RedirectToAction("Index", "Home");
        }
    }
};