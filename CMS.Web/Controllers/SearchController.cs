using CMS.Core.Repositories;
using CMS.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CMS.Web.Controllers
{
    public class SearchController : Controller
    {
        private IPageService pageService;
        private CmsContext _context;

        public SearchController()
        {
            _context = new CmsContext();
            pageService = new PageService(_context);
        }
        // GET: Search
        [Route("search")]
        public ActionResult Index(string q)
        {
            if (q == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var pageId = Convert.ToInt32(HttpContext.Request.QueryString["page"]);
            List<Page> pages;
            if (pageId == 0)
            {
                pages = pageService.SearchPagesByAll(q, true).ToList();
                ViewBag.pageID = 1;
            }
            else
            {
                pages = pageService.SearchPagesByAll(q, true, pageId).ToList();
                ViewBag.pageID = pageId;
            }

            ViewBag.search = q;
            ViewBag.count = pageService.SearchPagesByAll(q).ToList().Count;
            return View(pages);
        }
    }
}