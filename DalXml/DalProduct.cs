using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalProduct : IProduct
{
    readonly string ProductPath = @"Product.xml";
    public int Add(DO.Product IdAdd)
    {
        List<Product?> ListProduct = XMLTools.LoadSerializer<Product>(ProductPath);
        //if (ListProduct.FirstOrDefault(e => e?.Name == IdAdd.Name && e?.Price == IdAdd.Price && e?.Category == _p.Category && e?.InStock == _p.InStock) is not null)
        //{
        //    throw new ItemAlreadyExistsException("product exists, can not add") { ItemAlreadyExists = _p.ToString() };
        //}
        IdAdd.ID = XMLConfig.getProductId();
        ListProduct.Add(IdAdd);
        XMLTools.SaveSerializer(ListProduct, ProductPath);
        return IdAdd.ID;
    }

    public void Delete(int IdDelete)
    {
        List<DO.Product?> ListProduct = XMLTools.LoadSerializer<Product>(ProductPath);
        //if (ListProduct == null)
        //{
        //    throw new RequestedItemNotFoundException("product not exists,can not get") { RequestedItemNotFound = _num.ToString() };
        //}
        DO.Product? pre = ListProduct.Find(p => p?.ID == IdDelete);
        try
        {
            if (pre != null)
            {
                ListProduct.Remove(pre);
                XMLTools.SaveSerializer(ListProduct, ProductPath);

            }
        }
        catch
        {
            throw new Exception("product not exists,can not do delete");
          //  throw new RequestedItemNotFoundException("product not exists,can not do delete") { RequestedItemNotFound = _num.ToString() };
        }
    }
    public DO.Product? Get(int IdGet)
    {
        List<Product?> ListProduct = XMLTools.LoadSerializer<Product>(ProductPath);
        //if (ListProduct == null)
        //{
        //    throw new RequestedItemNotFoundException("order not exists,can not get") { RequestedItemNotFound = null };
        //}
        //if (predict == null)
        //{
        //    throw new GetPredictNullException("the predict is empty") { GetPredictNull = null };
        //}
        try
        {
            Product? product = ListProduct.Find(p => p!.Value.ID== IdGet);
            return product;
        }
        catch
        {
            throw new Exception("product not exists,can not do get");
          //  throw new RequestedItemNotFoundException("product not exists,can not do get") { RequestedItemNotFound = predict.ToString() };
        }
    }

    public IEnumerable<DO.Product?> GetAll(Predicate<DO.Product?>? predict = null)
    {

        List<Product?> ListProduct = XMLTools.LoadSerializer<Product>(ProductPath);


        //if (ListProduct == null)
        //{
        //    throw new RequestedItemNotFoundException("product not exists,can not get") { RequestedItemNotFound = "jjj".ToString() };
        //}
        if (predict == null)
        {
            return ListProduct;

        }
        else
        {
            //List<Product?> _products = new List<Product?>();
            //_products=DataSource._Products.FindAll(e=> predict(e)); 
            //return _products;
            try
            {
                IEnumerable<Product?> product = ListProduct.FindAll(p => predict(p));
                return product;
            }
            catch
            {
                throw new Exception("products not exists,can not get");
              //  throw new RequestedItemNotFoundException("products not exists,can not get") { RequestedItemNotFound = "jjj".ToString() };
            }
        }
    }

    public int Update(DO.Product IdUpdate)
    {
        if (IdUpdate.Name == null || IdUpdate.Category == null)
        {
            return IdUpdate.ID;
        }

        List<DO.Product?> ListProduct = XMLTools.LoadSerializer<DO.Product>(ProductPath);
        //if (ListProduct is null)
        //    throw new RequestedItemNotFoundException("product not exists,can not get") { RequestedItemNotFound = _p.ToString() };
        DO.Product? pro = ListProduct.Find(p => p?.ID == IdUpdate.ID);
        try
        {
            if (pro != null)
            {
                ListProduct.Remove(pro);
                ListProduct.Add(IdUpdate); 
                XMLTools.SaveSerializer(ListProduct, ProductPath);

            }
        }
        catch
        {
            throw new Exception("products not exists,can not do update");
            //throw new RequestedItemNotFoundException("product not exists,can not do update") { RequestedItemNotFound = _p.ToString() };
        }
        return IdUpdate.ID;

    }
}
