using LibDatabase.DatabaseContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VG_web.ViewModels
{
    public class SubcategoryViewModel
    {
        public int SubcategoryID { get; set; }

        [Required]
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public int? ImageID { get; set; }

        public string CategoryName { get; set; }
        public virtual ImageViewModel Image { get; set; }
    }
}