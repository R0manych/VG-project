using LibDatabase.Interface;
using LibDatabase.Repository;
using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VG_web.Interface.ServiceInterfaces;
using VG_web.Models;

namespace VG_web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ICategoryService _categoryService;

        public HomeController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return View(_categoryService.GetAll().ToList());
        }

        [HttpPost]
        public ActionResult Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new EmailController().SendEmail(model).Deliver();

                    return RedirectToAction("Success");
                }
                catch (Exception ex)
                {
                    Log.Debug($"Error. {ex.Message}");
                    return RedirectToAction("Error");
                }
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}