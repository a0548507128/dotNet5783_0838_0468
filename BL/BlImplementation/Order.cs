
using DalApi;
using static BO.Enums;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    public IEnumerable<BO.OrderForList> GetOrderList(Func<DO.Order?, bool>? predict = null)
    {
        IEnumerable<DO.Order?> orderList = new List<DO.Order?>();
        List<BO.OrderForList> ordersForList = new List<BO.OrderForList>();
        orderList = dal.Order.GetAll();
        //IEnumerable<BO.OrderTracking> orderTracking = new List<BO.OrderTracking>();
        foreach (var item in orderList)
        {
            if (item != null)
            {
                ordersForList.Add(new BO.OrderForList()
                {
                    OrderID = item.Value.ID,
                    CustomerName = item.Value.CustomerName,
                    Status = CheckStatus(item?.OrderDate, item?.ShipDate, item?.DeliveryrDate),
                    AmountOfItem = GetAmountItems(item.Value.ID),
                    TotalSum = CheckTotalSum(item.Value.ID)
                });
            }
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
                    Date = (DateTime)o.OrderDate,
                    Status = BO.Enums.EStatus.Done
                });
                break;
            case EStatus.Sent:
                ((List<BO.OrderTracking.StatusAndDate>)orderTracking.listOfStatus).Add(new BO.OrderTracking.StatusAndDate()
                {
                    Date = (DateTime)o.OrderDate,
                    Status = BO.Enums.EStatus.Done
                });
                ((List<BO.OrderTracking.StatusAndDate>)orderTracking.listOfStatus).Add(new BO.OrderTracking.StatusAndDate()
                {
                    Date = (DateTime)o.ShipDate,
                    Status = BO.Enums.EStatus.Sent

                });
                break;
            case EStatus.Provided:
                ((List<BO.OrderTracking.StatusAndDate>)orderTracking.listOfStatus).Add(new BO.OrderTracking.StatusAndDate()
                {
                    Date = (DateTime)o.OrderDate,
                    Status = BO.Enums.EStatus.Done
                });
                ((List<BO.OrderTracking.StatusAndDate>)orderTracking.listOfStatus).Add(new BO.OrderTracking.StatusAndDate()
                {
                    Date = (DateTime)o.ShipDate,
                    Status = BO.Enums.EStatus.Sent

                });
                ((List<BO.OrderTracking.StatusAndDate>)orderTracking.listOfStatus).Add(new BO.OrderTracking.StatusAndDate()
                {
                    Date = (DateTime)o.DeliveryrDate,
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
    public EStatus CheckStatus(DateTime? OrderDate, DateTime? ShipDate, DateTime? DeliveryDate)
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
        IEnumerable<DO.OrderItem?> orderItemList = new List<DO.OrderItem?>();
        orderItemList = dal.OrderItem.GetAll();
        int sum = 0;
        foreach (var item in orderItemList)
        {
            if (item != null)
                sum += item.Value.Amount;
        }
        return sum;

    }
    public double CheckTotalSum(int id)
    {
        IEnumerable<DO.OrderItem?> orderItemList = new List<DO.OrderItem?>();
        orderItemList = dal.OrderItem.GetAll();
        double sum = 0;
        foreach (var item in orderItemList)
        {
            if(item != null)
                sum = sum + item.Value.Price * item.Value.Amount;
        }
        return sum;
    }
    public List<BO.OrderItem> GetAllItemsToOrder(int id)
    {
        IEnumerable<DO.OrderItem?> orderItemList = new List<DO.OrderItem?>();
        List<BO.OrderItem> BOorderItemList = new List<BO.OrderItem>();
        orderItemList = dal.OrderItem.GetAll();
        int count = 0;
        foreach (var item in orderItemList)
        {
            BOorderItemList.Add(new BO.OrderItem()
            {
                numInOrder = count++,
                ID = item.Value.ID,
                Name = getOrderItemName(item.Value.ProductID),
                Price = item.Value.Price,
                Amount = item.Value.Amount,
                sumItem = item.Value.Price * item.Value.Amount

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
