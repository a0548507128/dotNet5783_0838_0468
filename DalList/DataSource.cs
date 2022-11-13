
using DO;

using static DO.Enums;

namespace Dal;

static internal class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }

    #region arrays of entities
    static internal readonly Random rand = new Random();

    static internal Product[] Products =new Product[50];
    static internal Order[] Orders=new Order[100];
    static internal OrderItem[] OrderItems=new OrderItem[200];

    static private void AddProduct(string Name, double Price, Category Category, int InStock){
        Products[Config._productIndex] = new Product()
        {
            ID = Config.NumOfProduct,
            Name = Name,
            Price = Price,
            Category = Category,
            InStock = InStock
        };
        
        Config._productIndex++;
    }
    static private void AddOrder(string CustomerName, string CustomerEmail, string CustomerAdress, DateTime OrderDate, DateTime ShipDate, DateTime DeliveryrDate) {
        Orders[Config._orderIndex] = new Order()
        {
            ID = Config.NumOfOrder,
            CustomerName = CustomerName,
            CustomerEmail = CustomerEmail,
            CustomerAdress = CustomerAdress,
            OrderDate = OrderDate,
            ShipDate = ShipDate,
            DeliveryrDate = DeliveryrDate
        };
        Config._orderIndex++;
    }
    static private void AddOrderItem(int ProductID, int OrderID, double Price, int Amount){
        OrderItems[Config._orderIndex] = new OrderItem()
        {
            ID = Config.NumOfOrderItem,
            ProductID = ProductID,
            OrderID = OrderID,
            Price = Price,
            Amount = Amount
        };
        Config._orderItemIndex++;

    }
    #endregion

    #region initialize
    static private void s_Initialize(){

    }
    #endregion
    #region config
    static internal class Config
    {
        static internal int _productIndex = 0;
        static internal int _orderIndex = 0;
        static internal int _orderItemIndex = 0;
        
        static private int _numOfProduct = 100000;
        static private int _numOfOrder = 0;
        static private int _numOfOrderItem = 0;
        static public int NumOfProduct { get { return _numOfProduct++; } }
        static public int NumOfOrder { get { return _numOfOrder++; } }
        static public int NumOfOrderItem { get { return _numOfOrderItem++; } }

    }
    #endregion
}
