using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibDatabase.DatabaseContext;
using LibDatabase.Interface;

namespace LibDatabase.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(Entities context) : base(context)
        {
        }        

        public Product GetByImg(int ImgId)
        {
            return this.Entities.Where(c => c.ImageID == ImgId).FirstOrDefault();
        }
       
    }
}
