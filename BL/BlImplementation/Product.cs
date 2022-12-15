﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DalApi;
using DO;
using static BO.Enums;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private IDal dal = new DalList();
    public IEnumerable<BO.ProductForList?> GetProductsList(Func<DO.Product?, bool>? predict = null)
    {
        IEnumerable<DO.Product?> dalProductsList; 
        List<BO.ProductForList?> blProductsList = new ();
        if(predict== null)
        {
            dalProductsList = dal.Product.GetAll();
        }
        else
        {
            dalProductsList = dal.Product.GetAll(x=>predict(x));
        }
        foreach (var productDal in dalProductsList)
        {
            if (productDal != null)
            {
                BO.ProductForList product = new BO.ProductForList()
                {
                    ID = productDal.Value.ID,
                    Name = productDal.Value.Name,
                    Price = productDal.Value.Price,
                    Category = (ECategory?)productDal.Value.Category
                };
                blProductsList.Add(product);
            }
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
                Category = (ECategory?)dalProduct.Category,
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
            foreach (var oi in c.ItemList)
            {
                if (oi?.ID == productID)
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
                Category = (ECategory?)productDal.Category,
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
        if (product.ID <= 0)
            throw new NegativeIdException("ERROR: Negative Id");
        if (product.Name == "")
            throw new EmptyNameException("ERROR: Empty Name");
        if ( product.Price <= 0)
            throw new NegativePriceException("ERROR: Negative Price");
        if (product.InStock < 0)
            throw new NegativeStockException("ERROR: Negative InStock");

        DO.Product dalProduct = new DO.Product()
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (DO.Enums.Category?)product.Category,
            InStock = product.InStock,
        };
        try
        {
            dal.Product.Add(dalProduct);
        }
        catch (DO.DuplicateID e)
        {
            throw new ItemAlreadyExistsException(e.Message);
        }
    }
    public void DeleteProductManager(int productID)
    {
        IEnumerable<DO.OrderItem?> ordersItemList = dal.OrderItem.GetAll();
        foreach (var orderItem in ordersItemList)
        {
            if(orderItem!=null)
                if (orderItem.Value.ProductID == productID)
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
        if (product.ID <= 0)
            throw new NegativeIdException("ERROR: Negative Id");
        if (product.Name == "")
            throw new EmptyNameException("ERROR: Empty Name");
        if (product.Price <= 0)
            throw new NegativePriceException("ERROR: Negative Price");
        if (product.InStock < 0)
            throw new NegativeStockException("ERROR: Negative InStock");

        DO.Product productDal = new DO.Product()
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (DO.Enums.Category?)product.Category,
            InStock = product.InStock,
        };

        try
        {
            dal.Product.Update(productDal);
        }
        catch (DO.EntityNotFound e)
        {
            throw new BO.ProductNotExistsException(e.Message);
        }
    }
}


