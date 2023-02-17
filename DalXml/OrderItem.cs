using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dal;
using DalApi;
using DO;
internal class OrderItem : IOrderItem
{
    public int Add(DO.OrderItem IdAdd)
    {
        throw new NotImplementedException();
    }

    public void Delete(int IdDelete)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem? Get(int IdGet)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem?> GetAll(Predicate<DO.OrderItem?>? predict = null)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem? getOrderItemByPIDOID(int pid, int oid)
    {
        throw new NotImplementedException();
    }

    public int Update(DO.OrderItem IdUpdate)
    {
        throw new NotImplementedException();
    }
}
