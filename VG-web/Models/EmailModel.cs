using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using VG_web.Properties;

namespace VG_web.Models
{
    public class EmailModel
    {
        public string To = Settings.Default.AdminMail;

        public string Subject = "Заявка с сайта";
        public string From { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Body { get; set; }
    }
}