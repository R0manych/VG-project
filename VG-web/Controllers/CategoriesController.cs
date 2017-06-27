using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using VG_web.ViewModels;
using VG_web.Interface.ServiceInterfaces;
using LibDatabase.DatabaseContext;
using log4net;

namespace VG_web.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoryService _categoryService;
        private IImageService _imageService;

        public CategoriesController(ICategoryService categoryService, IImageService imageService)
        {
            _categoryService = categoryService;
            _imageService = imageService;
        }

        // GET: Categories
        public ActionResult Index()
       {
            return View(_categoryService.GetAll());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryViewModel category = _categoryService.GetById((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {               
                _categoryService.Add(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryViewModel category = _categoryService.GetById((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(CategoryViewModel category, int? imgId)
        {
            if (ModelState.IsValid)
            {
                category.ImageID = imgId;
                _categoryService.Edit(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryViewModel category = _categoryService.GetById((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// OpenImage opens page for work with images for Category with ID = id
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns></returns>
        /// 
        [Authorize]
        public ActionResult OpenImage(int id)
        {
            return View(_categoryService.GetById(id));
        }

        /// <summary>
        /// Adds Image to Сategory with ID = subcatId 
        /// </summary>
        /// <param name="catId">Category Id</param>
        /// <returns></returns>
        /// 
        [Authorize]
        public ActionResult CreateImage(int catId)
        {
            ImageViewModel img = new ImageViewModel();
            _imageService.Add(img);
            _categoryService.SetImage(catId, img.ImageID);
            return View("Images/Create", img);            
        }

        /// <summary>
        /// Deletes Image for category with ID = catId
        /// </summary>
        /// <param name="catId">Сategory ID</param>
        /// <returns></returns>
        /// 
        [Authorize]
        public ActionResult DeleteImage(int catId)
        {
            ImageViewModel img = _categoryService.GetById(catId).Image;
            return View("Images/Delete", img);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            base.Dispose(disposing);
        }
    }
}
