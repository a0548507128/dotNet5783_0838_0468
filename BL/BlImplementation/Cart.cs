
using BlApi;
using BO;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Cart:ICart
{
    private IDal dal = new DalList();

    public BO.Cart AddProduct(BO.Cart c, int productID)
    {
        bool exist = c.ItemList.Exists(e => e.ID == productID);
        if (exist)
        {
            BO.OrderItem BOI = c.ItemList.Find(e => e.ID == productID);
            DO.Product DP = dal.Product.Get(productID);
            if (BOI.Amount < DP.InStock)
            {
                BOI.Amount++;
                BOI.sumItem += BOI.Price;
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
                DO.Product DP = dal.Product.Get(productID);
                if (DP.InStock > 0)
                {
                   
                    c.ItemList.Add(new BO.OrderItem()
                    {
                        numInOrder = c.ItemList.Count + 1,
                        ID = DP.ID,
                        Name = DP.Name,
                        Price = DP.Price,
                        Amount = 1,
                        sumItem = DP.Price

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
    public BO.Cart UpdateAmountProduct(BO.Cart cart, int productID, int newAmount) 
    {
        bool exist = cart.ItemList.Exists(e => e.ID == productID);
        if (!exist)
        {
            throw new BO.ItemNotInCartException("item not in cart") { ItemNotInCart = productID.ToString() };
        }
        BO.OrderItem BOI = cart.ItemList.Find(e => e.ID == productID);
        if (newAmount < 0)
        {
            throw new BO.NegativeAmountException("negative amount") { NegativeAmount = productID.ToString() };
        }
        else if (newAmount == 0)
        {
            cart.TotalSum -= BOI.sumItem;
            cart.ItemList.RemoveAll(e => e.ID == productID);
        }
        else if (BOI.Amount < newAmount)
        {
            for (int i = 0; i < (newAmount - BOI.Amount); i++)
            {
                AddProduct(cart, productID);
            }
        }
        else if (BOI.Amount > newAmount)
        {
            int difference = BOI.Amount - newAmount;
            BOI.Amount = newAmount;
            BOI.sumItem -= (difference * BOI.Price);
            cart.TotalSum -= (difference * BOI.Price);
        }
        //BOI.Amount== amount
        //amount didnt change
        return cart;

    }
    public void OrderConfirmation(BO.Cart cart, string customerName, string customerEmail, string customerAdress) 
    {
        #region check correct data

        if (customerName is null)
        {
            throw new BO.NameIsNullException("name is null") { NameIsNull = customerName.ToString() };
        }
        if (customerAdress is null)
        {
            throw new BO.AdressIsNullException("adress is null") { AdressIsNull = customerAdress.ToString() };
        }
       // checkEmail(customerEmail);
        foreach (var item in cart.ItemList)
        {
            try
            {
                DO.Product DP = dal.Product.Get(item.ID);
                if (item.Amount < 0)
                {
                    throw new BO.NegativeAmountException("negative amount")
                    {
                        NegativeAmount = item.Amount.ToString()
                    };
                }
                if (item.Amount > DP.InStock)
                {
                    throw new BO.NotEnoughInStockException("Not enough in stock") { NotEnoughInStock = item.Amount.ToString() };
                }

            }
            catch (DO.EntityNotFound)
            {
                throw new BO.ItemInCartNotExistsAsProductException("item in cart not exists as product") { ItemInCartNotExistsAsProduct = item.ToString() };
            }
        }

        #endregion


        DO.Order o = new DO.Order()
        {
            ID = 0,
            CustomerName = customerName,
            CustomerAdress = customerAdress,
            CustomerEmail = customerEmail,
            OrderDate = DateTime.Now,
            ShipDate = DateTime.MinValue,
            DeliveryrDate = DateTime.MinValue,
        };
        try
        {

            int orderID = dal.Order.Add(o);
            try
            {
                foreach (var item in cart.ItemList)
                {
                    dal.OrderItem.Add(new DO.OrderItem()
                    {
                        ID = 0,
                        ProductID = item.ID,
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
                DO.Product p = dal.Product.Get(item.ID);
                p.InStock -= item.Amount;
                dal.Product.Update(p);
            }
        }
        catch (DO.EntityNotFound)
        {

            throw new BO.FieldToGetProductException("order not exists") { FieldToGetProduct = o.ToString() };
        }

    }
}
