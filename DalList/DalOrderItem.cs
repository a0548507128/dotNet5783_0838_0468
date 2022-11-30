using DalApi;
using DO;
using static Dal.DataSource;

namespace Dal;

internal class DalOrderItem:IOrderItem
{

    public int Add(OrderItem _o)
    {
        _o.ID = Config.NumOfOrderItem;
        OrderItems.Add(_o);
        return _o.ID;
    }
    public OrderItem Get(int IdNum)
    {

        foreach (OrderItem o in OrderItems)
        {
            if (o.ID == IdNum)
                return o;
        }
        throw new Exception("this order-item does not exist");
    }
    public IEnumerable<OrderItem> GetAll(Func<OrderItem?, bool>? func = null)
    {
        List<OrderItem> _allOrderItems = new List<OrderItem>();
        for (int i = 0; i < OrderItems.Count; i++)
        {
            _allOrderItems[i] = OrderItems[i];
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
    public List<OrderItem> getOrderItemByOrder(int oid) 
    {
        int j = 0;
       List<OrderItem> allOrderItems= new List<OrderItem>();
        foreach (OrderItem o in OrderItems)
        {
            if (o.OrderID == oid)
                allOrderItems[j++] = o;
        }
        return allOrderItems;
    }
    public void Delete(int IdNum)
    {
        for (int i = 0; i < OrderItems.Count; i++)
        {
            if (OrderItems[i].ID == IdNum)
            {
                OrderItems.RemoveAt(IdNum);
                return;
            }
        }
        throw new Exception("this order-item doesn't exist");
    }
    public int Update(OrderItem upOrderItem)
    {
        for (int i = 0; i < OrderItems.Count; i++)
        {
            if (OrderItems[i].ID == upOrderItem.ID)
            {
                OrderItems[i] = upOrderItem;
                return upOrderItem.ID;
            }
        }
        throw new Exception("this order-item doesn't exist");
    }
}
