using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DmmmPortal.Models.Entities
{
    public class Subsection
    {
        public int Id { get; set; }
        [Display(Name = "Name Of Section")]
        public string NameOfSection { get; set; }
        [Display(Name = "Scratch Card Infos")]
        public ICollection<ScratchCardInfo> ScratchCardInfos { get; set; }
        [Display(Name = "Website Url")]
        public string WebsiteUrl { get; set; }

        [Display(Name = "Portal Url")]
        public string PortalUrl { get; set; }
        [Display(Name = "Check Result Url")]
        public string CheckResultUrl { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "School Logo")]
        public byte[] SchoolLogo { get; set; }
        [Display(Name = "School Address")]
        public string Address { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}