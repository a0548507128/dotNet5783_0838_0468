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
        IEnumerable<DO.Product> dalProductsList = dal.Product.getAllProduct();
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
                dalProduct = dal.Product.Get(productID);
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
                productDal = dal.Product.Get(productID);
            }
            catch (DO.EntityNotFound e)
            {
                //throw new Exception(e);
            }

            int amountInCart = 0;
            foreach (BO.OrderItem oi in c.ItemList)
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
        DO.Product dalProduct = new DO.Product()
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (DO.Enums.Category)product.Category,
            InStock = product.InStock,
        };
        try
        {
            dal.Product.Add(dalProduct);
        }
        catch (DO.EntityNotFound e)
        {
            //throw new Exception();
        }
    }
    public void DeleteProductManager(int productID)
    {
        IEnumerable<DO.OrderItem> ordersItemList = dal.OrderItem.getAllOrderItems();
        foreach (DO.OrderItem orderItem in ordersItemList)
        {
                if (orderItem.ProductID == productID)
                    throw new Exception();
        }
        try
        {
            dal.Product.Delete(productID);
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
            dal.Product.Update(productDal);
        }
        catch (DO.EntityNotFound e)
        {
            //throw new Exception();
        }
    }
}


