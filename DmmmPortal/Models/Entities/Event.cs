using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DmmmPortal.Models.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string DIscription { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Color { get; set; }
        [Display(Name = "General Event")]
        public bool? GeneralEvent { get; set; }
        public bool? IsFullDay { get; set; }
    }
}