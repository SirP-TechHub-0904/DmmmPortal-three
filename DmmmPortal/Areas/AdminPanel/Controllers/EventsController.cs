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

namespace DmmmPortal.Areas.AdminPanel.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminPanel/Events
        public async Task<ActionResult> Index()
        {
            return View(await db.Events.ToListAsync());
        }

        // GET: AdminPanel/Events/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event models = await db.Events.FindAsync(id);
            if (models == null)
            {
                return HttpNotFound();
            }
            return View(models);
        }

        // GET: AdminPanel/Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Event models)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   db.Events.Add(models);
                await db.SaveChangesAsync();
                    TempData["success"] = "Event Added";
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Something Went Wrong. Try again.";
                }
                
                return RedirectToAction("Index");
            }

            return View(models);
        }

        // GET: AdminPanel/Events/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event models = await db.Events.FindAsync(id);
            if (models == null)
            {
                return HttpNotFound();
            }
            return View(models);
        }

        // POST: AdminPanel/Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Subject,DIscription,Start,End,Color,GeneralEvent,IsFullDay")] Event models)
        {
            if (ModelState.IsValid)
            {
                db.Entry(models).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(models);
        }

        // GET: AdminPanel/Events/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event models = await db.Events.FindAsync(id);
            if (models == null)
            {
                return HttpNotFound();
            }
            return View(models);
        }

        // POST: AdminPanel/Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Event models = await db.Events.FindAsync(id);
            db.Events.Remove(models);
            await db.SaveChangesAsync();
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
