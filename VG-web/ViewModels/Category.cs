using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VG_web.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }

        [StringLength(40), Required]
        [Display(Name = "Category")]
        public string Name { get; set; }
        public int? ImageID { get; set; }
        [Display(Name = "View")]
        public virtual ImageViewModel Image { get; set; }
    }
}