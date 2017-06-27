using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibDatabase.DatabaseContext;

namespace LibDatabase.Repository
{
    public class SubcategoryPropertyRepository : RepositoryBase<SubcategoryProperty>, Interface.ISubcategoryPropertyRepository
    {
        public SubcategoryPropertyRepository(Entities context) : base(context)
        {
        }       
    }
}
