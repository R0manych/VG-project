using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VG_web.ViewModels;

namespace VG_web.Interface.ServiceInterfaces
{
    public interface ISubcategoryPropertyService
    {
        IEnumerable<SubcategoryPropertyViewModel> GetAll();

        SubcategoryPropertyViewModel GetById(int id);

        void Add(SubcategoryPropertyViewModel subcategoryProperty);

        void Edit(SubcategoryPropertyViewModel subcategoryProperty);

        void Delete(int id);
    }
}
