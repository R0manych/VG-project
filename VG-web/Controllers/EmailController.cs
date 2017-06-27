using ActionMailer.Net.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VG_web.Models;

namespace VG_web.Controllers
{
    public class EmailController : MailerBase
    {
        public EmailResult SendEmail(EmailModel model)
        {
            To.Add(model.To);

            From = model.From;

            Subject = model.Subject;

            return Email("SendEmail", model);
        }
    }
}