using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VG_web.ViewModels
{
    public class ProductDataViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }
        public int ProductID { get; set; }
        public int PropertyID { get; set; }

        public virtual PropertyViewModel Property { get; set; }

        public virtual ProductViewModel Product { get; set; }
    }
}