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
    [Authorize]
    public class SubcategoryPropertyController : Controller
    {
        private ISubcategoryPropertyService _subcatPropService;
        private IPropertyService _propertyService;
        private ISubcategoryService _subcategoryService;

        /// <summary>
        /// Constructor for controller
        /// </summary>
        /// <param name="subcatPropService">Service for SubcategoryProperties</param>
        /// <param name="propertyService">Service for Properties</param>
        /// <param name="subcategoryService">Service for Subcategory</param>
        public SubcategoryPropertyController(ISubcategoryPropertyService subcatPropService, IPropertyService propertyService, ISubcategoryService subcategoryService)
        {
            _subcatPropService = subcatPropService;
            _propertyService = propertyService;
            _subcategoryService = subcategoryService;
        }

        /// <summary>
        /// Index returns subcategory properties for subcategory with ID = id
        /// </summary>
        /// <param name="id">Subcategory ID</param>
        /// <returns></returns>
        // GET: SubcategoryProperty
        public ActionResult Index(int id)
        {
            ViewBag.SubcategoryId = id;
            return View(_subcatPropService.GetAll().Where(s => s.SubcategoryID == id));
        }

        /// <summary>
        /// Create returns form for create new property for the subcategory with ID = id
        /// </summary>
        /// <param name="subcatId">Subcategory ID</param>
        /// <returns></returns>
        // GET: SubcategoryProperty/Create
        public ActionResult Create(int subcatId)
        {
            ViewBag.PropertyID = new SelectList(_propertyService.GetAll(), "PropertyID", "Name");
            ViewBag.SubcategoryID = subcatId;
            return View();
        }

        // POST: SubcategoryProperty/Create
        /// <summary>
        /// Crete adds saves subcategoryProperty in service.
        /// </summary>
        /// <param name="subcategoryProperty">Entity to save</param>
        /// <param name="subcatId">Subcategory ID for entity</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubcategoryID,PropertyID")] SubcategoryPropertyViewModel subcategoryProperty, int subcatId)
        {
            if (ModelState.IsValid)
            {
                subcategoryProperty.SubcategoryID = subcatId;
                _subcatPropService.Add(subcategoryProperty);
                return RedirectToAction("Index", new { id = subcategoryProperty.SubcategoryID });
            }
            ViewBag.PropertyID = new SelectList(_propertyService.GetAll(), "PropertyID", "Name", subcategoryProperty.PropertyID);
            ViewBag.SubcategoryID = new SelectList(_subcategoryService.GetAll(), "SubcategoryID", "Name", subcategoryProperty.SubcategoryID);
            return View(subcategoryProperty);
        }

        // GET: SubcategoryProperty/Edit/5
        /// <summary>
        /// Edit serches for subcategory property with ID=id and allows user to edit it
        /// </summary>
        /// <param name="id">ID for entity to edit</param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubcategoryPropertyViewModel SubcategoryProperty = _subcatPropService.GetById((int)id);
            if (SubcategoryProperty == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyID = new SelectList(_propertyService.GetAll(), "PropertyID", "Name", SubcategoryProperty.PropertyID);
            ViewBag.SubcategoryID = new SelectList(_subcategoryService.GetAll(), "SubcategoryID", "Name", SubcategoryProperty.SubcategoryID);
            return View(SubcategoryProperty);
        }

        // POST: SubcategoryProperty/Edit/5
        /// <summary>
        /// Edit saves the subcategory property to service and sets its subcategoryId to subcatId
        /// </summary>
        /// <param name="subcategoryProperty">Entity to save</param>
        /// <param name="subcatId">Subcategory ID for entity</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubcategoryID,PropertyID")] SubcategoryPropertyViewModel subcategoryProperty, int subcatId)
        {
            if (ModelState.IsValid)
            {
                subcategoryProperty.SubcategoryID = subcatId;
                _subcatPropService.Edit(subcategoryProperty);
                return RedirectToAction("Index", new { id = subcategoryProperty.SubcategoryID });
            }
            ViewBag.PropertyID = new SelectList(_propertyService.GetAll(), "PropertyID", "Name", subcategoryProperty.PropertyID);
            ViewBag.SubcategoryID = new SelectList(_subcategoryService.GetAll(), "SubcategoryID", "Name", subcategoryProperty.SubcategoryID);
            return View(subcategoryProperty);
        }

        // GET: SubcategoryProperty/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubcategoryPropertyViewModel SubcategoryProperty = _subcatPropService.GetById((int)id);
            if (SubcategoryProperty == null)
            {
                return HttpNotFound();
            }
            return View(SubcategoryProperty);
        }

        // POST: SubcategoryProperty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int subcatPropId = _subcatPropService.GetById(id).SubcategoryID;
            _subcatPropService.Delete(id);
            return RedirectToAction("Index", new { id = subcatPropId });
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
