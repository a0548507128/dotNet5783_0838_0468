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
        _p.ID = Config.NumOfProduct;
        Products.Add(_p);
        return _p.ID;
    }
    public Product Get(int IdNum)
    {
        foreach (Product p in Products)
        {
            if (p.ID == IdNum)
                return p;
        }
        throw new Exception("this product does not exist");
    }
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? predict =null)
    {
        List<Product?> _allProducts = new List<Product?>();

        if (predict == null)
        {
            _allProducts = Products;
            //    foreach (Product p in Products)
            //    {
            //        _allProducts.Add(p);
            //    }
        }
        else
        {
            foreach (Product p in Products)
            {
                if (predict(p))
                    _allProducts.Add(p);
            }
        }
        return _allProducts;

    }
    public void Delete(int IdNum)
    {
        for (int i = 0; i < Products.Count; i++)
        {
            if (Products[i]?.ID == IdNum)
            {
                Products.RemoveAt(IdNum);
                return;
            }
        }
        throw new Exception("this item doesn't exist");
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
        throw new Exception("this item doesn't exist");
    }
}

