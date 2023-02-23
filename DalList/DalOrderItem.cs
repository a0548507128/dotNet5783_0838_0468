﻿using DalApi;
using DO;
using System;
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
    public OrderItem? Get(int IdNum)
    {
        OrderItem? o = OrderItems.FirstOrDefault(OrderItem => OrderItem?.ID == IdNum);
        if (o == null)
            throw new Exception("this order-item does not exist");
        return o;
    }
    public IEnumerable<OrderItem?> GetAll(Predicate<OrderItem?>? predict = null)
    { 
        List<OrderItem?> _allOrderItems = new();
        if (predict == null)
        {
            _allOrderItems = OrderItems;
        }
        else
        {
            _allOrderItems = OrderItems.FindAll(x => predict(x));
        }
        return _allOrderItems;
    }
    //public OrderItem? getOrderItemByPIDOID(int pid,int oid)
    //{
    //    OrderItem? o = OrderItems.FirstOrDefault(OrderItem => OrderItem?.ProductID == pid&& OrderItem?.OrderID == oid);
    //    if (o == null)
    //        throw new Exception("this orderitem doesn't exist");
    //    return o;
    //}
    public List<OrderItem?> getOrderItemByOrder(int oid)
    {
        int j = 0;
        List<OrderItem?> allOrderItems = new List<OrderItem?>();
        var v = from o in OrderItems
                where o?.OrderID == oid
                select allOrderItems[j++] = o;

        return allOrderItems;
    }
    public void Delete(int IdNum)
    {
        OrderItems.Remove((OrderItems.FirstOrDefault(item => item?.ID == IdNum))
           ?? throw new Exception("this order-item doesn't exist"));
    }
    public int Update(OrderItem upOrderItem)
    {
        for (int i = 0; i < OrderItems.Count; i++)
        {
            if (OrderItems[i]?.ID == upOrderItem.ID)
            {
                OrderItems[i] = upOrderItem;
                return upOrderItem.ID;
            }
        }
        throw new Exception("this order-item doesn't exist");
    }
}
