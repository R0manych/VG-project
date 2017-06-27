using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ninject;
using VG_web.Interface.ServiceInterfaces;
using VG_web.ViewModels;

namespace VG_web.Controllers
{
    public class ImagesController : Controller
    {
        private IImageService _imageService;
        private ICategoryService _categoryService;
        private ISubcategoryService _subcategoryService;
        private IMakerService _makerService;
        private IProductService _productService;

        public ImagesController(ICategoryService categoryService, IImageService imageService, ISubcategoryService subcategoryService, IMakerService makerService, IProductService productService )
        {
            _categoryService = categoryService;
            _imageService = imageService;
            _subcategoryService = subcategoryService;
            _makerService = makerService;
            _productService = productService;
        }      

        // GET: Images/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        /// <summary>
        /// Adds picture to image with ID = imgId
        /// </summary>
        /// <param name="image"></param>
        /// <param name="photo">Picture to add</param>
        /// <param name="imgId">Id of Image to attach with picture</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageID,UI,Picture")] ImageViewModel image, HttpPostedFileBase photo, int imgId)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    var supportedTypes = new[] { "jpg", "jpeg", "png" };

                    var fileExt = System.IO.Path.GetExtension(photo.FileName).Substring(1);

                    if (!supportedTypes.Contains(fileExt))
                    {
                        ModelState.AddModelError("photo", "Invalid type. Only the following types (jpg, jpeg, png) are supported.");
                    }
                    image.Picture = new byte[photo.ContentLength];
                    photo.InputStream.Read(image.Picture, 0, image.Picture.Length);
                    image.ImageID = imgId;
                    _imageService.Edit(image);
 
                }
                if (_categoryService.GetByImg(imgId) != null)
                {
                    return RedirectToAction("Index", "Categories");
                }
                if (_subcategoryService.GetByImg(imgId) != null)
                {
                    return RedirectToAction("IndexAdmin", "Subcategories", new { id = _subcategoryService.GetByImg(imgId).CategoryID });
                }
                if (_makerService.GetByImg(imgId) != null)
                {
                    return RedirectToAction("Index", "Makers");
                }
                if (_productService.GetByImg(imgId) != null)
                {
                    return RedirectToAction("Index", "Products", new { id = _productService.GetByImg(imgId).SubcategoryID });
                }
            }
            return View(image);
        }

        //GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageViewModel image = _imageService.GetById((int)id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {          
            if (_categoryService.GetByImg(id) != null)
            {
                _categoryService.RemoveImage(id);
                _imageService.Delete(id);
                return RedirectToAction("Index", "Categories");
            }
            if (_subcategoryService.GetByImg(id) != null)
            {
                int catId = _subcategoryService.GetByImg(id).CategoryID;
                _subcategoryService.RemoveImage(id);
                _imageService.Delete(id);
                return RedirectToAction("IndexAdmin", "Subcategories", new { id = catId});
            }
            if (_makerService.GetByImg(id) != null)
            {
                _makerService.RemoveImage(id);
                _imageService.Delete(id);
                return RedirectToAction("Index", "Makers");
            }
            if (_productService.GetByImg(id) != null)
            {
                int subcatId = _productService.GetByImg(id).SubcategoryID;
                _productService.RemoveImage(id);
                _imageService.Delete(id);
                return RedirectToAction("Index", "Products", new { subcatId = subcatId });
            }
            return RedirectToAction("Index");
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
