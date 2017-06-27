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
    public class ProductDataController : Controller
    {
        private IProductDataService _productDataService;
        private ISubcategoryService _subcategoryService;
        private IPropertyService _propertyService;
        private ISubcategoryPropertyService _subcatPropService;
        private IProductService _productService;


        public ProductDataController(IProductDataService productDataService, ISubcategoryService subcategoryService, IPropertyService propertyService, ISubcategoryPropertyService subcatPropService, IProductService productService)
        {
            _productDataService = productDataService;
            _subcategoryService = subcategoryService;
            _propertyService = propertyService;
            _subcatPropService = subcatPropService;
            _productService = productService;
        }

        // GET: ProductData
        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            ViewBag.SubcategoryId = _productService.GetById(id).SubcategoryID;
            return View(_productDataService.GetAll().Where(pd => pd.ProductID == id));
        }

        // GET: ProductData/Create
        /// <summary>
        /// Creates product data for product with ID = productId
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Create(int productId)
        {
            ViewBag.ProductID = productId;
            int subcatId = _productService.GetById(productId).SubcategoryID;            
            ViewBag.SubcategoryID = new SelectList(_subcategoryService.GetAll().Where(s => s.SubcategoryID == subcatId), "SubcategoryID", "Name");

            var curSubcatProperties = _subcatPropService.GetAll().Where(p => p.SubcategoryID == subcatId);
            var properties = _propertyService.GetAll();
            var curProdProperties = from Subcategory_property in curSubcatProperties
                                    join property in properties on Subcategory_property.PropertyID equals property.PropertyID
                                    select new { PropertyID = Subcategory_property.PropertyID, Name = property.Name };

            ViewBag.PropertyID = new SelectList(curProdProperties.AsEnumerable(), "PropertyID", "Name");            
            return View(); 
        }

        // POST: ProductData/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,SubcategoryID,PropertyID,Value")] ProductDataViewModel productData)
        {
            if (ModelState.IsValid)
            {
                _productDataService.Add(productData);
                return RedirectToAction("Index", new { id = productData.ProductID });
            }
            return View(productData);
        }

        // GET: ProductData/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if ((id == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDataViewModel ProductData = _productDataService.GetById((int)id);
            if (ProductData == null)
            {
                return HttpNotFound();
            }
            return View(ProductData);
        }

        // POST: ProductData/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,SubcategoryID,PropertyID,Value")] ProductDataViewModel ProductData)
        {
            if (ModelState.IsValid)
            {
                _productDataService.Edit(ProductData);
                return RedirectToAction("Index", new { id = ProductData.ProductID });
            }
            return View(ProductData);
        }

        // GET: Products/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if ((id == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDataViewModel ProductData = _productDataService.GetById((int)id);
            if (ProductData == null)
            {
                return HttpNotFound();
            }
            return View(ProductData);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            int productId = _productDataService.GetById((int)id).ProductID;
            _productDataService.Delete((int)id);
            return RedirectToAction("Index", new { id = productId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }

        /*private bool CheckFormat(ProductDataViewModel productData)
        {
            bool format = _propertyService.GetById(productData.PropertyID).Format;
            int result = 0;
            if (format)
            {
                return (Int32.TryParse(productData.Value, out result));
            }
            else
                return true;
        }*/
    }
}
