using CMS.Core.Repositories;
using CMS.DataLayer;
using System.Net;
using System.Web.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    public class PageGroupsController : Controller
    {
        private IPageGroupService pageGroupService;

        public PageGroupsController()
        {
            pageGroupService = new PageGroupService();
        }

        // GET: Admin/PageGroups
        public ActionResult Index()
        {
            return View(pageGroupService.GetAllGroups());
        }

        // GET: Admin/PageGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PageGroup pageGroup = pageGroupService.GetGroupById(id.Value);

            if (pageGroup == null)
            {
                return HttpNotFound();
            }

            return View(pageGroup);
        }

        // GET: Admin/PageGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupTitle")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {
                pageGroupService.InsertGroup(pageGroup);
                pageGroupService.Save();

                return RedirectToAction("Index");
            }

            return View(pageGroup);
        }

        // GET: Admin/PageGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PageGroup pageGroup = pageGroupService.GetGroupById(id.Value);

            if (pageGroup == null)
            {
                return HttpNotFound();
            }
            return View(pageGroup);
        }

        // POST: Admin/PageGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,GroupTitle")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {
                pageGroupService.UpdateGroup(pageGroup);
                pageGroupService.Save();

                return RedirectToAction("Index");
            }
            return View(pageGroup);
        }

        // GET: Admin/PageGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PageGroup pageGroup = pageGroupService.GetGroupById(id.Value);

            if (pageGroup == null)
            {
                return HttpNotFound();
            }
            return View(pageGroup);
        }

        // POST: Admin/PageGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pageGroupService.GetGroupById(id);
            pageGroupService.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pageGroupService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
