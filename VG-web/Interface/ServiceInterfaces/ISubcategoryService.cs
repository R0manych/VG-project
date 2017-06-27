using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VG_web.ViewModels;

namespace VG_web.Interface.ServiceInterfaces
{
    public interface ISubcategoryService
    {
        IEnumerable<SubcategoryViewModel> GetAll();

        SubcategoryViewModel GetById(int id);

        void Add(SubcategoryViewModel subcategory);

        void Edit(SubcategoryViewModel subcategory);

        void Delete(int id);

        void SetImage(int subcatId, int imgId);

        void RemoveImage(int subcatId);

        SubcategoryViewModel GetByImg(int id);
    }
}
