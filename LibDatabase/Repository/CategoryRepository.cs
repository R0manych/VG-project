using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibDatabase.DatabaseContext;
using LibDatabase.Interface;

namespace LibDatabase.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    { 
        public CategoryRepository(Entities context) : base(context)
        {

        }
        public Category GetByImg(int ImgId)
        {
            return this.Entities.Where(c => c.ImageID == ImgId).FirstOrDefault();
        }
    }
}
