using CMS.Core.Repositories;
using CMS.DataLayer;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CMS.Web.Controllers
{
    public class PagesController : Controller
    {
        CmsContext _context = new CmsContext();
        private IPageGroupService _pageGroupService;
        private IPageService _pageService;

        public PagesController()
        {
            _pageGroupService = new PageGroupService(_context);
            _pageService = new PageService(_context);
        }

        [ChildActionOnly]
        // GET: News
        public ActionResult ShowGroups()
        {
            return PartialView(_pageGroupService.GetGroupsForView().ToList());
        }

        [ChildActionOnly]
        // GET: News
        public ActionResult ShowGroupsInMenu()
        {
            return PartialView(_pageGroupService.GetGroupsInMenuForView().ToList());
        }

        [ChildActionOnly]
        // GET: TopNews
        public ActionResult TopPages()
        {
            return PartialView(_pageService.GetTopPages().ToList());
        }


        public ActionResult Index()
        {
            var categories = HttpContext.Request.QueryString["categories"];
            if (categories == null)
            {
                return View(_pageService.GetAllPages().ToList());
            }
            else
            {
                var model = _pageGroupService.GetGroupById(Convert.ToInt32(categories));
                ViewBag.CategoryTitle = model.GroupTitle;
                return View(_pageService.GetAllPages().Where(p => p.GroupID == model.GroupID).ToList());
            }

        }

        [Route("pages/{id}")]
        public ActionResult Index(int id)
        {
            return View("ShowPage", _pageService.GetPageById(id));
        }
    }
}