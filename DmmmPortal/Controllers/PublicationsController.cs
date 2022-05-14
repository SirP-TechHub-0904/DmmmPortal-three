using DmmmPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using PagedList;
using System.Net;
using DmmmPortal.Models.Entities;

namespace DmmmPortal.Controllers
{
    public class PublicationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Publications
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult News(string searchString, string currentFilter, int? page)
        {

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var items = db.Posts.Include(x => x.ImageModels).Include(d => d.Comments);

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(p => p.Title.ToUpper().Contains(searchString.ToUpper())
                    || p.Content.ToUpper().Contains(searchString.ToUpper()));
                    
            }
            items = items.OrderBy(x => x.DatePosted);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            

            var calendar = db.Events.OrderBy(x => x.Start).Take(9).ToList();
            ViewBag.events = calendar;
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Preview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = db.Posts.Include(x => x.ImageModels).Include(x=>x.Comments).FirstOrDefault(x => x.Id == id);
            var img = db.ImageModel.Where(x => x.PostId == id);
            var img1 = db.ImageModel.Where(x => x.PostId == id).FirstOrDefault().ImageContent;
            ViewBag.firstimage = img1;
            ViewBag.img = img;
           
            if (post == null)
            {
                return HttpNotFound();
            }

            var calendar = db.Events.OrderBy(x => x.Start).Take(9).ToList();
            ViewBag.events = calendar;
            return View(post);
        }

        public ActionResult Comment(int id, string text, string name, string Email)
        {
            var post = db.Posts.FirstOrDefault(x => x.Id == id);
            if(post != null)
            {
                Comment com = new Comment();
                com.Name = name;
                com.DateCommented = DateTime.UtcNow.AddHours(1);
                com.Email = Email;
                com.Content = text;
                com.PostId = id;
                db.Comments.Add(com);
                db.SaveChanges();

            }
            return RedirectToAction("Preview", new { id = id });
        }

        public ActionResult Videos()
        {
            return View();
        }

        public ActionResult Projects()
        {
            return View();
        }
    }
}