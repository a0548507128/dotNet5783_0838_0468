
using DO;
using static Dal.DataSource;

namespace Dal;

public class DalOrderItem
{

    public int addOrderItem(OrderItem _o)
    {
        _o.ID = Config.NumOfOrderItem;
        OrderItems[Config._orderItemIndex] = _o;
        return _o.ID;
    }
    public OrderItem GetOrderItem(int IdNum)
    {

        foreach (OrderItem o in OrderItems)
        {
            if (o.ID == IdNum)
                return o;
        }
        throw new Exception("this order-item does not exist");
    }
    public OrderItem[] getAllOrderItems()
    {
        OrderItem[] _allOrderItems = new OrderItem[Config._orderItemIndex];
        for (int i = 0; i < Config._orderItemIndex; i++)
        {
            _allOrderItems[i] = _allOrderItems[i];
        }
        return _allOrderItems;
    }

    public OrderItem getOrderItemByPIDOID(int pid,int oid)
    {
        foreach(OrderItem o in OrderItems)
        {
            if(o.ProductID==pid&&o.OrderID==o.OrderID)
                return o;
        }
        throw new Exception("this orderitem doesn't exist");
    }
    public OrderItem[] getOrderItemByOrder(int oid) 
    {
        int j = 0;
        OrderItem[] allOrderItems=new OrderItem[Config._orderItemIndex];
        foreach (OrderItem o in OrderItems)
        {
            if (o.OrderID == oid)
                allOrderItems[j++] = o;
        }
        return allOrderItems;
    }
    public void deleteOrderItem(int IdNum)
    {
        for (int i = 0; i < Config._orderItemIndex; i++)
        {
            if (OrderItems[i].ID == IdNum)
            {
                OrderItems[i] = OrderItems[Config._orderItemIndex - 1];
                return;
            }
        }
        throw new Exception("this order-item doesn't exist");
    }

    public void updateOrderItem(OrderItem upOrderItem)
    {
        for (int i = 0; i < Config._orderItemIndex; i++)
        {
            if (OrderItems[i].ID == upOrderItem.ID)
            {
                OrderItems[i] = upOrderItem;
                return;
            }
        }
        throw new Exception("this order-item doesn't exist");
    }
}
