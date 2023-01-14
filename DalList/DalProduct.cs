using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Dal;
using static DataSource;
public class DalProduct:IProduct
{
    public int Add(Product _p)
    {
        //_p.ID = Config.NumOfProduct;
        try 
        {
            Get(_p.ID);
            throw new DuplicateID("product already exist");
        }
        catch(DO.EntityNotFound) {
            Products.Add(_p);
        }
        return _p.ID;
    }
    public Product? Get(int IdNum)
    {
        //foreach (Product? p in Products)
        //{
        //    if (p?.ID == IdNum)
        //        return p;
        //}
        Product? p = Products.FirstOrDefault(Product => Product?.ID == IdNum);
        if (p == null)
            throw new EntityNotFound("this product does not exist");
        return p;
    }
    public IEnumerable<Product?> GetAll(Predicate<Product?>? predict = null)
    {
        List<Product?> _allProducts = new List<Product?>();

        if (predict == null)
        {
            _allProducts = Products;
        }
        else
        {
            _allProducts= Products.FindAll(x=>predict(x));
            
        }
        return _allProducts;

    }
    public void Delete(int IdNum)
    {
        Products.Remove((Products.FirstOrDefault(item => item?.ID == IdNum))
            ?? throw new Exception("this item doesn't exist"));

        //for (int i = 0; i < Products.Count; i++)
        //{
        //    if (Products[i]?.ID == IdNum)
        //    {
        //        Products.RemoveAt(IdNum);
        //        return;
        //    }
        //}
        //throw new Exception("this item doesn't exist");
    }
    public int Update(Product upProduct)
    {
        for (int i = 0; i < Products.Count; i++)
        {
            if (Products[i]?.ID == upProduct.ID)
            {
                Products[i] = upProduct;
                return upProduct.ID;
            }
        }
        throw new EntityNotFound("this product doesn't exist");
    }
}

