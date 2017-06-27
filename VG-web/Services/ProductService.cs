using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibDatabase.DatabaseContext;
using System.Linq.Expressions;
using VG_web.Interface.ServiceInterfaces;
using LibDatabase.Interface;
using AutoMapper;
using VG_web.ViewModels;

namespace VG_web.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productsRepository;
        private readonly IProductDataRepository _productDataRepository;

        public ProductService(IProductRepository productsRepository, IProductDataRepository productDataRepository)
        {
            _productsRepository = productsRepository;
            _productDataRepository = productDataRepository;
        }

        public void Add(ProductViewModel productViewModel)
        {
            Product product = MapFromViewModel(productViewModel);
            _productsRepository.Add(product);
            productViewModel.ProductID = product.ProductID;
        }

        public void Delete(int Id)
        {
            Product product = _productsRepository.GetById(Id);
            _productsRepository.Delete(product);
        }

        public void Edit(ProductViewModel productViewModel)
        {
            Product product = MapFromViewModel(productViewModel);
            _productsRepository.Edit(product);
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            List<ViewModels.ProductViewModel> prodViewModels = new List<ProductViewModel>();
            foreach (Product product in _productsRepository.GetAll())
            {
                ProductViewModel productViewModel = MapToViewModel(product);
                prodViewModels.Add(productViewModel);
            }
            return prodViewModels;
        }

        public ProductViewModel GetById(int id)
        {
            Product product = _productsRepository.GetById(id);
            ViewModels.ProductViewModel productViewModel = MapToViewModel(product);
            return productViewModel;
        }

        public ProductViewModel GetByImg(int id)
        {
            Product product = _productsRepository.GetByImg(id);
            ViewModels.ProductViewModel productViewModel = MapToViewModel(product);
            return productViewModel;
        }

        private ProductViewModel MapToViewModel(Product product)
        {
            ProductViewModel productViewModel = Mapper.Map<ProductViewModel>(product);
            return productViewModel;
        }

        private Product MapFromViewModel(ProductViewModel productViewModel)
        {
            Product product = Mapper.Map<Product>(productViewModel);
            return product;
        }

        public void SetImage(int productId, int imgId)
        {
            Product product = _productsRepository.GetById(productId);
            product.ImageID = imgId;
            _productsRepository.Edit(product);
        }

        public void RemoveImage(int productId)
        {
            Product product = _productsRepository.GetById(productId);
            product.ImageID = null;
            _productsRepository.Edit(product);
        }

        public IEnumerable<ProductViewModel> GetByName(IEnumerable<ProductViewModel> productViewModelList,string name)
        {
            return productViewModelList.Where(p => p.Name.Contains(name));
        }

        public IEnumerable<ProductViewModel> GetByMaker(IEnumerable<ProductViewModel> productViewModelList, int makerId)
        {
            return productViewModelList.Where(p => p.MakerID == makerId);
        }

        public IEnumerable<ProductViewModel> GetBySubcategory( int subcatId)
        {
            return this.GetAll().Where(p => p.SubcategoryID == subcatId);
        }
    }
}
