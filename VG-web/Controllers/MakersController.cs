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
    public class MakersController : Controller
    {
        private IMakerService _makerService;
        private IImageService _imageService;

        public MakersController(IMakerService makerService, IImageService imageService)
        {
            _makerService = makerService;
            _imageService = imageService;
        }

        // GET: Makers
        public ActionResult Index()
        {
            return View(_makerService.GetAll());
        }

        // GET: Makers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MakerViewModel maker = _makerService.GetById((int)id);
            if (maker == null)
            {
                return HttpNotFound();
            }
            return View(maker);
        }

        // GET: Makers/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Makers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MakerID,Name,Website,Description,UI,Image")] MakerViewModel maker)
        {
            if (ModelState.IsValid)
            {
                _makerService.Add(maker);
                return RedirectToAction("Index");
            }
            return View(maker);
        }

        // GET: Makers/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MakerViewModel maker = _makerService.GetById((int)id);
            if (maker == null)
            {
                return HttpNotFound();
            }
            return View(maker);
        }

        // POST: Makers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MakerID,Name,Website,Description")] MakerViewModel maker, int? imgId)
        {
            if (ModelState.IsValid)
            {
                maker.ImageID = imgId;
                _makerService.Edit(maker);
                return RedirectToAction("Index");
            }
            return View(maker);
        }

        // GET: Makers/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MakerViewModel maker = _makerService.GetById((int)id);
            if (maker == null)
            {
                return HttpNotFound();
            }
            return View(maker);
        }

        // POST: Makers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _makerService.Delete(id);
            return RedirectToAction("Index");
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
            return View(_makerService.GetById(id));
        }

        /// <summary>
        /// Adds Image to subcategory with ID = subcatId 
        /// </summary>
        /// <param name="subcatId">Subcategory Id</param>
        /// <returns></returns>
        /// 
        [Authorize]
        public ActionResult CreateImage(int catId)
        {
            ImageViewModel img = new ImageViewModel();
            _imageService.Add(img);
            _makerService.SetImage(catId, img.ImageID);
            return View("Images/Create", img);
        }

        /// <summary>
        /// Deletes Image for subcategory with ID = subcatId
        /// </summary>
        /// <param name="subcatId">Subcategory ID</param>
        /// <returns></returns>
        /// 
        [Authorize]
        public ActionResult DeleteImage(int catId)
        {
            ImageViewModel img = _makerService.GetById(catId).Image;
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
