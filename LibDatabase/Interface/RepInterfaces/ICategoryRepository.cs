using LibDatabase.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibDatabase.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByImg(int ImgId);
    }
}
