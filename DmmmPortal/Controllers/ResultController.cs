using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DmmmPortal.Models;

namespace DmmmPortal.Controllers
{
    public class ResultController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Result
        public ActionResult CheckResult(string url)
        {

            ViewBag.School = new SelectList(db.SchoolPortalDatas.Where(x=> !string.IsNullOrEmpty(x.SchoolName)).OrderByDescending(x=>x.SchoolName).ThenBy(x=>x.Id), "WebUrl", "SchoolName");
            var sch = db.SchoolPortalDatas.FirstOrDefault(x => x.WebUrl == url);
            if(sch != null)
            {
    string link = sch.WebUrl;
            ViewBag.link = link;
            }
            if (string.IsNullOrEmpty(url))
            {
                sch = null;
            }
            return View(sch);
        }
    }
}