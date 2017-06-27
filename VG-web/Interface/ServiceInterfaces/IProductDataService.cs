
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VG_web.ViewModels;

namespace VG_web.Interface.ServiceInterfaces
{
    public interface IProductDataService
    {
        IEnumerable<ProductDataViewModel> GetAll();

        ProductDataViewModel GetById(int id);

        void Add(ProductDataViewModel productData);

        void Edit(ProductDataViewModel productData);

        void Delete(int id);
    }
}
