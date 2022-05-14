using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DmmmPortal.Models.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
       
        public string Content { get; set; }
        [Display(Name = "Date Commented:")]
        public DateTime DateCommented { get; set; }

        //navigation property
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}