using LibDatabase.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VG_web.ViewModels;

namespace VG_web.Interface.ServiceInterfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryViewModel> GetAll();

        CategoryViewModel GetById(int id);

        void Add(CategoryViewModel categoryViewModel);

        void Edit(CategoryViewModel categoryViewModel);

        void Delete(int id);

        void SetImage(int catId, int imgId);

        void RemoveImage(int catId);

        CategoryViewModel GetByImg(int id);
    }
}
