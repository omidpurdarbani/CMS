using CMS.Core.Repositories;
using CMS.DataLayer;
using System.Net;
using System.Web.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class PageGroupsController : Controller
    {
        private IPageGroupService _pageGroupService;
        CmsContext _context = new CmsContext();
        public PageGroupsController()
        {
            _pageGroupService = new PageGroupService(_context);
        }

        // GET: Admin/PageGroups
        public ActionResult Index()
        {
            return View(_pageGroupService.GetAllGroups());
        }

        // GET: Admin/PageGroups/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupTitle")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {
                _pageGroupService.InsertGroup(pageGroup);
                _pageGroupService.Save();

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

            PageGroup pageGroup = _pageGroupService.GetGroupById(id.Value);

            if (pageGroup == null)
            {
                return HttpNotFound();
            }
            return PartialView(pageGroup);
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
                _pageGroupService.UpdateGroup(pageGroup);
                _pageGroupService.Save();

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

            PageGroup pageGroup = _pageGroupService.GetGroupById(id.Value);

            if (pageGroup == null)
            {
                return HttpNotFound();
            }
            return PartialView(pageGroup);
        }

        // POST: Admin/PageGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _pageGroupService.GetGroupById(id);
            _pageGroupService.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pageGroupService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
