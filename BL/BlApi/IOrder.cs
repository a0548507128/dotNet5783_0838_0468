using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

public interface IOrder
{
    public IEnumerable<OrderForList> GetOrderList();//for manager
    public BO.Order GetOrderDetails(int orderId);//for manager and customer
    public BO.Order OrderShippingUpdate(int numOrder);//for manager
    public BO.Order OrderDeliveryUpdate(int numOrder);//for manager
    public BO.OrderTracking OrderTracking(int numOrder);//for manager

}
