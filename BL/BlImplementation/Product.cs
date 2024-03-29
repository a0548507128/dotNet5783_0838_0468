﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using DO;
using static BO.Enums;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    IDal? dal = DalApi.Factory.Get();

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList?> GetProductsList(Func<DO.Product?, bool>? predict = null)
    {
        IEnumerable<DO.Product?> dalProductsList;
        IEnumerable<BO.ProductForList?> blProductsList= new List<BO.ProductForList?>();
        if(predict== null)
        {
            dalProductsList = dal!.Product.GetAll();
        }
        else
        {
            dalProductsList = dal!.Product.GetAll(x=>predict(x));
        }
        //foreach (var productDal in dalProductsList)
        //{
        //    if (productDal != null)
        //    {
        //        blProductsList.Add(new BO.ProductForList()
        //        {
        //            ID = productDal.Value.ID,
        //            Name = productDal.Value.Name,
        //            Price = productDal.Value.Price,
        //            Category = (ECategory?)productDal.Value.Category
        //        });
        //    }
        //}
        blProductsList = from productDal in dalProductsList
                        where (productDal != null|| predict!(productDal))
                        select new BO.ProductForList()
                        {
                            ID = productDal.Value.ID,
                            Name = productDal.Value.Name,
                            Price = productDal.Value.Price,
                            Category = (ECategory?)productDal.Value.Category
                        };
        return blProductsList;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Product GetProductDetailsManager(int productID)
    {
        if (productID > 0)
        {
            DO.Product dalProduct = new DO.Product();
            try
            {
                dalProduct = (DO.Product)dal!.Product.Get(productID)!;
            }
            catch (DO.EntityNotFound )
            {
                throw new Exception();
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

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.ProductItem GetProductDetailsCustomer(int productID, BO.Cart c)
    {
        if (productID > 0)
        {
            DO.Product productDal = new DO.Product();
            try
            {
                productDal = (DO.Product)dal!.Product.Get(productID)!;
            }
            catch (DO.EntityNotFound )
            {
                throw new Exception();
            }
           
            //int amountInCart = 0;
            //foreach (var oi in c.ItemList)
            //{
            //    if (oi?.ID == productID)
            //    {
            //        amountInCart = oi.Amount;
            //        break;
            //    }
            //}
            int amountInCart = c.ItemList?.FirstOrDefault(oi => oi?.ID == productID)?.Amount ?? 0;
            if (amountInCart == 0)
                throw new Exception();

            BO.ProductItem product = new BO.ProductItem()
            {
                ID = productDal.ID,
                Name = productDal.Name,
                Price = productDal.Price,
                Category = (ECategory?)productDal.Category,
                AmoutInYourCart = amountInCart,
                InStock = (productDal.InStock)
            };
            return product;
        }
        else
            throw new Exception();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AddProductManager(BO.Product product)
    {
        if (product?.ID == null || product?.Price == null || product?.InStock == null)
            throw new ProductInUseException("ERROR: empty field");
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
            dal!.Product.Add(dalProduct);
        }
        catch (DO.DuplicateID e)
        {
            throw new ItemAlreadyExistsException(e.Message);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeleteProductManager(int productID)
    {
        IEnumerable<DO.OrderItem?> ordersItemList = dal!.OrderItem.GetAll();
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
        catch (DO.EntityNotFound )
        {
            throw new Exception();
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void UpdateProductManager(BO.Product product)
    {
        if (product?.ID == null || product?.Price == null || product?.InStock == null)
            throw new ProductInUseException("ERROR: empty field");
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
            dal!.Product.Update(productDal);
        }
        catch (DO.EntityNotFound e)
        {
            throw new BO.ProductNotExistsException(e.Message);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductItem?> GetProductsItem(Func<DO.Product?, bool>? predict = null) {
        IEnumerable<DO.Product?> dalProductsList;
        IEnumerable<BO.ProductItem?> blProductsItem = new List<BO.ProductItem?>();
        if (predict == null)
        {
            dalProductsList = dal!.Product.GetAll();
        }
        else
        {
            dalProductsList = dal!.Product.GetAll(x => predict(x));
        }
        blProductsItem = from productDal in dalProductsList
                         where (productDal != null || predict!(productDal))
                         select new BO.ProductItem()
                         {
                             ID = productDal.Value.ID,
                             Name = productDal.Value.Name,
                             Price = productDal.Value.Price,
                             Category = (ECategory?)productDal.Value.Category,
                             InStock=productDal.Value.InStock,
                            // AmoutInYourCart=
                         };
        return blProductsItem;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.ProductItem GetProductItemDetails(int productItemID, BO.Cart c)
    {
        if (productItemID > 0)
        {
            DO.Product dalProduct = new DO.Product();
            try
            {
                dalProduct = (DO.Product)dal!.Product.Get(productItemID)!;
            }
            catch (DO.EntityNotFound )
            {
                throw new Exception();
            }
            var OrderItem = c.ItemList!.Where(x => x!.ID == dalProduct.ID).FirstOrDefault();
            int amount=0;
            if (OrderItem != null)
            {
                amount = OrderItem.Amount;
            }
                BO.ProductItem BLproduct = new BO.ProductItem()
                {
                    ID = dalProduct.ID,
                    Name = dalProduct.Name,
                    Price = dalProduct.Price,
                    Category = (ECategory?)dalProduct.Category,
                    InStock = dalProduct.InStock,
                    AmoutInYourCart = amount
                };
            
            return BLproduct;
        }
        else
            throw new Exception();
    }
}


