using LibDatabase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibDatabase.DatabaseContext;

namespace LibDatabase.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Entities context;

        public UnitOfWork(Entities context)
        {
            this.context = context;
        }

        /*public UnitOfWork()
        {
            context = new Entities();
        }*/

        public void Save()
        {
            context.SaveChanges();
        }     
    }
}
