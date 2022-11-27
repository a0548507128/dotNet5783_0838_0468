using Dal;
using DalApi;
using static BO.Enums;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private IDal dal = new DalList();

    public IEnumerable<BO.OrderForList> GetOrderList()
    {
        IEnumerable<DO.Order> orderList = new List<DO.Order>();
        List<BO.OrderForList> ordersForList = new List<BO.OrderForList>();
        orderList = dal.Order.getAllOrders();
        IEnumerable<BO.OrderTracking> orderTracking = new List<BO.OrderTracking>();
        foreach (var item in orderList)
        {
            ordersForList.Add(new BO.OrderForList()
            {
                OrderID = item.ID,
                CustomerName = item.CustomerName,
                Status = CheckStatus(item.OrderDate, item.ShipDate, item.DeliveryrDate),
                AmountOfItem = GetAmountItems(item.ID),
                TotalSum = CheckTotalSum(item.ID)
            });

        }
        return ordersForList;
    }
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
                o = dal.Order.Get(id);
            }
            catch (DO.EntityNotFound)
            {
                throw new BO.OrderNotExistsException("order not exists") { OrderNotExists = o.ToString() };

            }
            return DOorderToBOorder(o);


        }
    }
    public BO.Order OrderShippingUpdate(int numOrder)
    {
        DO.Order o = new DO.Order();
        try
        {
            o = dal.Order.Get(numOrder);
        }
        catch (DO.EntityNotFound)
        {
            throw new BO.OrderNotExistsException("order not exists") { OrderNotExists = o.ToString() };
        }
        try
        {
            if (CheckStatus(o.OrderDate, o.ShipDate, o.DeliveryrDate) == EStatus.Done)
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
    public BO.Order OrderDeliveryUpdate(int numOrder)
    {
        DO.Order o = new DO.Order();
        try
        {
            o = dal.Order.Get(numOrder);
        }
        catch
        {
            throw new BO.OrderNotExistsException("order not exists") { OrderNotExists = o.ToString() };

        }
        try
        {
            if (CheckStatus(o.OrderDate, o.ShipDate, o.DeliveryrDate) == BO.Enums.EStatus.Sent)
            {
                o.DeliveryrDate = DateTime.Now;
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
    public BO.OrderTracking OrderTracking(int numOrder)
    {
        DO.Order o = new DO.Order();
        try
        {
            o = dal.Order.Get(numOrder);
        }
        catch (DO.EntityNotFound)
        {
            throw new BO.OrderNotExistsException("order not exists") { OrderNotExists = o.ToString() };

        }
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        orderTracking.ID = numOrder;
        orderTracking.Status = CheckStatus(o.OrderDate, o.ShipDate, o.DeliveryrDate);
        switch (orderTracking.Status)
        {
            case EStatus.Done:
                ((List<BO.OrderTracking.StatusAndDate>)(orderTracking.listOfStatus)).Add(new BO.OrderTracking.StatusAndDate()
                {
                    Date = o.OrderDate,
                    Status = BO.Enums.EStatus.Done
                });
                break;
            case EStatus.Sent:
                ((List<BO.OrderTracking.StatusAndDate>)orderTracking.listOfStatus).Add(new BO.OrderTracking.StatusAndDate()
                {
                    Date = o.OrderDate,
                    Status = BO.Enums.EStatus.Done
                });
                ((List<BO.OrderTracking.StatusAndDate>)orderTracking.listOfStatus).Add(new BO.OrderTracking.StatusAndDate()
                {
                    Date = o.ShipDate,
                    Status = BO.Enums.EStatus.Sent

                });
                break;
            case EStatus.Provided:
                ((List<BO.OrderTracking.StatusAndDate>)orderTracking.listOfStatus).Add(new BO.OrderTracking.StatusAndDate()
                {
                    Date = o.OrderDate,
                    Status = BO.Enums.EStatus.Done
                });
                ((List<BO.OrderTracking.StatusAndDate>)orderTracking.listOfStatus).Add(new BO.OrderTracking.StatusAndDate()
                {
                    Date = o.ShipDate,
                    Status = BO.Enums.EStatus.Sent

                });
                ((List<BO.OrderTracking.StatusAndDate>)orderTracking.listOfStatus).Add(new BO.OrderTracking.StatusAndDate()
                {
                    Date = o.DeliveryrDate,
                    Status = BO.Enums.EStatus.Provided

                });
                break;
        }
        return orderTracking;
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
            Status = CheckStatus(o.OrderDate, o.ShipDate, o.DeliveryrDate),
            OrderDate = o.OrderDate,
            ShipDate = o.ShipDate,
            DeliveryDate = o.DeliveryrDate,
            ItemList = GetAllItemsToOrder(o.ID),
            TotalSum = CheckTotalSum(o.ID)


        };
        return newOrder;
    }
    public EStatus CheckStatus(DateTime OrderDate, DateTime ShipDate, DateTime DeliveryDate)
    {
        DateTime today = DateTime.Now;
        if (today.Equals(OrderDate) && today.Equals(ShipDate) && today.Equals(DeliveryDate))
            return EStatus.Provided;
        else if (today.Equals(OrderDate) && today.Equals(ShipDate))
            return EStatus.Sent;
        else
            return EStatus.Done;
    }
    public int GetAmountItems(int id)
    {
        IEnumerable<DO.OrderItem> orderItemList = new List<DO.OrderItem>();
        orderItemList = dal.OrderItem.getAllOrderItems();
        int sum = 0;
        foreach (var item in orderItemList)
        {
            sum += item.Amount;
        }
        return sum;

    }
    public double CheckTotalSum(int id)
    {
        IEnumerable<DO.OrderItem> orderItemList = new List<DO.OrderItem>();
        orderItemList = dal.OrderItem.getAllOrderItems();
        double sum = 0;
        foreach (var item in orderItemList)
        {
            sum = sum + item.Price * item.Amount;
        }
        return sum;
    }
    public List<BO.OrderItem> GetAllItemsToOrder(int id)
    {
        IEnumerable<DO.OrderItem> orderItemList = new List<DO.OrderItem>();
        List<BO.OrderItem> BOorderItemList = new List<BO.OrderItem>();
        orderItemList = dal.OrderItem.getAllOrderItems();
        int count = 0;
        foreach (var item in orderItemList)
        {
            BOorderItemList.Add(new BO.OrderItem()
            {
                numInOrder = count++,
                ID = item.ID,
                Name = getOrderItemName(item.ProductID),
                Price = item.Price,
                Amount = item.Amount,
                sumItem = item.Price * item.Amount

            });
        }
        return BOorderItemList;

    }
    public string getOrderItemName(int productId)
    {
        DO.Product product = new DO.Product();
        product = dal.Product.Get(productId);
        return product.Name;
    }
    #endregion


}
