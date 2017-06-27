using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VG_web.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace VG_web.Controllers
{
       [Authorize]
        public class RoleController : Controller
        {
            ApplicationDbContext context;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public RoleController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public RoleController()
            {
                context = new ApplicationDbContext();
            }

            /// Get All Roles
            public ActionResult Index()
            {

                if (User.Identity.IsAuthenticated)
                {


                    if (!isAdminUser())
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

                var Roles = context.Roles.ToList();
                return View(Roles);

            }
            public Boolean isAdminUser()
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = User.Identity;
                    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                    var s = UserManager.GetRoles(user.GetUserId());
                    if (s[0].ToString() == "Admin")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }

            /// Create  a New user
            public ActionResult Create()
            {
                if ((!User.Identity.IsAuthenticated) || (!isAdminUser()))
                {
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }

        /// Create a New Role  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                    return RedirectToAction("Index", "Home");                    
                }
                AddErrors(result);               
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
   
}