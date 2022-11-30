using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IOrderItem : ICrud<OrderItem>
    {
        public IEnumerable<OrderItem>? GetAll(Func<OrderItem?, bool>? func = null);
        public List<OrderItem>? getOrderItemByOrder(int oid);
        public OrderItem getOrderItemByPIDOID(int pid, int oid);
    }
}
