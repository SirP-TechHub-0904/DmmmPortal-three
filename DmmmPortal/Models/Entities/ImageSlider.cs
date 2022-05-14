using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DmmmPortal.Models.Entities
{
    public class ImageSlider
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        [Display(Name = "Slider Alt")]
        public string SliderAlt { get; set; }
       
        [Display(Name = "Current Slider")]
        public bool CurrentSlider { get; set; }
    }
}