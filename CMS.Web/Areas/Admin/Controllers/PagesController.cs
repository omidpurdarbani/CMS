﻿using CMS.Core.Generator;
using CMS.Core.Repositories;
using CMS.DataLayer;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [RoutePrefix("admin/pages")]
    public class PagesController : Controller
    {
        IPageService _pageService;
        IPageGroupService _pageGroupService;
        CmsContext _context = new CmsContext();

        public PagesController()
        {
            _pageService = new PageService(_context);
            _pageGroupService = new PageGroupService(_context);
        }

        // GET: Admin/Pages?sort=visit
        public ActionResult Index(string sort)
        {
            var pages = _pageService.GetAllPages();

            if (sort == "visit")
            {
                return View(pages.OrderByDescending(p => p.Visit).ToList());
            }

            return View(pages.ToList().OrderByDescending(p => p.CreateDate));

        }


        // GET: Admin/Pages/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(_pageGroupService.GetAllGroups(), "GroupID", "GroupTitle");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageID,GroupID,Title,ShortDescription,Text,Visit,ImageName,ShowInSlider,CreateDate,Tags")] Page page, HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {

                page.Visit = 0;
                page.CreateDate = DateTime.Now;

                if (imgUp != null)
                {
                    page.ImageName = TextGenerator.GenerateUniqCode() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/PageImage/") + page.ImageName);
                }

                _pageService.InsertPage(page);
                _pageService.Save();

                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(_pageGroupService.GetAllGroups(), "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // GET: Admin/Pages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = _pageService.GetPageById(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }

            return PartialView(page);
        }

        // GET: Admin/Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var page = _pageService.GetPageById(id.Value);

            if (page == null)
            {
                return HttpNotFound();
            }

            ViewBag.GroupID = new SelectList(_pageGroupService.GetAllGroups(), "GroupID", "GroupTitle", page.GroupID);

            return PartialView(page);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageID,GroupID,Title,ShortDescription,Text,ShowInSlider,CreateDate,Tags")] Page page, HttpPostedFileBase imgUp)
        {

            if (ModelState.IsValid)
            {

                var oldPage = _pageService.GetPageById(page.PageID);

                if (oldPage != null)
                {
                    page.ImageName = oldPage.ImageName;

                    if (imgUp != null)
                    {
                        if (page.ImageName != null)
                        {
                            System.IO.File.Delete(Server.MapPath("/PageImage/") + page.ImageName);
                        }

                        page.ImageName = TextGenerator.GenerateUniqCode() + Path.GetExtension(imgUp.FileName);
                        imgUp.SaveAs(Server.MapPath("/PageImage/") + page.ImageName);
                    }

                    page.CreateDate = oldPage.CreateDate;

                    _pageService.UpdatePage(page);
                    _pageService.Save();

                    return RedirectToAction("Index");
                }
            }

            ViewBag.GroupID = new SelectList(_pageGroupService.GetAllGroups(), "GroupID", "GroupTitle", page.GroupID);

            return View(page);
        }

        // GET: Admin/Pages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = _pageService.GetPageById(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }
            return PartialView(page);
        }

        // POST: Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var page = _pageService.GetPageById(id);
            System.IO.File.Delete((Server.MapPath("/PageImage/") + page.ImageName));

            _pageService.DeletePage(page);
            _pageService.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pageGroupService.Dispose();
                _pageService.Dispose();
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
