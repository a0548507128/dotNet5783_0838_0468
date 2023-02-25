using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Dal;
using static DataSource;
public class DalProduct:IProduct
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Product _p)
    {
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

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product? Get(int IdNum)
    {
        Product? p = Products.FirstOrDefault(Product => Product?.ID == IdNum);
        if (p == null)
            throw new EntityNotFound("this product does not exist");
        return p;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
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

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int IdNum)
    {
        Products.Remove((Products.FirstOrDefault(item => item?.ID == IdNum))
            ?? throw new Exception("this item doesn't exist"));
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
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

