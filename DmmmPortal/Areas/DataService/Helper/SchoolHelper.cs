using DmmmPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Net;
using DmmmPortal.Models.Dtos.Api;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DmmmPortal.Areas.DataService.Helper
{
    public class SchoolHelper
    {
        public async static Task<string> AvialableResult(string url, string sessionyear, string term)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var item = db.SchoolPortalDatas.FirstOrDefault(x => x.PortalUrl == url);
                   
                    string endurl = "/api/SchoolApi/GetAllResultsBySession";
                    string starturl = "http://";
                    string apiUrl = String.Format(starturl + item.PortalUrl + endurl);

                    WebRequest requestObj = WebRequest.Create(apiUrl);
                    requestObj.Method = "GET";

                    HttpWebResponse responseGet = null;
                    responseGet = (HttpWebResponse)requestObj.GetResponse();
                    string result = null;
                    List<ApiAvailableResultsDto> schools = new List<ApiAvailableResultsDto>();
                    using (Stream stream = responseGet.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(stream);
                        result = sr.ReadToEnd();
                        schools = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<ApiAvailableResultsDto>>(result));

                        sr.Close();
                    }
                    var resultbysession = schools.ToList();
                    foreach(var i in resultbysession)
                    {
                        if(i.Session.Contains(sessionyear) && i.Session.Contains(term))
                        {
                            return i.ResultCount;
                        }
                    }
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
            return "";
        }
    }
}