using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public List<OrderItem?>? ItemList { get; set; }=new List<OrderItem?>();
    
    public double TotalSum { get; set; }

    public override string ToString() => $@"
    Name: {CustomerName}, 
    Email - {CustomerEmail}
    Adress: {CustomerAdress}
    List of Item:{ItemList}
    Total sum:{TotalSum}
";
}
