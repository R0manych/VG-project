using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace VG_web.Handlers
{
    /// <summary>
    /// Summary description for MailHandler
    /// </summary>
    public class MailHandler : IHttpHandler
    {
        class Email
        {
            public string From {get;set ;}
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Body { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string jsonString = String.Empty;
            HttpContext.Current.Request.InputStream.Position = 0;
            using (System.IO.StreamReader inputStream =
            new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
                System.Web.Script.Serialization.JavaScriptSerializer jSerialize =
                    new System.Web.Script.Serialization.JavaScriptSerializer();
                var email = jSerialize.Deserialize<Email>(jsonString);

                if (email != null)
                {
                    string from = email.From;
                    string name = email.Name;
                    string phone = email.Phone;
                    string body = email.Body;

                    MailMessage mail = new MailMessage(from, "rchernigovskix@rambler.ru");
                    SmtpClient client = new SmtpClient();
                    client.Port = 25;
                    client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                    client.PickupDirectoryLocation = "C://Mail";
                    client.UseDefaultCredentials = false;
                    client.Host = "smtp.google.com";
                    mail.Subject = name;
                    mail.Body = body + Environment.NewLine + phone;
                    client.Send(mail);  
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}