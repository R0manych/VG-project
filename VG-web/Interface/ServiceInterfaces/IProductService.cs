using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VG_web.ViewModels;

namespace VG_web.Interface.ServiceInterfaces
{
    public interface IProductService
    {
        IEnumerable<ProductViewModel> GetAll();

        IEnumerable<ProductViewModel> GetByName(IEnumerable<ProductViewModel> productViewModelList, string name);

        IEnumerable<ProductViewModel> GetByMaker(IEnumerable<ProductViewModel> productViewModelList, int makerId);

        IEnumerable<ProductViewModel> GetBySubcategory(int subcatId);

        ProductViewModel GetById(int id);

        void Add(ProductViewModel product);

        void Edit(ProductViewModel product);

        void Delete(int id);

        void SetImage(int productId, int imgId);

        void RemoveImage(int productId);

        ProductViewModel GetByImg(int id);
    }
}
