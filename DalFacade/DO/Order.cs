

using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

public struct Order
{
    public int ID { get; set; }
    public int CustomerName { get; set; }
    public int CustomerEmail { get; set; }
    public int CustomerAdress { get; set; }
    public int OrderDate { get; set; }
    public int ShipDate { get; set; }
    public int DeliveryrDate { get; set; }

    public override string ToString() => $@"
        Order ID={ID},
        User information: {CustomerName}, {CustomerEmail}, {CustomerAdress}
    	Order date: {OrderDate}.
    	Ship date: {ShipDate}.
        Deliveryr date: {DeliveryrDate}.
       ";

}
