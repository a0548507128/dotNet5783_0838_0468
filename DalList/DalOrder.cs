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
    public Order Get(int IdNum)
    {

        foreach (Order o in Orders)
        {
            if (o.ID == IdNum)
                return o;
        }
        throw new Exception("this order does not exist");
    }
    public IEnumerable<Order> GetAll(Func<Order?, bool>? func = null)
    {
        List<Order> _allOrders = new List<Order>();
        for (int i = 0; i < Orders.Count; i++)
        {
            _allOrders[i] = Orders[i];
        }
        return _allOrders;
    }
    public void Delete(int IdNum)
    {
        for (int i = 0; i < Orders.Count; i++)
        {
            if (Orders[i].ID == IdNum)
            {
                Orders.RemoveAt(IdNum);
                return;
            }
        }
        throw new Exception("this order doesn't exist");
    }

    public int Update(Order upOrder)
    {
        for (int i = 0; i < Orders.Count; i++)
        {
            if (Orders[i].ID == upOrder.ID)
            {
                Orders[i] = upOrder;
                return upOrder.ID;
            }
        }
        throw new Exception("this order doesn't exist");
    }
}
