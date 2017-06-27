using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using LibDatabase.Repository;
using LibDatabase.Interface;

namespace LibDatabase.Util
{
    public class NinjectDependencyResolver 
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
        }
    }
}