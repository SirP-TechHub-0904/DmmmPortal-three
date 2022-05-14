using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DmmmPortal.Models.Dtos
{
    [JsonObject]
    public class OutputDetailsDto
    {
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        public string SchoolCurrentPrincipal { get; set; }
        public string ClassCount { get; set; }
        public string EnrolStudentsCount { get; set; }
        public string UnEnrolStudentsCount { get; set; }
        public string Url { get; set; }

        public string Usedcard { get; set; }
        public string NonUsedcard { get; set; }
        public string Totalcard { get; set; }
        public string TotalStaff { get; set; }
        public string CurrentSession { get; set; }
    }
}