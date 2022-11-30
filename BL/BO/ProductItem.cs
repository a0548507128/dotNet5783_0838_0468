using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;


namespace BO;

public class ProductItem
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public ECategory? Category { get; set; }
    public double Price { get; set; }
    public int InStock { get; set; }
    public int AmoutInYourCart { get; set; }

    public override string ToString() => $@"
    Product item ID={ID}: {Name}, 
    category - {Category}
    Price: {Price}
    Amount in stock: {InStock}
    Amout in your cart:{AmoutInYourCart}
";
}
