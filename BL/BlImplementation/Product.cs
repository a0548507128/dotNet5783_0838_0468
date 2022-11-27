using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
using DalApi;
using static BO.Enums;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private IDal dal = new DalList();
    public IEnumerable<BO.ProductForList> GetProductsList()
    {
        IEnumerable<DO.Product> dalProductsList = Dal.DalProduct.getAllProduct();
        List<BO.ProductForList> blProductsList = new List<BO.ProductForList>();
        foreach (DO.Product productDal in dalProductsList)
        {
            BO.ProductForList product = new BO.ProductForList()
            {
                ID = productDal.ID,
                Name = productDal.Name,
                Price = productDal.Price,
                Category = (ECategory)productDal.Category
            };
            blProductsList.Add(product);
        }
        return blProductsList;
    }
    public BO.Product GetProductDetailsManager(int productID)
    {
        if (productID > 0)
        {
            DO.Product dalProduct = new DO.Product();
            try
            {
                dalProduct = Dal.DalProduct.Get(productID);
            }
            catch (DO.EntityNotFound e)
            {
                //throw new Exception(e);
            }
            BO.Product BLproduct = new BO.Product()
            {
                ID = dalProduct.ID,
                Name = dalProduct.Name,
                Price = dalProduct.Price,
                Category = (ECategory)dalProduct.Category,
                InStock = dalProduct.InStock
            };
            return BLproduct;
        }
        else
            throw new Exception();
    }
    public BO.ProductItem GetProductDetailsCustomer(int productID, BO.Cart c)
    {
        if (productID > 0)
        {
            DO.Product productDal = new DO.Product();
            try
            {
                productDal = Dal.DalProduct.Get(productID);
            }
            catch (DO.EntityNotFound e)
            {
                //throw new Exception(e);
            }

            int amountInCart = 0;
            foreach (BO.OrderItem oi in BO.Cart.ItemList)
            {
                if (oi.ID == productID)
                {
                    amountInCart = oi.Amount;
                    break;
                }
            }
            if (amountInCart == 0)
                throw new Exception();

            BO.ProductItem product = new BO.ProductItem()
            {
                ID = productDal.ID,
                Name = productDal.Name,
                Price = productDal.Price,
                Category = (ECategory)productDal.Category,
                AmoutInYourCart = amountInCart,
                InStock = (productDal.InStock)//לתקן!!!!
            };
            return product;
        }
        else
            throw new Exception();
    }
    public void AddProductManager(BO.Product product)
    {
        if (product.ID <= 0 || product.Name == "" || product.Price <= 0 || product.InStock < 0)
            throw new Exception();
        DO.Product productDal = new DO.Product()
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (DO.Enums.Category)product.Category,
            InStock = product.InStock,
        };
        try
        {
            Dal.DalProduct.Add(productDal);
        }
        catch (DO.EntityNotFound e)
        {
            //throw new Exception();
        }
    }
    public void DeleteProductManager(int productID)
    {
        IEnumerable<BO.Order> ordersList = BO.Order.GetOrders();//fix???
        foreach (BO.Order order in ordersList)
        {
            foreach (BO.OrderItem oi in BO.Order.ItemList)
                if (oi.ID == productID)
                    throw new Exception();
        }
        try
        {
            Dal.DalProduct.Delete(productID);
        }
        catch (DO.EntityNotFound e)
        {
            //throw new Exception();
        }
    }
    public void UpdateProductManager(BO.Product product)
    {
        if (product.ID <= 0 || product.Name == "" || product.Price <= 0 || product.InStock < 0)
            throw new Exception();

        DO.Product productDal = new DO.Product()
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (DO.Enums.Category)product.Category,
            InStock = product.InStock,
        };

        try
        {
            Dal.DalProduct.Update(productDal);
        }
        catch (DO.EntityNotFound e)
        {
            //throw new Exception();
        }
    }
}


