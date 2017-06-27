using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using LibDatabase.Repository;
using LibDatabase.Interface;
using VG_web.Interface.ServiceInterfaces;
using VG_web.Services;

namespace VG_web.App_Start
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            kernel.Bind<IMakerRepository>().To<MakerRepository>();
            kernel.Bind<IProductDataRepository>().To<ProductDataRepository>();
            kernel.Bind<IProductRepository>().To<ProductRepository>();
            kernel.Bind<ISubcategoryPropertyRepository>().To<SubcategoryPropertyRepository>();
            kernel.Bind<ISubcategoryRepository>().To<SubcategoryRepository>();
            kernel.Bind<IPropertyRepository>().To<PropertyRepository>();
            kernel.Bind<IImageRepository>().To<ImageRepository>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<IImageService>().To<ImageService>();
            kernel.Bind<IMakerService>().To<MakerService>();
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<IProductDataService>().To<ProductDataService>();
            kernel.Bind<IPropertyService>().To<PropertyService>();
            kernel.Bind<ISubcategoryPropertyService>().To<SubcategoryPropertyService>();
            kernel.Bind<ISubcategoryService>().To<SubcategoryService>();
        }
    }
}