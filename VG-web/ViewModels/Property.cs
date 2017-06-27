using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VG_web.ViewModels
{
    public class PropertyViewModel
    {
        public int PropertyID { get; set; }

        [StringLength(40), Required]
        public string Name { get; set; }
    }
}