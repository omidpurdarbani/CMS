using CMS.Core.DTOs;
using CMS.Core.Services.Interfaces;
using CMS.Core.Services.Services;
using CMS.DataLayer;
using System.Web.Mvc;
using System.Web.Security;

namespace CMS.Web.Controllers
{
    public class AccountController : Controller
    {
        private ILoginService _loginService;
        private CmsContext _context;

        public AccountController()
        {
            _context = new CmsContext();
            _loginService = new LoginService(_context);
        }

        [Route("login")]
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_loginService.IsExistUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                var returnPath = HttpContext.Request.QueryString["ReturnUrl"];
                if (returnPath != null)
                {
                    return Redirect(returnPath);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("UserName", "کاربری یافت نشد");
            return View(model);
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}