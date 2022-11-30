

namespace BO;

public class OrderItem
{
    public int numInOrder { get; set; }//אתחול ב-0 ואז ליסט.קאונט
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public double sumItem { get; set; }

    public override string ToString() => $@"
    item number={numInOrder}
    Order Item ID={ID}
    name={Name}:
    Price: {Price}
    Amount : {Amount}
    sum item={sumItem}
";
}
