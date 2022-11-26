using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Cart:ICart
{
    private IDal dal = new DalList();

    public BO.Cart AddProduct(BO.Cart c, int productID)
    {
        return c;
    }
    public BO.Cart UpdateAmountProduct(BO.Cart c, int productID, int newAmount) 
    {
        return c;
    }
    public void OrderConfirmation(BO.Cart c, string customerName, string customerEmail, string customerAdress) 
    { 

    }
}
