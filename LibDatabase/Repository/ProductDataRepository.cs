using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibDatabase.DatabaseContext;

namespace LibDatabase.Repository
{
    public class ProductDataRepository : RepositoryBase<ProductData>, Interface.IProductDataRepository
    {
        public ProductDataRepository(Entities context) : base(context)
        {
        }
    }
}
