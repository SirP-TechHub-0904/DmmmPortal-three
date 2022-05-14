using DmmmPortal.Models;
using DmmmPortal.Models.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace DmmmPortal.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet, Tls]
        public ActionResult Index()
        {
            return View();
        }

        //mail
        public ActionResult Mail(string Message, string subject, string EmailAddress, string Name)
        {
            try
            {
                MailMessage mail = new MailMessage();

                //set the addresses 
                mail.From = new MailAddress("noreply@dmmmg.org"); //IMPORTANT: This must be same as your smtp authentication address.
                mail.To.Add("noreply@dmmmg.org");

                //set the content 
                mail.Subject = subject;
                mail.Body = "Name: -"+ Name + "Email: -"+ "Message: "+ Message;
                //send the message 
                SmtpClient smtp = new SmtpClient("mail.dmmmg.org");

                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                NetworkCredential Credentials = new NetworkCredential("noreply@dmmmg.org", "sister@2018");
                smtp.Credentials = Credentials;
                smtp.Send(mail);
                TempData["Emails"] = "Message Sent Successfully!";
            }
            catch (Exception ex)
            {
                TempData["Emailf"] = "mail currently not responding. Server undergoing update. " + ex.Message;
            }
            return RedirectToAction("Contact", "Home");
        }

        #region


        
        public JsonResult GetEvents()
        {
            var events = db.Events.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEventpa(string subject, string description, DateTime start, DateTime end, string color, bool general, bool fday)
        {
            try
            {
                Event m = new Event();
                m.Subject = subject;
                m.DIscription = description;
                m.Start = start;
                m.End = end;
                m.Color = color;
                m.GeneralEvent = general;
                m.IsFullDay = fday;
                db.Events.Add(m);
                db.SaveChanges();
                TempData["success"] = "Event Added";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Something Went Wrong. Try again.";
            }
            return RedirectToAction("Index", "Panel", new { area = "Student" });
        }
        #endregion

        public ActionResult About()
        {
          

            return View();
        }

        public ActionResult Contact()
        {
           

            return View();
        }

        public ActionResult Region()
        {


            return View();
        }

        public ActionResult Calendar(int? page)
        {
            var calendar = db.Events.OrderBy(x => x.Start).ToList();

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(calendar.ToPagedList(pageNumber, pageSize));
          
        }

        public ActionResult Apostolate()
        {


            return View();
        }
        [HttpGet, Tls]
        public ActionResult Main()
        {          
            return View();
        }

        public ActionResult _Slider()
        {
            var item = db.ImageSlider.Where(x => x.CurrentSlider == true).ToList();
            return PartialView(item);
        }
        public ActionResult _News()
        {
            var items = db.Posts.Include(x => x.ImageModels).Include(d => d.Comments).OrderByDescending(x=>x.DatePosted).Take(4).ToList();

            return PartialView(items);
        }

        public class TlsAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var request = filterContext.HttpContext.Request;
                if (request.IsSecureConnection)
                {
                    filterContext.HttpContext.Response.AddHeader("Strict-Transport-Security", "max-age=15552000");
                }
                else if (!request.IsLocal && request.Headers["Upgrade-Insecure-Requests"] == "1")
                {
                    var url = new Uri("https://" + request.Url.GetComponents(UriComponents.Host | UriComponents.PathAndQuery, UriFormat.Unescaped), UriKind.Absolute);
                    filterContext.Result = new RedirectResult(url.AbsoluteUri);
                }
            }
        }

    }
}