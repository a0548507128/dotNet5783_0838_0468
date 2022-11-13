
namespace DO;

public struct Order
{ 
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryrDate { get; set; }
    public override string ToString() => $@"
        Order ID={ID},
        User information: {CustomerName}, {CustomerEmail}, {CustomerAdress}
    	Order date: {OrderDate}.
    	Ship date: {ShipDate}.
        Deliveryr date: {DeliveryrDate}.
       ";

}
