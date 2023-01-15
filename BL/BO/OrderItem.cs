

using System.ComponentModel;

namespace BO;

public class OrderItem: INotifyPropertyChanged
{
   // public int numInOrder { get; set; }//אתחול ב-0 ואז ליסט.קאונט
    private int numInOrder;
    public int NumInOrder
    {
        get { return numInOrder; }
        set
        {
            numInOrder = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(nameof(NumInOrder)));
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
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(nameof(ID)));
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
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(nameof(Name)));
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
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(nameof(Price)));
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
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(nameof(Amount)));
        }
    }

   // public double sumItem { get; set; }
    private double sumItem;
    public double SumItem
    {
        get { return sumItem; }
        set
        {
            sumItem = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(nameof(SumItem)));
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    public override string ToString() => $@"
    item number={NumInOrder}
    Order Item ID={ID}
    name={Name}:
    Price: {Price}
    Amount : {Amount}
    sum item={SumItem}
";
}
