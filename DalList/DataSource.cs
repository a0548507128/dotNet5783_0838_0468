
using DO;

using static DO.Enums;

namespace Dal;

static internal class DataSource
{
    public static void Debug() { }
    static DataSource()
    {
        s_Initialize();
    }

    #region arrays of entities
    static internal readonly Random rand = new Random();

    static internal List<Product> Products = new List<Product>();
    static internal List<Order> Orders = new List<Order>();
    static internal List<OrderItem> OrderItems = new List<OrderItem>();

    static private void AddProduct(string Name, double Price, Category Category, int InStock){
        Products.Add(new Product()
        {
            ID = Config.NumOfProduct,
            Name = Name,
            Price = Price,
            Category = Category,
            InStock = InStock
        });
    }
    static private void AddOrder(string CustomerName, string CustomerEmail, string CustomerAdress, DateTime OrderDate, DateTime ShipDate, DateTime DeliveryrDate) {
        Orders.Add(new Order()
        {
            ID = Config.NumOfOrder,
            CustomerName = CustomerName,
            CustomerEmail = CustomerEmail,
            CustomerAdress = CustomerAdress,
            OrderDate = OrderDate,
            ShipDate = ShipDate,
            DeliveryrDate = DeliveryrDate
        });

    }
    static private void AddOrder(string CustomerName, string CustomerEmail, string CustomerAdress)
    {
        DateTime _today = DateTime.Now;
        int daysAgo = new Random().Next(600);
        DateTime NewOrderDate = _today.AddDays(-daysAgo);
        int daysbetweenOrderToShip = new Random().Next(10);
        DateTime newShipDate = NewOrderDate.AddDays(daysbetweenOrderToShip);
        int daysbetweenDeliveryToShip = new Random().Next(7);
        DateTime newDeliveryDate = newShipDate.AddDays(daysbetweenDeliveryToShip);
        AddOrder(CustomerName, CustomerEmail, CustomerAdress, NewOrderDate, newShipDate, newDeliveryDate);
    }
    static private void AddOrderItem(int ProductID, int OrderID, double Price, int Amount){
        OrderItems.Add(new OrderItem()
        {
            ID = Config.NumOfOrderItem,
            ProductID = ProductID,
            OrderID = OrderID,
            Price = Price,
            Amount = Amount
        });
    }
    #endregion
    #region initialize
    static private void s_Initialize(){
        #region AddProduct
        AddProduct("rabbit", 55, Category.Rodents, 80);//100000
        AddProduct("hamster", 20, Category.Rodents, 50);//100001
        AddProduct("snake", 60, Category.Reptiles, 35);//100002
        AddProduct("turtle", 15, Category.Reptiles, 20);//100003
        AddProduct("poodle dog", 9500, Category.CatsAndDogs, 20);//100004
        AddProduct("Russian blue cat", 1200, Category.CatsAndDogs, 40);//100005
        AddProduct("gold fish", 10, Category.Fish, 30);//100006
        AddProduct("fighting fish", 17, Category.Fish, 0);//100007
        AddProduct("Cockatiel parrot", 110, Category.Birds, 12);//100008
        AddProduct("Jaco parrot",3000, Category.Birds, 6);//100009
        #endregion
        #region AddOrder
        Random rnd = new Random();
        AddOrder("Avi", "avi@gmail", "amaram gaon", DateTime.Now.AddDays(-(rnd.Next(9))), DateTime.Now, DateTime.MinValue);
        AddOrder("Shira", "shira137@gmail", "nachal shacham");
        AddOrder("Tamar", "tamar@gmail", "nachal micha");
        AddOrder("Netanel", "n6517@gmail", "kordovaro");
        AddOrder("Asher", "a123@gmail", "nachal shacham", DateTime.Now.AddDays(-(rnd.Next(9))), DateTime.Now, DateTime.MinValue);
        AddOrder("Shira", "shira137@gmail", "nachal shacham", DateTime.Now.AddDays(-(rnd.Next(9))), DateTime.Now, DateTime.MinValue);
        AddOrder("Avi", "avi@gmail", "amaram gaon");
        AddOrder("Netanel", "n6517@gmail", "kordovaro", DateTime.Now.AddDays(-(rnd.Next(9))), DateTime.Now, DateTime.MinValue);
        AddOrder("Asher", "a123@gmail", "nachal shacham");
        AddOrder("Tamar", "tamar@gmail", "nachal micha", DateTime.Now.AddDays(-(rnd.Next(9))), DateTime.Now, DateTime.MinValue);
        AddOrder("Avi", "avi@gmail", "amaram gaon", DateTime.Now.AddDays(-(rnd.Next(9))), DateTime.Now, DateTime.MinValue);
        AddOrder("Shira", "shira137@gmail", "nachal shacham");
        AddOrder("Tamar", "tamar@gmail", "nachal micha");
        AddOrder("Netanel", "n6517@gmail", "kordovaro");
        AddOrder("Asher", "a123@gmail", "nachal shacham", DateTime.Now.AddDays(-(rnd.Next(9))), DateTime.Now, DateTime.MinValue);
        AddOrder("Shira", "shira137@gmail", "nachal shacham", DateTime.Now.AddDays(-(rnd.Next(9))), DateTime.Now, DateTime.MinValue);
        AddOrder("Avi", "avi@gmail", "amaram gaon");
        AddOrder("Netanel", "n6517@gmail", "kordovaro", DateTime.Now.AddDays(-(rnd.Next(9))), DateTime.Now, DateTime.MinValue);
        AddOrder("Asher", "a123@gmail", "nachal shacham");
        AddOrder("Tamar", "tamar@gmail", "nachal micha", DateTime.Now.AddDays(-(rnd.Next(9))), DateTime.Now, DateTime.MinValue);
        #endregion
        #region  AddOrderItem
        AddOrderItem(100000, 1, 55, 2);
        AddOrderItem(100001, 1, 20, 3);
        AddOrderItem(100006, 1, 10, 5);

        AddOrderItem(100004, 2, 9500, 2);

        AddOrderItem(100002, 3, 60, 1);

        AddOrderItem(100000, 4, 55, 1);
        AddOrderItem(100003, 4, 15, 3);
        AddOrderItem(100008, 4, 110, 1);
        AddOrderItem(100001, 4, 20, 3);

        AddOrderItem(100005, 5, 100005, 1);

        AddOrderItem(100004, 6, 9500, 1);
        AddOrderItem(100008, 6, 110, 1);

        AddOrderItem(100000, 7, 55, 2);
        AddOrderItem(100001, 7, 20, 3);
        AddOrderItem(100006, 7, 10, 5);

        AddOrderItem(100004, 8, 9500, 2);

        AddOrderItem(100002, 9, 60, 1);

        AddOrderItem(100000, 10, 55, 1);
        AddOrderItem(100003, 10, 15, 3);
        AddOrderItem(100008, 10, 110, 1);
        AddOrderItem(100001, 10, 20, 3);

        AddOrderItem(100005, 11, 100005, 1);

        AddOrderItem(100000, 12, 55, 1);
        AddOrderItem(100003, 12, 15, 3);
        AddOrderItem(100008, 12, 110, 1);
        AddOrderItem(100001, 12, 20, 3);

        AddOrderItem(100000, 13, 55, 2);
        AddOrderItem(100001, 13, 20, 3);
        AddOrderItem(100006, 13, 10, 5);

        AddOrderItem(100004, 14, 9500, 2);

        AddOrderItem(100002, 15, 60, 1);

        AddOrderItem(100000, 16, 55, 1);
        AddOrderItem(100003, 16, 15, 3);
        AddOrderItem(100008, 16, 110, 1);
        AddOrderItem(100001, 16, 20, 3);

        AddOrderItem(100005, 17, 100005, 1);

        AddOrderItem(100004, 18, 9500, 1);
        AddOrderItem(100008, 18, 110, 1);


        AddOrderItem(100004, 19, 9500, 1);
        AddOrderItem(100008, 19, 110, 1);

        AddOrderItem(100004, 20, 9500, 1);
        AddOrderItem(100008, 20, 110, 1);
        #endregion
    }
    #endregion
    #region config
    static internal class Config
    {
        static private int _numOfProduct = 100000;
        static private int _numOfOrder = 1;
        static private int _numOfOrderItem = 0;
        static public int NumOfProduct { get { return _numOfProduct++; } }
        static public int NumOfOrder { get { return _numOfOrder++; } }
        static public int NumOfOrderItem { get { return _numOfOrderItem++; } }

    }
    #endregion
}
