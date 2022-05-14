using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DmmmPortal.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}