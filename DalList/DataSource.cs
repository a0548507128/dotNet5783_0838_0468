
using DO;

namespace Dal;

static internal class DataSource
{
    static internal readonly Random rand = new Random();

    static internal Product[] Products =new Product[50];
    static internal Order[] Orders=new Order[100];
    static internal OrderItem[] OrderItems=new OrderItem[200];

    static private void AddProduct(int ID, string Name, int Price, int Category, int InStock){

    }
    static private void AddOrder(int ID, string CustomerName, string CustomerEmail, string CustomerAdress, DateTime OrderDate, DateTime ShipDate, DateTime DeliveryrDate) {

    }
    static private void AddOrderItem(int ProductID, int OrderID, int Price, int Amount){

    }
     
}
