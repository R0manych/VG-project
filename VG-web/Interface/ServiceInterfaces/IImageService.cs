using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VG_web.ViewModels;

namespace VG_web.Interface.ServiceInterfaces
{
    public interface IImageService
    {
        IEnumerable<ImageViewModel> GetAll();

        ImageViewModel GetById(int id);

        void Add(ImageViewModel image);

        void Edit(ImageViewModel image);

        void Delete(int id);
    }
}
