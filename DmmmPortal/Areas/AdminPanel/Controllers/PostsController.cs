using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DmmmPortal.Models;
using DmmmPortal.Models.Entities;
using DmmmPortal.Areas.DataService.Services;
using DmmmPortal.Areas.Data.IServices;
using PagedList;

namespace DmmmPortal.Areas.AdminPanel.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IPostService _PostServices = new PostService();

        public PostsController()
        { }
        public PostsController(PostService postServices)
        {
            _PostServices = postServices;
        }
        // GET: Admin/Posts
        public async Task<ActionResult> Index(string searchString, string currentFilter, int? page)
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
            var items = await _PostServices.Posts(searchString, currentFilter, page);
            
            int pageSize = 20;
            int pageNumber = (page ?? 1);
           
            return View(items.ToPagedList(pageNumber, pageSize));

        }

        // GET: Admin/Posts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = await _PostServices.Get(id);
            var img = db.ImageModel.Where(x => x.PostId == id);
            ViewBag.images = img.ToList();
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Post models, IEnumerable<HttpPostedFileBase> upload)
        {
            if (ModelState.IsValid)
            {
                await _PostServices.New(models, upload);
                return RedirectToAction("Index");
            }

            return View(models);
        }

        // GET: Admin/Posts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var list = db.ImageModel.Where(x => x.PostId == id).ToList();
            ViewBag.PostImage = list;
            var post = await _PostServices.Get(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Post models, IEnumerable<HttpPostedFileBase> upload)
        {
            if (ModelState.IsValid)
            {
                await _PostServices.Edit(models, upload);
                return RedirectToAction("Index");
            }
            return View(models);
        }

        public async Task<ActionResult> Publish(int id)
        {
            await _PostServices.PublishUnpublish(id);
            return RedirectToAction("Posts");
        }
        public async Task<ActionResult> PostCorfirm(int id)
        {
            await _PostServices.PublishUnpublish(id);
            return RedirectToAction("Posts");
        }

        // GET: Admin/Posts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = await _PostServices.Get(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _PostServices.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
