using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibDatabase.DatabaseContext;
using LibDatabase.Interface;

namespace LibDatabase.Repository
{
    public class SubcategoryRepository : RepositoryBase<Subcategory>, ISubcategoryRepository
    {
        public SubcategoryRepository(Entities context) : base(context)
        {
        }
        public Subcategory GetByImg(int ImgId)
        {
            return this.Entities.Where(c => c.ImageID == ImgId).FirstOrDefault();
        }
    }
}
