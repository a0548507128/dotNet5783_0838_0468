
using BO;
using DalApi;
using System.Runtime.CompilerServices;
using static BO.Enums;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.OrderForList?> GetOrderList(Func<DO.Order?, bool>? predict = null)
    {
        IEnumerable<DO.Order?> orderList = new List<DO.Order?>();
        IEnumerable<BO.OrderForList?> ordersForList = new List<BO.OrderForList?>();
        orderList = dal!.Order.GetAll();
        ordersForList = from DO.Order order in orderList
                        where (predict == null || predict(order))
                        select new BO.OrderForList()
                        {
                            OrderID = order.ID,
                            CustomerName = order.CustomerName,
                            Status = CheckStatus(order.OrderDate, order.ShipDate, order.DeliveryDate),
                            AmountOfItem = GetAmountItems(order.ID),
                            TotalSum = CheckTotalSum(order.ID)
                        };

        return ordersForList;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order GetOrderDetails(int id)
    {
        if (id <= 0)
        {
            throw new BO.NegativeIdException("negative id") { NegativeId = id.ToString() };
        }
        else
        {
            DO.Order o = new DO.Order();
            try
            {
                o = (DO.Order)dal!.Order.Get(id)!;
            }
            catch (DO.EntityNotFound)
            {
                throw new BO.OrderNotExistsException("order not exists") { OrderNotExists = o.ToString() };

            }
            return DOorderToBOorder(o);


        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order OrderShippingUpdate(int numOrder)
    {
        DO.Order o = new DO.Order();
        try
        {
            o = (DO.Order)dal!.Order.Get(numOrder)!;
        }
        catch (DO.EntityNotFound)
        {
            throw new BO.OrderNotExistsException("order not exists") { OrderNotExists = o.ToString() };
        }
        try
        {
            if (CheckStatus(o.OrderDate, o.ShipDate, o.DeliveryDate) == EStatus.Done)
            {
                o.ShipDate = DateTime.Now;
                try
                {
                    dal.Order.Update(o);
                }
                catch (DO.EntityNotFound)
                {
                    throw new BO.UpdateOrderNotSucceedException("update order not succeed") { UpdateOrderNotSucceed = o.ToString() };
                }
            }
        }
        catch
        {
            throw new BO.OrderHasAlreadySentException("Order has already sent") { OrderHasAlreadySent = o.ToString() };
        }
        return DOorderToBOorder(o); ;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order OrderDeliveryUpdate(int numOrder)
    {
        DO.Order o = new DO.Order();
        try
        {
            o = (DO.Order)dal!.Order.Get(numOrder)!;
        }
        catch
        {
            throw new BO.OrderNotExistsException("order not exists") { OrderNotExists = o.ToString() };

        }
        try
        {
            if (CheckStatus(o.OrderDate, o.ShipDate, o.DeliveryDate) == BO.Enums.EStatus.Sent)
            {
                o.DeliveryDate = DateTime.Now;
                try
                {
                    dal.Order.Update(o);
                }
                catch (DO.EntityNotFound)
                {

                    throw new BO.UpdateOrderNotSucceedException("update order not succeed") { UpdateOrderNotSucceed = o.ToString() };
                }
            }
        }
        catch
        {
            throw new BO.OrderHasAlreadyProvidedException("Order has already sent") { OrderHasAlreadyProvided = o.ToString() };

        }
        return DOorderToBOorder(o); ;

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.OrderTracking OrderTracking(int numOrder)
    {
        DO.Order? o = new DO.Order();
        try
        {
            o= dal!.Order.Get(numOrder)!;
        }
        catch (Exception)
        {
            throw new BO.OrderNotExistsException("order not exists") { OrderNotExists = o.ToString()! };

        }
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        orderTracking.ID = numOrder;
        orderTracking.Status = CheckStatus(o?.OrderDate, o?.ShipDate, o?.DeliveryDate);
        switch (orderTracking.Status)
        {
            case EStatus.Done:
                orderTracking.listOfStatus = new();
                orderTracking.listOfStatus.Add(new()
                {
                    Date = o?.OrderDate,
                    Status = EStatus.Done
                });
                break;
            case EStatus.Sent:
                orderTracking.listOfStatus = new();
                orderTracking.listOfStatus.Add(new()
                {
                    Date = (DateTime?)o?.OrderDate,
                    Status = BO.Enums.EStatus.Done
                });
                orderTracking.listOfStatus = new();
                orderTracking.listOfStatus.Add(new()
                {
                    Date = (DateTime?)o?.ShipDate,
                    Status = BO.Enums.EStatus.Sent
                });
                break;

            case EStatus.Provided:
                orderTracking.listOfStatus = new();
                orderTracking.listOfStatus.Add(new()
                {
                    Date = (DateTime?)o?.OrderDate,
                    Status = BO.Enums.EStatus.Done
                });
                orderTracking.listOfStatus = new();
                orderTracking.listOfStatus.Add(new()
                {
                    Date = (DateTime?)o?.ShipDate,
                    Status = BO.Enums.EStatus.Sent
                });
                orderTracking.listOfStatus = new();
                orderTracking.listOfStatus.Add(new()
                {
                    Date = (DateTime?)o?.DeliveryDate,
                    Status = BO.Enums.EStatus.Provided
                });
                break;
        }
        return orderTracking;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int? SelectingAnOrderForTreatment()
    {
        DateTime minDate = DateTime.Now;
        int? orderId = null;
        List<OrderForList>? orderList = GetOrderList().ToList()!;
        orderList?.ForEach(o =>

        {
            switch (o.Status)
            {
                case EStatus.Done:
                    if (GetOrderDetails(o.OrderID).OrderDate < minDate)
                    {
                        orderId = o.OrderID;
                        minDate = (DateTime)GetOrderDetails(o.OrderID).OrderDate!;
                    }
                    break;
                case EStatus.Sent:
                    if (GetOrderDetails(o.OrderID).ShipDate < minDate)
                    {
                        orderId = o.OrderID;
                        minDate = (DateTime)GetOrderDetails(o.OrderID).ShipDate!;
                    }
                    break;
                default:
                    break;
            }
        });
        return orderId;
    }
    #region ezer
    public BO.Order DOorderToBOorder(DO.Order o)
    {
        BO.Order newOrder = new BO.Order()
        {
            ID = o.ID,
            CustomerName = o.CustomerName,
            CustomerEmail = o.CustomerEmail,
            CustomerAdress = o.CustomerAdress,
            Status = CheckStatus(o.OrderDate, o.ShipDate, o.DeliveryDate),
            OrderDate = o.OrderDate,
            ShipDate = o.ShipDate,
            DeliveryDate = o.DeliveryDate,
            ItemList = GetAllItemsToOrder(o.ID),
            TotalSum = CheckTotalSum(o.ID)


        };
        return newOrder;
    }
    public EStatus CheckStatus(DateTime? OrderDate, DateTime? ShipDate, DateTime? DeliveryDate)
    {
        if (DeliveryDate == null && ShipDate == null)
        {
            return EStatus.Done;
        }
        else if(DeliveryDate==null&& ShipDate != null)
        {
            return EStatus.Sent;
        }
        else
         return EStatus.Provided;
    }
    public int GetAmountItems(int id)
    {
        IEnumerable<DO.OrderItem?> orderItemList = new List<DO.OrderItem?>();
        orderItemList = dal!.OrderItem.GetAll();
        var items = orderItemList.Where(item => item != null && item.Value.OrderID==id);
        int sum = items.Count();
        if (sum == 0)
            throw new Exception();
        return sum;

    }
    public double CheckTotalSum(int id)
    {
        IEnumerable<DO.OrderItem?> orderItemList = new List<DO.OrderItem?>();
        orderItemList = dal!.OrderItem.GetAll();
        var items = orderItemList.Where(item => item != null && item.Value.OrderID == id);
        double totalPrice = items.Sum(x => x!.Value.Amount * x.Value.Price);
        return totalPrice;
    }
    public List<BO.OrderItem?> GetAllItemsToOrder(int id)
    {
        IEnumerable<DO.OrderItem?> orderItemList = new List<DO.OrderItem?>();
        IEnumerable<BO.OrderItem?> BOorderItemList = new List<BO.OrderItem?>();
        orderItemList = dal!.OrderItem.GetAll();
        int count = 0;
        BOorderItemList = (from item in orderItemList
                           where (item != null && item?.OrderID == id)
                           select new BO.OrderItem()
                           {
                               NumInOrder = count++,
                               ID = item.Value.ID,
                               Name = getOrderItemName(item.Value.ProductID),
                               Price = item.Value.Price,
                               Amount = item.Value.Amount,
                               SumItem = item.Value.Price * item.Value.Amount
                           }).ToList();
        return (List<BO.OrderItem?>)BOorderItemList;

    }
    public string getOrderItemName(int productId)
    {
        DO.Product product = new DO.Product();
        product = (DO.Product)dal!.Product.Get(productId)!;
        return product.Name!;
    }
    #endregion

    
}
