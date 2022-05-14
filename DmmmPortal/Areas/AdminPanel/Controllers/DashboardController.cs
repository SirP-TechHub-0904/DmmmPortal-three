using DmmmPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace DmmmPortal.Areas.AdminPanel.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminPanel/Dashboard
        public async Task<ActionResult> Index()
        {
            var school = await db.SchoolPortalDatas.CountAsync();
            ViewBag.countschool = school;
            return View();
        }
    }
}