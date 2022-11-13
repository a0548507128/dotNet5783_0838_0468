
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
        //////intilialize of users
        ////(string, string, string)[] users = { 
        ////    ("avi", "avi@gmail", "amaram gaon"), 
        ////    ("avigail", "avigail@gmail", "kordovero-3"),
        ////    ("shira", "shira@gmail", "nachal shacham-9") };
        //////intilialize of products (10)
        ////(string, double, Category, int)[] products = { 
        ////    ("rabit", 55, Enums.Category.Rodents, 80),
        ////};
        ////for(int i=0; i<20; i++)
        ////{

        ////}
        //AddProduct("rabbit", 55, Category.Reptiles, 80);     
        //AddProduct("small notebook", 30, Category.Reptiles, 0);       
        //AddProduct("campuse notebook",67, Category.Reptiles, 35);      
        //AddProduct("rabbit", 55, Category.Reptiles, 80);     
        //AddProduct("small notebook", 30, Category.Reptiles, 0);       
        //AddProduct("campuse notebook", 67, Category.Reptiles, 35);      
        //AddProduct("rabbit", 55, Category.Reptiles, 80);     
        //AddProduct("small notebook", 30, Category.Reptiles, 0);       
        //AddProduct("campuse notebook", 67, Category.Reptiles, 35);      
        //AddProduct("rabbit", 55, Category.Reptiles, 80);     
        //AddProduct("small notebook", 30, Category.Reptiles, 0);       
        //AddProduct("campuse notebook", 67, Category.Reptiles, 35);

        //////  addNewOrder()
        ////AddOrder(100000, 200001, 6.9, 3,DeliveryrDate,h);
        ////AddOrder(100002, 200001, 5.9, 1);
        ////AddOrder(100009, 200001, 7, 7);
        ////AddOrder(100010, 200001, 9.5, 2);

        ////AddOrder(100000, 200002, 6.9, 3);
        ////AddOrder(100002, 200002, 5.9, 1);
        ////AddOrder(100003, 200002, 4.9, 1);
        ////AddOrder(100004, 200002, 9.9, 2);

        ////AddOrder(100007, 200003, 6.9, 3);
        ////AddOrder(100002, 200003, 5.9, 1);
        ////AddOrder(100005, 200003, 5.6, 3);
        ////AddOrder(100008, 200003, 17, 2);

        ////AddOrder(100000, 200004, 6.9, 3);
        ////AddOrder(100002, 200004, 5.9, 1);
        ////AddOrder(100009, 200004, 7, 7);
        ////AddOrder(100010, 200004, 9.5, 2);

        ////AddOrder(100000, 200005, 6.9, 3);
        ////AddOrder(100002, 200005, 5.9, 1);
        ////AddOrder(100003, 200005, 4.9, 1);
        ////AddOrder(100004, 200005, 9.9, 2);

        ////AddOrder(100007, 200006, 6.9, 3);
        ////AddOrder(100002, 200006, 5.9, 1);
        ////AddOrder(100005, 200006, 5.6, 3);
        ////AddOrder(100008, 200006, 17, 2);

        ////AddOrder(100000, 200007, 6.9, 3);
        ////AddOrder(100002, 200007, 5.9, 1);
        ////AddOrder(100009, 200007, 7, 7);
        ////AddOrder(100010, 200007, 9.5, 2);

        ////AddOrder(100000, 200008, 6.9, 3);
        ////AddOrder(100002, 200008, 5.9, 1);
        ////AddOrder(100003, 200008, 4.9, 1);
        ////AddOrder(100004, 200008, 9.9, 2);

        ////AddOrder(100007, 200009, 6.9, 3);
        ////AddOrder(100002, 200009, 5.9, 1);
        ////AddOrder(100005, 200009, 5.6, 3);
        ////AddOrder(100008, 200009, 17, 2);

        ////AddOrder(100000, 200010, 6.9, 3);
        ////AddOrder(100002, 200010, 5.9, 1);
        ////AddOrder(100009, 200010, 7, 7);
        ////AddOrder(100010, 200010, 9.5, 2);

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
