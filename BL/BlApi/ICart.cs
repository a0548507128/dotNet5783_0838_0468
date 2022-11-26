using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

public interface ICart
{
    public BO.Cart AddProduct(BO.Cart c, int productID);
    public BO.Cart UpdateAmountProduct(BO.Cart c, int productID, int newAmount);
    public void OrderConfirmation(BO.Cart c, string customerName, string customerEmail, string customerAdress);
}