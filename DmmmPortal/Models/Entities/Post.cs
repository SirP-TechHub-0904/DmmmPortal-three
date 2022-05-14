using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DmmmPortal.Models.Entities
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        //[MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Date Posted")]
        public DateTime DatePosted { get; set; }

        [UIHint("Enum")]
        public PostStatus Status { get; set; }

        [Display(Name = "Posted By")]
        public string PostedBy { get; set; }

        [Display(Name = "Sort Order")]
        public int? SortOrder { get; set; }

      public bool IsPublished { get; set; }
        

        public ICollection<Comment> Comments { get; set; }
        public ICollection<ImageModel> ImageModels { get; set; }
        
    }
}