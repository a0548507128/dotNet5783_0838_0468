using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Order:BlApi.IOrder
{
    private IDal dal = new DalList();

    public IEnumerable<BO.OrderForList> GetOrderList()
    {
    }
    public Order GetOrderDetails(int orderId)
    {

    }
    public Order OrderShippingUpdate(int numOrder) 
    {
      
    }
    public Order OrderDeliveryUpdate(int numOrder) 
    {
    }
    public Order OrderTracking(int numOrder) 
    {
    }

}
