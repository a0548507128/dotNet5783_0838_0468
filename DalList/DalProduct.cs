using DO;
using System;

namespace Dal;
using static DataSource;
public class DalProduct
{
    public int addProduct(Product _p)
    {
        _p.ID = Config.NumOfProduct;
        Products[Config._productIndex] = _p;
        return _p.ID;
    }
    public Product getProduct(int IdNum)
    {

        foreach (Product p in Products)
        {
            if (p.ID == IdNum)
                return p;
        }
        throw new Exception("this product does not exist");
    }
    public Product[] getAllProduct()
    {
        Product[] _allProducts = new Product[DataSource.Config._productIndex];
        for (int i = 0; i < Config._productIndex; i++)
        {
            _allProducts[i] = Products[i];
        }
        return _allProducts;
    }
    public void deleteProduct(int IdNum)
    {
        for (int i = 0; i < Config._productIndex; i++)
        {
            if (Products[i].ID == IdNum)
            {
                Products[i] = Products[Config._productIndex - 1];
                return;
            }
        }
        throw new Exception("this item doesn't exist");
    }

    public void updateProduct(Product upProduct)
    {
        for (int i = 0; i < Config._productIndex; i++)
        {
            if (Products[i].ID == upProduct.ID)
            {
                Products[i] = upProduct;
                return;
            }
        }
        throw new Exception("this item doesn't exist");
    }
}
