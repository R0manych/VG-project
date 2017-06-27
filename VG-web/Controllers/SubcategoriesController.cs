using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VG_web.Models;
using System.IO;
using VG_web.Interface.ServiceInterfaces;
using VG_web.ViewModels;

namespace VG_web.Controllers
{
    public class SubcategoriesController : Controller
    {
        private ISubcategoryService _subcategoryService;
        private ISubcategoryPropertyService _subcatPropService;
        private ICategoryService _categoryService;
        private IImageService _imageService;

        public SubcategoriesController(ISubcategoryService subcategoryService, ISubcategoryPropertyService subcatPropService, ICategoryService categoryService, IImageService imageService)
        {
            _subcategoryService = subcategoryService;
            _subcatPropService = subcatPropService;
            _categoryService = categoryService;
            _imageService = imageService;
        }

        // GET: Subcategories
        /// <summary>
        /// Insex returns list with subcategories where CategoryID = id
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            ViewBag.CategoryId = id;
            return View(_subcategoryService.GetAll().ToList().Where(s => s.CategoryID == id));
        }

        public ActionResult IndexAdmin(int id)
        {
            ViewBag.CategoryId = id;
            return View(_subcategoryService.GetAll().ToList().Where(s => s.CategoryID == id));
        }

        // GET: Subcategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subctgr_property = _subcatPropService.GetById((int)(id));
            if (subctgr_property == null)
            {
                return HttpNotFound();
            }
            return View(subctgr_property);
        }

        // GET: Subcategories/Create
        /// <summary>
        /// Creates subcategory for the Category with ID = catId
        /// </summary>
        /// <param name="catId">Category ID</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Create(int catId)
        {
            ViewBag.CategoryID = catId;
            return View();
        }

        // POST: Subcategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubcategoryID,Name,UI,Image")] SubcategoryViewModel subcategory, int catid)
        {
            if (ModelState.IsValid)
            {
                subcategory.CategoryID = catid;
                _subcategoryService.Add(subcategory);
                return RedirectToAction("Index", "SubcategoryProperty", new { id = subcategory.SubcategoryID });
            }
            ViewBag.CategoryID = new SelectList(_categoryService.GetAll(), "CategoryID", "Name", subcategory.CategoryID);
            return View(subcategory);
        }

        // GET: Subcategories/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubcategoryViewModel subcategory = _subcategoryService.GetById((int)id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(_categoryService.GetAll(), "CategoryID", "Name", subcategory.CategoryID);
            return View(subcategory);
        }

        // POST: Subcategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubcategoryID,Name,CategoryID,UI,Image")] SubcategoryViewModel subcategory, int? imgId, int catId)
        {
            if (ModelState.IsValid)
            {
                subcategory.ImageID = imgId;
                subcategory.CategoryID = catId;
                _subcategoryService.Edit(subcategory);
                return RedirectToAction("IndexAdmin", new { id = subcategory.CategoryID });
            }
            ViewBag.CategoryID = new SelectList(_categoryService.GetAll(), "CategoryID", "Name", subcategory.CategoryID);
            return View(subcategory);
        }

        // GET: Subcategories/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubcategoryViewModel subcategory = _subcategoryService.GetById((int)id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // POST: Subcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int catId = _subcategoryService.GetById(id).CategoryID;
            _subcategoryService.Delete(id);
            return RedirectToAction("IndexAdmin", new { id = catId });
        }

        /// <summary>
        /// OpenImage opens page for work with images for subcategory with ID = id
        /// </summary>
        /// <param name="id">Subcategory Id</param>
        /// <returns></returns>
        /// 
        [Authorize]
        public ActionResult OpenImage(int id)
        {
            return View(_subcategoryService.GetById(id));
        }

        /// <summary>
        /// Adds Image to subcategory with ID = subcatId 
        /// </summary>
        /// <param name="subcatId">Subcategory Id</param>
        /// <returns></returns>
        /// 
        [Authorize]
        public ActionResult CreateImage(int subcatId)
        {
            ImageViewModel img = new ImageViewModel();
            _imageService.Add(img);
            _subcategoryService.SetImage(subcatId, img.ImageID);
            return View("Images/Create", img);
        }

        /// <summary>
        /// Deletes Image for subcategory with ID = subcatId
        /// </summary>
        /// <param name="subcatId">Subcategory ID</param>
        /// <returns></returns>
        /// 
        [Authorize]
        public ActionResult DeleteImage(int subcatId)
        {
            ImageViewModel img = _subcategoryService.GetById(subcatId).Image;
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
