using CMS.Core.Repositories;
using CMS.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CMS.Web.Controllers
{
    public class PagesController : Controller
    {
        CmsContext _context = new CmsContext();
        private IPageGroupService _pageGroupService;
        private IPageService _pageService;
        private IPageCommentService _pageCommentService;

        public PagesController()
        {
            _pageGroupService = new PageGroupService(_context);
            _pageService = new PageService(_context);
            _pageCommentService = new PageCommentService(_context);
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

            var id = HttpContext.Request.QueryString["pageid"];
            if (id != null)
            {
                return View("ShowPage", _pageService.GetPageById(Convert.ToInt32(id)));
            }

            var categories = HttpContext.Request.QueryString["categories"];
            if (categories == null)
            {
                List<Page> modelPage;
                var page = Convert.ToInt32(HttpContext.Request.QueryString["page"]);
                if (page != 0)
                {
                    modelPage = _pageService.GetAllPages(paging: true, skip: page).ToList();
                    ViewBag.pageID = page;
                }
                else
                {
                    modelPage = _pageService.GetAllPages(paging: true).ToList();
                    ViewBag.pageID = 1;
                }

                ViewBag.count = _pageService.GetAllPages().ToList().Count;
                return View(modelPage);
            }
            else
            {
                var model = _pageGroupService.GetGroupById(Convert.ToInt32(categories));
                ViewBag.CategoryTitle = model.GroupTitle;
                ViewBag.CategoryID = model.GroupID;

                var modelPage = new List<Page>();
                var page = HttpContext.Request.QueryString["page"];
                if (page != null)
                {
                    modelPage = _pageService.GetAllPages(paging: true, skip: Convert.ToInt32(page)).Where(p => p.GroupID == model.GroupID).ToList();
                    ViewBag.pageID = page;
                }
                else
                {
                    modelPage = _pageService.GetAllPages(paging: true).Where(p => p.GroupID == model.GroupID).ToList();
                    ViewBag.pageID = 1;
                }

                ViewBag.count = _pageService.GetAllPages().ToList().Count;
                return View(modelPage);
            }

        }

        [HttpPost]
        public ActionResult AddComment(int pageId, string name, string email, string comment)
        {
            PageComment pageComment = new PageComment()
            {
                CreateDate = DateTime.Now,
                Comment = comment,
                PageID = pageId,
                Email = email,
                Name = name
            };
            try
            {
                _pageCommentService.InsertComment(pageComment);
                return PartialView("ShowComment", _pageCommentService.GetAllCommentsOfPage(pageId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        public ActionResult ShowComment(int pageid)
        {
            return PartialView(_pageCommentService.GetAllCommentsOfPage(pageid));
        }

    }
}