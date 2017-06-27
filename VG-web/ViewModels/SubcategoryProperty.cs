using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VG_web.ViewModels
{
    public class SubcategoryPropertyViewModel
    {
        public int Id { get; set; }
        public int SubcategoryID { get; set; }
        public int PropertyID { get; set; }

        public virtual PropertyViewModel Property { get; set; }
        public virtual SubcategoryViewModel Subcategory { get; set; }
    }
}