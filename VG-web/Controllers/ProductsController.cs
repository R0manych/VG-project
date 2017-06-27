using System.Net;
using System.Web.Mvc;
using VG_web.Interface.ServiceInterfaces;
using VG_web.ViewModels;
using PagedList;

namespace VG_web.Controllers
{
 
    public class ProductsController : Controller
    {
        private IProductService _productService;
        private IMakerService _makerService;
        private IProductDataService _productDataService;
        private IPropertyService _propertyService;
        private IImageService _imageService;

        public ProductsController(IProductService productService, IMakerService makerService, IProductDataService productDataService, IPropertyService propertyService, IImageService imageService)
        {
            _productService = productService;
            _makerService = makerService;
            _productDataService = productDataService;
            _propertyService = propertyService;
            _imageService = imageService;
        }

        // GET: Products
        /// <summary>
        /// Rerturns product list with filtering by subcategory, name, maker and paging
        /// </summary>
        /// <param name="subcatId">Subcategory filter</param>
        /// <param name="searchString">Name filter</param>
        /// <param name="maker">Maker Filter</param>
        /// <param name="page">Paging</param>
        /// <returns></returns>
        [OutputCache (Duration =120, VaryByParam ="page")]  
        public ActionResult Index(int? subcatId, string searchString, int? maker, int? page)
        {
            var products = _productService.GetAll();
            if (subcatId != null)
            {
                products = _productService.GetBySubcategory((int)subcatId);
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                products = _productService.GetByName(products, searchString);
                ViewBag.SearchString = searchString;
            }
            if (maker != null)
            {
                products = _productService.GetByMaker(products, (int)maker);
                ViewBag.Maker = maker;
            }
            ViewBag.SubcategoryId = subcatId;
            ViewBag.maker = new SelectList(_makerService.GetAll(), "MakerID", "Name");
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }   
            return View(_productDataService.GetById((int)id));
        }

        // GET: Products/Create
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">SubcategoryID</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Create(int? id)
        {
            ViewBag.MakerID = new SelectList(_makerService.GetAll(), "MakerID", "Name");
            ViewBag.SubcategoryId = id;
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel product, int subcatId)
        {
            if (ModelState.IsValid)
            {
                product.SubcategoryID = subcatId;
                _productService.Add(product);
                return RedirectToAction("Create", "ProductData", new { ProductId = product.ProductID});
            }
            ViewBag.MakerID = new SelectList(_makerService.GetAll(), "MakerID", "Name", product.MakerID);
            ViewBag.SubcategoryId = subcatId;
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductViewModel product = _productService.GetById((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakerID = new SelectList(_makerService.GetAll(), "MakerID", "Name", product.MakerID);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,Price,Description,MakerID,UI,Image")] ProductViewModel product, int? imgId, int subcatId)
        {
            if (ModelState.IsValid)
            {
                product.ImageID = imgId;
                product.SubcategoryID = subcatId;               
                _productService.Edit(product);
                return RedirectToAction("Index", new {subcatId = subcatId });
            }
            ViewBag.MakerID = new SelectList(_makerService.GetAll(), "MakerID", "Name", product.MakerID);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {   
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductViewModel product = _productService.GetById((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int subcategoryId = _productService.GetById((int)id).SubcategoryID;
            _productService.Delete(id);
            return RedirectToAction("Index", new { id = subcategoryId });
        }

        /// <summary>
        /// OpenImage opens page for work with images for subcategory with ID = id
        /// </summary>
        /// <param name="id">Subcategory Id</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult OpenImage(int id)
        {
            return View(_productService.GetById(id));
        }

        /// <summary>
        /// Adds Image to subcategory with ID = subcatId 
        /// </summary>
        /// <param name="subcatId">Subcategory Id</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult CreateImage(int prodId)
        {
            ImageViewModel img = new ImageViewModel();
            _imageService.Add(img);
            _productService.SetImage(prodId, img.ImageID);
            return View("Images/Create", img);
        }

        /// <summary>
        /// Deletes Image for subcategory with ID = subcatId
        /// </summary>
        /// <param name="subcatId">Subcategory ID</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult DeleteImage(int catId)
        {
            ImageViewModel img = _productService.GetById(catId).Image;
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
