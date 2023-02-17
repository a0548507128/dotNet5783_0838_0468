using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class Order : IOrder
    {
        public int Add(DO.Order IdAdd)
        {
            throw new NotImplementedException();
        }

        public void Delete(int IdDelete)
        {
            throw new NotImplementedException();
        }

        public DO.Order? Get(int IdGet)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DO.Order?> GetAll(Predicate<DO.Order?>? predict = null)
        {
            throw new NotImplementedException();
        }

        public int Update(DO.Order IdUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
