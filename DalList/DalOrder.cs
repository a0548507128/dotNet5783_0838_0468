using DalApi;
using DO;
using static Dal.DataSource;

namespace Dal;

internal class DalOrder:IOrder
{

    public int Add(Order _o)
    {
        _o.ID = Config.NumOfOrder;
        Orders.Add(_o);
        return _o.ID;
    }
    public Order? Get(int IdNum)
    {
            Order? o = Orders.FirstOrDefault(Order => Order?.ID == IdNum);
            if (o == null)
                throw new Exception("this order does not exist");
            return o;
        
    }
    public IEnumerable<Order?> GetAll(Predicate<Order?>? predict = null)
    {
        List<Order?> _allOrders = new ();
        if (predict == null)
        {
            _allOrders = Orders;
        }
        else
        {
            _allOrders = Orders.FindAll(x => predict(x));
        }
        return _allOrders;
    }
    public void Delete(int IdNum)
    {
        Orders.Remove((Orders.FirstOrDefault(item => item?.ID == IdNum))
           ?? throw new Exception("this order doesn't exist"));
    }
    public int Update(Order upOrder)
    {
        for (int i = 0; i < Orders.Count; i++)
        {
            if (Orders[i]?.ID == upOrder.ID)
            {
                Orders[i] = upOrder;
                return upOrder.ID;
            }
        }
        throw new Exception("this order doesn't exist");
    }
}
