using CMS.Core.Repositories;
using CMS.DataLayer;
using System.Linq;
using System.Web.Mvc;

namespace CMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private IPageService _pageService;
        private CmsContext _context = new CmsContext();

        public HomeController()
        {
            _pageService = new PageService(_context);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Slider()
        {
            return PartialView(_pageService.PageForSlider().ToList());
        }

        [ChildActionOnly]
        public ActionResult LastPages()
        {
            return PartialView(_pageService.LastPages().ToList());
        }
    }
}