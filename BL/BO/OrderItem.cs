

using System.ComponentModel;

namespace BO;

public class OrderItem: INotifyPropertyChanged
{
   // public int numInOrder { get; set; }//אתחול ב-0 ואז ליסט.קאונט
    private int productId;
    public int numInOrder
    {
        get { return productId; }
        set
        {
            productId = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("numInOrder"));
        }
    }
    //public int ID { get; set; }
    private int id;
    public int ID
    {
        get { return id; }
        set
        {
            id = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ID"));
        }
    }
    //public string? Name { get; set; }
    private string? name;
    public string? Name
    {
        get { return name; }
        set
        {
            name = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Name"));
        }
    }
    //public double Price { get; set; }
    private double price;
    public double Price
    {
        get { return price; }
        set
        {
            price = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Price"));
        }
    }

    //public int Amount { get; set; }
    private int amount;
    public int Amount
    {
        get { return amount; }
        set
        {
            amount = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
        }
    }

   // public double sumItem { get; set; }
    private double totalPrice;
    public double sumItem
    {
        get { return totalPrice; }
        set
        {
            totalPrice = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("sumItem"));
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    public override string ToString() => $@"
    item number={numInOrder}
    Order Item ID={ID}
    name={Name}:
    Price: {Price}
    Amount : {Amount}
    sum item={sumItem}
";
}
