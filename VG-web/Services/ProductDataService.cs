using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibDatabase.DatabaseContext;
using System.Linq.Expressions;
using LibDatabase.Interface;
using VG_web.Interface.ServiceInterfaces;
using AutoMapper;
using VG_web.ViewModels;

namespace VG_web.Services
{
    public class ProductDataService : IProductDataService
    {
        private readonly IProductDataRepository _productDataRepository;

        public ProductDataService(IProductDataRepository productDataRepository)
        {
            _productDataRepository = productDataRepository;
        }

        public void Add(ProductDataViewModel productDataViewModel)
        {
            if (!(_productDataRepository.GetAll().Where(pd => pd.PropertyID == productDataViewModel.PropertyID && pd.ProductID == productDataViewModel.ProductID).Any()))
            {
                ProductData productData = MapFromViewModel(productDataViewModel);
                _productDataRepository.Add(productData);
            }
        }

        public void Delete(int Id)
        {
            ProductData productData = _productDataRepository.GetById(Id);
            _productDataRepository.Delete(productData);
        }

        public void Edit(ProductDataViewModel productDataViewModel)
        {
            ProductData productData = MapFromViewModel(productDataViewModel);
            _productDataRepository.Edit(productData);
        }

        public IEnumerable<ProductDataViewModel> GetAll()
        {
            List<ProductDataViewModel> prodDataViewModels = new List<ProductDataViewModel>();
            foreach (ProductData productData in _productDataRepository.GetAll())
            {
                ProductDataViewModel productDataViewModel = MapToViewModel(productData);
                prodDataViewModels.Add(productDataViewModel);
            }
            return prodDataViewModels;
        }

        public ProductDataViewModel GetById(int id)
        {
            ProductData productData = _productDataRepository.GetById(id);
            ProductDataViewModel productDataViewModel = MapToViewModel(productData);
            return productDataViewModel;
        }

        private ProductDataViewModel MapToViewModel(ProductData productData)
        {
            ProductDataViewModel productDataViewModel = Mapper.Map<ProductDataViewModel>(productData);
            return productDataViewModel;
        }

        private ProductData MapFromViewModel(ProductDataViewModel productDataViewModel)
        {
            ProductData productData = Mapper.Map<ProductData>(productDataViewModel);
            return productData;
        }



    }
}
