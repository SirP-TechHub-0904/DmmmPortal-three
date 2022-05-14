using DmmmPortal.Models;
using DmmmPortal.Models.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Data.Entity;

namespace DmmmPortal.Areas.AdminPanel.Controllers
{
    [Authorize]
    public class PortalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminPanel/Portal
        public async Task<ActionResult> Index()
        {
            var items = db.Subsections.Include(x => x.Section).Where(x => x.Section.Name == "Schools").OrderBy(x => x.NameOfSection);

            ViewBag.TotalSchool = items.Count();
            return View();
        }

        public async Task<ActionResult> EducationDashboard()
        {
            var items = db.Subsections.Include(x => x.Section).Where(x => x.Section.Name == "Schools").OrderBy(x => x.NameOfSection);
            int totalStudent = 0;
            int totalEnrolled = 0;
            int totalCards = 0;
            int totalUsedCards = 0;
            int totalUnCards = 0;
            int totalClass = 0;
            int totalStaff = 0;
            foreach (var a in items)
            {
                try
                {
  string portal = a.PortalUrl;
                WebRequest request = WebRequest.Create(
           portal + "/api/SchoolApi/GetSchool?url=" + portal);
                // If required by the server, set the credentials.  
                request.Credentials = CredentialCache.DefaultCredentials;
                // Get the response.  
                WebResponse response = request.GetResponse();
                // Display the status.  
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.  
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();

                JavaScriptSerializer oJS = new JavaScriptSerializer();
                OutputDetailsDto item = new OutputDetailsDto();
                item = oJS.Deserialize<OutputDetailsDto>(responseFromServer);
                totalStudent += Convert.ToInt32(item.UnEnrolStudentsCount);
                totalEnrolled += Convert.ToInt32(item.EnrolStudentsCount);
                totalCards += Convert.ToInt32(item.Totalcard);
                totalUnCards += Convert.ToInt32(item.NonUsedcard);
                totalUsedCards += Convert.ToInt32(item.Usedcard);
                totalClass += Convert.ToInt32(item.ClassCount);
                totalStaff += Convert.ToInt32(item.TotalStaff);

                }catch(Exception e)
                {

                }
              
                
            }

            ViewBag.totalStudent = totalStudent;
            ViewBag.totalEnrolled = totalEnrolled;
            ViewBag.totalCards = totalCards;
            ViewBag.totalUsedCards = totalUsedCards;
            ViewBag.totalUnCards = totalUnCards;
            ViewBag.totalClass = totalClass;
            ViewBag.totalStaff = totalStaff;

            return View(items);
        }
    }
}