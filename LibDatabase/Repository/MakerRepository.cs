using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibDatabase.DatabaseContext;

namespace LibDatabase.Repository
{
    public class MakerRepository : RepositoryBase<Maker>, Interface.IMakerRepository
    {
        public MakerRepository(Entities context) : base(context)
        {
        }       

        public Maker GetByImg(int ImgId)
        {
            return this.Entities.Where(c => c.ImageID == ImgId).FirstOrDefault();
        }
    }
}
