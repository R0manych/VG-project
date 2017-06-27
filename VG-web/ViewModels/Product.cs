using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VG_web.ViewModels
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }

        [StringLength(40), Required]
        public string Name { get; set; }

        [Range(0, 10000000)]
        public decimal? Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int MakerID { get; set; }
        public int? ImageID { get; set; }

        [HiddenInput(DisplayValue=false)]   
        public int SubcategoryID { get; set; }
        public virtual ImageViewModel Image { get; set; }

        public virtual MakerViewModel Maker { get; set; }

        public virtual SubcategoryViewModel Subcategory { get; set; }
    }
}