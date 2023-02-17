using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class Product : IProduct
    {
        public int Add(DO.Product IdAdd)
        {
            throw new NotImplementedException();
        }

        public void Delete(int IdDelete)
        {
            throw new NotImplementedException();
        }

        public DO.Product? Get(int IdGet)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DO.Product?> GetAll(Predicate<DO.Product?>? predict = null)
        {
            throw new NotImplementedException();
        }

        public int Update(DO.Product IdUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
