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
using DmmmPortal.Areas.DataService.IServices;
using DmmmPortal.Areas.DataService.Services;

namespace DmmmPortal.Areas.AdminPanel.Controllers
{
    [Authorize]
    public class SectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ISectionService _sectionService = new SectionService();


        public SectionsController()
        {

        }
        public SectionsController(SectionService sectionService)
        {
            _sectionService = sectionService;
        }
        // GET: AdminPanel/Sections
        public async Task<ActionResult> Index()
        {
            var items = await _sectionService.List();
            return View(items);
        }

        public async Task<ActionResult> Subsectionbysection(int id)
        {
            var items = await _sectionService.ListSubSectionBySectionId(id);
            var sectionName = await _sectionService.Get(id);
            ViewBag.sectionName = sectionName.Name;
            return View(items);
        }

        // GET: AdminPanel/Sections/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: AdminPanel/Sections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Section section)
        {
            if (ModelState.IsValid)
            {
                await _sectionService.Create(section);
                return RedirectToAction("Index");
            }

            return View(section);
        }

        // GET: AdminPanel/Sections/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var section = await _sectionService.Get(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: AdminPanel/Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Section section)
        {
            if (ModelState.IsValid)
            {
                await _sectionService.Edit(section);
                return RedirectToAction("Index");
            }
            return View(section);
        }

        // GET: AdminPanel/Sections/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var section = await _sectionService.Get(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: AdminPanel/Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _sectionService.Delete(id);
            return RedirectToAction("Index");
        }

        #region /*Subsection region*/

        // GET: AdminPanel/Sections
        public async Task<ActionResult> SubsectionIndex()
        {
            var items = await _sectionService.ListSubSection();
            return View(items);
        }

        // GET: AdminPanel/Sections/Details/5
        public async Task<ActionResult> SubsectionDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var section = await _sectionService.GetSubSection(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: AdminPanel/Sections/Create
        public ActionResult SubsectionCreate()
        {
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Name");
            return View();
        }

        // POST: AdminPanel/Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubsectionCreate(Subsection items, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                await _sectionService.CreateSubSection(items, upload);
                return RedirectToAction("Index");
            }
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Name", items.SectionId);
            return View(items);
        }

        // GET: AdminPanel/Sections/Edit/5
        public async Task<ActionResult> SubsectionEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = await _sectionService.GetSubSection(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Name", item.SectionId);
            return View(item);
        }

        // POST: AdminPanel/Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubsectionEdit(Subsection item, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                await _sectionService.EditSubSection(item, upload);
                return RedirectToAction("Index");
            }
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Name", item.SectionId);
            return View(item);
        }

        // GET: AdminPanel/Sections/Delete/5
        public async Task<ActionResult> SubsectionDelete(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = await _sectionService.GetSubSection(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: AdminPanel/Sections/Delete/5
        [HttpPost, ActionName("SubsectionDeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubsectionDeleteConfirmed(int id)
        {
            await _sectionService.DeleteSubSection(id);
            return RedirectToAction("Index");
        }
        #endregion
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
