using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibDatabase.DatabaseContext;

namespace LibDatabase.Repository
{
    public class PropertyRepository : RepositoryBase<Property>, Interface.IPropertyRepository
    {
        public PropertyRepository(Entities context) : base(context)
        {
        }
    }
}
