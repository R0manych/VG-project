using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VG_web.ViewModels;

namespace VG_web.Interface.ServiceInterfaces
{
    public interface IMakerService
    {
        IEnumerable<MakerViewModel> GetAll();

        MakerViewModel GetById(int id);

        void Add(MakerViewModel maker);

        void Edit(MakerViewModel maker);

        void Delete(int id);

        void SetImage(int makerId, int imgId);

        void RemoveImage(int mkrId);

        MakerViewModel GetByImg(int id);
    }
}
