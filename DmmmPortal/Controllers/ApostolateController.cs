using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DmmmPortal.Models;
using System.Data.Entity;

namespace DmmmPortal.Controllers
{
    public class ApostolateController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Apostolate
       

        public ActionResult Hospitals()
        {
            var items = db.Subsections.Include(x=>x.Section).Where(x => x.Section.Name == "Hospitals").OrderBy(x=>x.NameOfSection);
            return View(items.ToList());
        }

        public ActionResult Education()
        {
            var items = db.Subsections.Include(x=>x.Section).Where(x => x.Section.Name == "Schools").OrderBy(x=>x.NameOfSection);
           
            return View(items.ToList());
        }

        public ActionResult Spirituality()
        {
            var items = db.Subsections.Include(x=>x.Section).Where(x => x.Section.Name == "Spirituality").OrderBy(x=>x.NameOfSection);
            return View(items.ToList());
        }

        public ActionResult Orphanage()
        {
            var items = db.Subsections.Include(x=>x.Section).Where(x => x.Section.Name == "Orphanage").OrderBy(x=>x.NameOfSection);
            return View(items.ToList());
        }

        public ActionResult Farming()
        {
            var items = db.Subsections.Include(x=>x.Section).Where(x => x.Section.Name == "Farming").OrderBy(x=>x.NameOfSection);
            return View(items.ToList());
        }

        public ActionResult GenerateAndConference()
        {
            var items = db.Subsections.Include(x=>x.Section).Where(x => x.Section.Name == "GenerateAndConference").OrderBy(x=>x.NameOfSection);
            return View(items.ToList());
        }

        public ActionResult Banking()
        {
            var items = db.Subsections.Include(x=>x.Section).Where(x => x.Section.Name == "Banking").OrderBy(x=>x.NameOfSection);
            return View(items.ToList());
        }

        public ActionResult Caregiving()
        {
            var items = db.Subsections.Include(x=>x.Section).Where(x => x.Section.Name == "Caregiving").OrderBy(x=>x.NameOfSection);
            return View(items.ToList());
        }
    }
}