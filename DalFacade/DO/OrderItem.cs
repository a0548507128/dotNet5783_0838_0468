﻿

namespace DO;

public struct OrderItem
{
    public int ProductID { get; set; }
    public int OrderID { get; set; }
    public int Price { get; set; }
    public int Amount { get; set; }

    public override string ToString() => $@"
        Product ID={ProductID}.
        Order ID: {OrderID}.
    	Price: {Price}.
    	Amount: {Amount}.
       ";

}
