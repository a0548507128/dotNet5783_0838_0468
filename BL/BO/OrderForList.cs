using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;


namespace BO;

public class OrderForList
{
    public int OrderID { get; set; }
    public string CustomerName { get; set; }
    public EStatus Status { get; set; }
    public int AmountOfItem { get; set; }
    public double TotalSum { get; set; }

    public override string ToString() => $@"
    Order ID={OrderID}: 
    name: {CustomerName}, 
   Status of order: {Status}
    amount of item: {AmountOfItem}
    Total sum:{TotalSum}
";
}
