using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DmmmPortal.Models.Entities
{
    public class ScratchCardInfo
    {
        public int Id { get; set; }
        public int SubsectionId { get; set; }
        public Subsection Subsection { get; set; }
        [Display(Name = "Card Batch Number")]
        public string CardBatchNumber { get; set; }
        [Display(Name = "Is Updated To Portal")]
        public bool IsUpdatedToPortal { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Date Updated To Portal")]
        public DateTime? DateUpdatedToPortal { get; set; }
    }
}