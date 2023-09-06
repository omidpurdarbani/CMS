﻿using System.Web.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class IndexController : Controller
    {
        // GET: Admin/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}