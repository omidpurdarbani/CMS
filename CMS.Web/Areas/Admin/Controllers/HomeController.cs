using System.Web.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}