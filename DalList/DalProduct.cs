using DO;   
namespace Dal;

public class DalProduct
{ 
   public int addProduct(Product _p)
   {


        return _p.ID;
   }
   public Product getProduct(int IdNum)
   {
        Product _p = new Product();


        return _p;
   }
    public Product[] getAllProduct()
    {
        Product[] _allProducts = new Product[DataSource.Config._productIndex];

        return _allProducts;
    }
    public void deleteProduct(int IdNum)
    {

    }

  public void updateProduct()
    {

    }
}
