
using BlApi;
using BO;
using DalApi;
using DO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using static BO.Enums;

namespace BlImplementation;

internal class Cart:ICart
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart AddProduct(BO.Cart c, int productID)
    {
        bool exist = c.ItemList!.Exists(e => e!.ID == productID);
        if (exist)
        {
            BO.OrderItem? BOI = c.ItemList.Find(e => e?.ID == productID);
            DO.Product? DP = dal?.Product.Get(productID);
            if (BOI?.Amount < DP?.InStock)
            {
                BOI.Amount++;
                BOI.SumItem += BOI.Price;
                c.TotalSum += BOI.Price;
                return c;
            }
            else
            {
                throw new BO.NotEnoughInStockException("Not enough in stock") { NotEnoughInStock = productID.ToString() };
            }
        }
        else
        {
            try
            {
                DO.Product DP = (DO.Product)dal!.Product.Get(productID)!;
                if (DP.InStock > 0)
                {
                   
                    c.ItemList.Add(new BO.OrderItem()
                    {
                        NumInOrder = c.ItemList.Count + 1,
                        ID = DP.ID,
                        Name = DP.Name,
                        Price = DP.Price,
                        Amount = 1,
                        SumItem = DP.Price

                    });
                    c.TotalSum += DP.Price;
                    
                    return c;
                }
                else
                {
                    throw new BO.ProductNotInStockException("product not in stock") { ProductNotInStock = productID.ToString() };
                }

            }
            catch (DO.EntityNotFound)
            {
                throw new BO.ProductNotExistsException("product not exists") { ProductNotExists = productID.ToString() };
            }
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart UpdateAmountProduct(BO.Cart cart, int productID, int newAmount) 
    {
        bool exist = cart.ItemList!.Exists(e => e?.ID == productID);
        if (!exist)
        {
            throw new BO.ItemNotInCartException("item not in cart") { ItemNotInCart = productID.ToString() };
        }
        BO.OrderItem BOI = cart.ItemList.Find(e => e!.ID == productID)!;
        if (newAmount < 0)
        {
            throw new BO.NegativeAmountException("negative amount") { NegativeAmount = productID.ToString() };
        }
        else if (newAmount == 0)
        {
            cart.TotalSum -= BOI.SumItem;
            cart.ItemList.RemoveAll(e => e?.ID == productID);
        }
        else if (BOI?.Amount < newAmount)
        {
            for (int i = 0; i < (newAmount - BOI.Amount); i++)
            {
                AddProduct(cart, productID);
            }
        }
        else if (BOI!.Amount > newAmount)
        {
            int difference = BOI.Amount - newAmount;
            BOI.Amount = newAmount;
            BOI.SumItem -= (difference * BOI.Price);
            cart.TotalSum -= (difference * BOI.Price);
        }
        return cart;

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int OrderConfirmation(BO.Cart cart, string customerName, string customerEmail, string customerAdress) 
    {
        #region check correct data

        if (customerName is null)
        {
            throw new NameIsNullException("name is null");
        }
        if (customerAdress is null)
        {
            throw new AdressIsNullException("adress is null");
        }
       var ii =(from i in cart.ItemList
               where i is not null && i.Amount>0
               select i).FirstOrDefault();
        try
        {
            DO.Product DP = (DO.Product)dal!.Product.Get(ii!.ID)!;
            if ((from i in cart.ItemList
                 where i is not null && i.Amount < 0
                 select i).FirstOrDefault() is not null)
            {
                throw new BO.NegativeAmountException("negative amount")
                {
                    NegativeAmount = 0.ToString()
                };
                if ((from i in cart.ItemList
                     where i is not null && i.Amount < 0
                     select i).FirstOrDefault() is not null)
                {
                    throw new BO.NotEnoughInStockException("Not enough in stock") { NotEnoughInStock = ii.Amount.ToString() };
                }
            }
        }
        catch (Exception)
        {
            throw new BO.ItemInCartNotExistsAsProductException("item in cart not exists as product") { ItemInCartNotExistsAsProduct = ii!.ToString() };
        }


        #endregion

        
        DO.Order o = new DO.Order()
        {
            ID = 0,
            CustomerName = customerName,
            CustomerAdress = customerAdress,
            CustomerEmail = customerEmail,
            OrderDate = DateTime.Now,
            ShipDate = null,
            DeliveryDate = null,
        };
        int orderID = dal.Order.Add(o);
        try
        {  
            try
            {
                foreach (var item in cart.ItemList!)
                {
                    dal.OrderItem.Add(new DO.OrderItem()
                    {
                        ID = 0,
                        ProductID = item!.ID,
                        OrderID = orderID,
                        Price = item.Price,
                        Amount = item.Amount
                    });
                }
            }
            catch (DO.DuplicateID)
            {
                throw new BO.ItemAlreadyExistsException("item aleardy exists") { ItemAlreadyExists = o.ToString() };
            }
        }
        catch (DO.EntityNotFound)
        {
            throw new BO.OrderNotExistsException("order not exists") { OrderNotExists = o.ToString() };
        }
        try
        {
            foreach (var item in cart.ItemList)
            {
                DO.Product p = (DO.Product)dal.Product.Get(item!.ID)!;
                p.InStock -= item.Amount;
                dal.Product.Update(p);
            }
        }
        catch (DO.EntityNotFound)
        {

            throw new BO.FieldToGetProductException("order not exists") { FieldToGetProduct = o.ToString() };
        }
        return orderID;
    }
}
