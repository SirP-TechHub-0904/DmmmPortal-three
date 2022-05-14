using DmmmPortal.Models;
using DmmmPortal.Models.Entities;
using DmmmPortal.Models.Entities.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DmmmPortal.Areas.DataService.Helper;

namespace DmmmPortal.Areas.AdminPanel.Controllers
{
    public class SchoolsController : Controller
    
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/SchoolPortalDatas

        public async Task<ActionResult> Index()
        {

            var sch = await db.SchoolPortalDatas.OrderByDescending(x => x.TotalResultsCount).ThenByDescending(x=>x.SchoolName).ToListAsync();
            return View(sch);
        }

        public JsonResult LgaList(string Id)
        {
            var schools = db.SchoolPortalDatas.OrderByDescending(x => x.SchoolName);

            return Json(schools, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> RefreshAll()
        {
            try
            {
                var portals = await db.SchoolPortalDatas.OrderByDescending(x => x.TotalResultsCount).ThenByDescending(x => x.SchoolName).ToListAsync();
                foreach (var item in portals)
                {
                    try
                    {
                        string endurl = ":80/api/SchoolApi/GetSchool";
                        string starturl = "http://";
                        string apiUrl = String.Format(starturl + item.PortalUrl + endurl);

                        WebRequest requestObj = WebRequest.Create(apiUrl);
                        requestObj.Method = "GET";

                        HttpWebResponse responseGet = null;
                        responseGet = (HttpWebResponse)requestObj.GetResponse();
                        string result = null;
                        
                        using (Stream stream = responseGet.GetResponseStream())
                        {
                            StreamReader sr = new StreamReader(stream);
                            result = sr.ReadToEnd();
                            SchoolPortalDataDto school = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<SchoolPortalDataDto>(result));
                            var model = await db.SchoolPortalDatas.FirstOrDefaultAsync(x => x.PortalUrl == item.PortalUrl);
                            //

                            var resultcount = await SchoolHelper.AvialableResult(school.PortalUrl, school.Session, school.Term);
                            //


                            model.ClassCount = school.ClassCount;
                            model.SiteName = school.SiteName;
                            model.SchoolName = school.SchoolName;
                            model.SchoolAddress = school.SchoolAddress;
                            model.SchoolCurrentPrincipal = school.SchoolCurrentPrincipal;
                            model.TotalResultsCount = resultcount;
                            model.EnrolStudentsCount = school.EnrolStudentsCount;
                            model.TotalStudentsCount = school.TotalStudentsCount;
                            model.UnEnrolStudentsCount = school.UnEnrolStudentsCount;
                            model.WebUrl = school.Url;
                            model.Usedcard = school.Usedcard;
                            model.NonUsedcard = school.NonUsedcard;
                            model.Totalcard = school.Totalcard;
                            model.TotalStaff = school.TotalStaff;
                            model.CurrentSession = school.CurrentSession;
                            model.LastModifiedDate = DateTime.UtcNow.AddHours(1);
                            model.ClassCount = school.ClassCount;
                            model.SchoolType = school.SchoolType;
                            model.Term = school.Term;
                            model.Session = school.Session;
                            model.BatchResultPrint = school.BatchPrint;
                            db.Entry(model).State = EntityState.Modified;
                            await db.SaveChangesAsync();

                            sr.Close();
                        }
                    }
                    catch (Exception c) { }
                }
            }

            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RefreshSingleSchool(string url)
        {
            try
            {
                var item = await db.SchoolPortalDatas.FirstOrDefaultAsync(x => x.PortalUrl == url);

                string endurl = ":80/api/SchoolApi/GetSchool";
                string starturl = "http://";
                string apiUrl = String.Format(starturl + item.PortalUrl + endurl);

                WebRequest requestObj = WebRequest.Create(apiUrl);
                requestObj.Method = "GET";

                HttpWebResponse responseGet = null;
                responseGet = (HttpWebResponse)requestObj.GetResponse();
                string result = null;
                using (Stream stream = responseGet.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                    SchoolPortalDataDto school = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<SchoolPortalDataDto>(result));
                    var model = await db.SchoolPortalDatas.FirstOrDefaultAsync(x => x.PortalUrl == item.PortalUrl);
                    //\
                    var resultcount = await SchoolHelper.AvialableResult(item.PortalUrl, school.Session, school.Term);
                    //

                    model.ClassCount = school.ClassCount;
                    model.SiteName = school.SiteName;
                    model.SchoolName = school.SchoolName;
                    model.SchoolAddress = school.SchoolAddress;
                    model.SchoolCurrentPrincipal = school.SchoolCurrentPrincipal;
                    model.TotalResultsCount = resultcount;
                    model.EnrolStudentsCount = school.EnrolStudentsCount;
                    model.TotalStudentsCount = school.TotalStudentsCount;
                    model.UnEnrolStudentsCount = school.UnEnrolStudentsCount;
                    model.WebUrl = school.Url;
                    model.Usedcard = school.Usedcard;
                    model.NonUsedcard = school.NonUsedcard;
                    model.Totalcard = school.Totalcard;
                    model.TotalStaff = school.TotalStaff;
                    model.CurrentSession = school.CurrentSession;
                    model.LastModifiedDate = DateTime.UtcNow.AddHours(1);
                    model.ClassCount = school.ClassCount;
                    model.SchoolType = school.SchoolType;
                    model.Term = school.Term;
                    model.Session = school.Session;
                    model.BatchResultPrint = school.BatchPrint;
                    db.Entry(model).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    sr.Close();
                }


            }

            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SchoolSessions(string url)
        {
            try
            {
                var item = await db.SchoolPortalDatas.FirstOrDefaultAsync(x => x.PortalUrl == url);
                ViewBag.schoolname = item.SchoolName;
                string endurl = "/api/SchoolApi/GetSchoolSessions";
                string starturl = "http://";
                string apiUrl = String.Format(starturl + item.PortalUrl + endurl);

                WebRequest requestObj = WebRequest.Create(apiUrl);
                requestObj.Method = "GET";

                HttpWebResponse responseGet = null;
                responseGet = (HttpWebResponse)requestObj.GetResponse();
                string result = null;
                List<ApiSessionList> schools = new List<ApiSessionList>();
                using (Stream stream = responseGet.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                    schools = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<ApiSessionList>>(result));

                    sr.Close();
                }
                ViewBag.url = url;
                return View(schools);
            }

            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SessionsDetails(string url, int sessionid)
        {
            try
            {
                var item = await db.SchoolPortalDatas.FirstOrDefaultAsync(x => x.PortalUrl == url);
                ViewBag.schoolname = item.SchoolName;

                string endurl = "/api/SchoolApi/GetSessionDetails?sessionId=" + sessionid;
                //:80/api/SchoolApi/GetSchoolSessions
                string starturl = "http://";
                string apiUrl = String.Format(starturl + item.PortalUrl + endurl);

                WebRequest requestObj = WebRequest.Create(apiUrl);
                requestObj.Method = "GET";

                HttpWebResponse responseGet = null;
                responseGet = (HttpWebResponse)requestObj.GetResponse();
                string result = null;
                ApiSessionDetails school = null;
                using (Stream stream = responseGet.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                    school = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ApiSessionDetails>(result));

                    sr.Close();
                }
                ViewBag.session = school.CurrentSession;
                
                return View(school);
            }

            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Admin/SchoolPortalDatas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolPortalData schoolPortalData = await db.SchoolPortalDatas.FindAsync(id);
            if (schoolPortalData == null)
            {
                return HttpNotFound();
            }
            return View(schoolPortalData);
        }

        // GET: Admin/SchoolPortalDatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SchoolPortalDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SchoolPortalData schoolPortalData)
        {
            if (ModelState.IsValid)
            {
                schoolPortalData.DateCeated = DateTime.UtcNow.AddHours(1);
                db.SchoolPortalDatas.Add(schoolPortalData);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(schoolPortalData);
        }

        // GET: Admin/SchoolPortalDatas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolPortalData schoolPortalData = await db.SchoolPortalDatas.FindAsync(id);
            if (schoolPortalData == null)
            {
                return HttpNotFound();
            }
            return View(schoolPortalData);
        }

        // POST: Admin/SchoolPortalDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SiteName,SchoolName,SchoolAddress,SchoolCurrentPrincipal,ClassCount,EnrolStudentsCount,UnEnrolStudentsCount,TotalStudentsCount,PortalUrl,WebUrl,DateCeated,LastModifiedDate,Usedcard,NonUsedcard,Totalcard,TotalStaff,CurrentSession")] SchoolPortalData schoolPortalData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolPortalData).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(schoolPortalData);
        }

        // GET: Admin/SchoolPortalDatas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolPortalData schoolPortalData = await db.SchoolPortalDatas.FindAsync(id);
            if (schoolPortalData == null)
            {
                return HttpNotFound();
            }
            return View(schoolPortalData);
        }

        // POST: Admin/SchoolPortalDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SchoolPortalData schoolPortalData = await db.SchoolPortalDatas.FindAsync(id);
            db.SchoolPortalDatas.Remove(schoolPortalData);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
       
        //
        public async Task<ActionResult> RefreshSchoolAnalysis(string url)
        {
            try
            {
                var item = await db.SchoolPortalDatas.FirstOrDefaultAsync(x => x.PortalUrl == url);
                ViewBag.schoolname = item.SchoolName;
                string endurl = "/api/SchoolApi/GetSchoolSessions";
                string starturl = "http://";
                string apiUrl = String.Format(starturl + item.PortalUrl + endurl);

                WebRequest requestObj = WebRequest.Create(apiUrl);
                requestObj.Method = "GET";

                HttpWebResponse responseGet = null;
                responseGet = (HttpWebResponse)requestObj.GetResponse();
                string result = null;
                List<ApiSessionList> schools = new List<ApiSessionList>();
                using (Stream stream = responseGet.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                    schools = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<ApiSessionList>>(result));
                    foreach (var sch in schools)
                    {
                        //                        var checksession = await db.SessionAnalyses.FirstOrDefaultAsync(x => x.PortalUrl == url && x.Session == sch.Session && x.Term == sch.Term);
                        //                        if(checksession == null)
                        //                        {
                        //SessionAnalysis sessionAnalysis = new SessionAnalysis();
                        //                            sessionAnalysis.Term = sch.Term;
                        //                            sessionAnalysis.StaffCount = sch
                        //                        }

                    }


                    sr.Close();
                }
                ViewBag.url = url;
                return View();
            }

            catch (WebException webex)
            {

            }

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