using DmmmPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DmmmPortal.Models.Entities
{
    public class ApiSessionList
    {
        public int Id { get; set; }

        [Display(Name = "Full Session")]

        public string FullSession { get; set; }

        [Display(Name = "Session Status")]

        public SessionStatus SessionStatus { get; set; }

        [Display(Name = "Session")]
        public string Session { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }
    }
}