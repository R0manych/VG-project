using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VG_web.ViewModels;

namespace VG_web.Interface.ServiceInterfaces
{
    public interface IPropertyService
    {
        IEnumerable<PropertyViewModel> GetAll();

        PropertyViewModel GetById(int id);

        void Add(PropertyViewModel property);

        void Edit(PropertyViewModel property);

        void Delete(int id);
    }
}
