
using DO;
using static Dal.DataSource;

namespace Dal;

public class DalOrder
{

    public int addOrder(Order _o)
    {
        _o.ID = Config.NumOfOrder;
        Orders[Config._orderIndex] = _o;
        return _o.ID;
    }
    public Order getOrder(int IdNum)
    {

        foreach (Order o in Orders)
        {
            if (o.ID == IdNum)
                return o;
        }
        throw new Exception("this order does not exist");
    }
    public Order[] getAllOrders()
    {
        Order[] _allOrders = new Order[Config._orderIndex];
        for (int i = 0; i < Config._orderIndex; i++)
        {
            _allOrders[i] = Orders[i];
        }
        return _allOrders;
    }
    public void deleteOrders(int IdNum)
    {
        for (int i = 0; i < Config._orderIndex; i++)
        {
            if (Orders[i].ID == IdNum)
            {
                Orders[i] = Orders[Config._orderIndex - 1];
                return;
            }
        }
        throw new Exception("this order doesn't exist");
    }

    public void updateOrder(Order upOrder)
    {
        for (int i = 0; i < Config._orderIndex; i++)
        {
            if (Orders[i].ID == upOrder.ID)
            {
                Orders[i] = upOrder;
                return;
            }
        }
        throw new Exception("this order doesn't exist");
    }
}
