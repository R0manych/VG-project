using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VG_web.ViewModels
{
    public class MakerViewModel
    {
        public int MakerID { get; set; }

        [StringLength(40), Required]
        public string Name { get; set; }

        [StringLength(100), Required]
        public string Website { get; set; }
        public string Description { get; set; }
        public int? ImageID { get; set; }
        public virtual ImageViewModel Image { get; set; }
    }
}