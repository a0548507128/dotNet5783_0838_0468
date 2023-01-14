using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BO;
namespace BlApi;

public interface IProduct
{
    public IEnumerable<BO.ProductForList?> GetProductsList(Func<DO.Product?, bool>? predict = null);
    public BO.Product GetProductDetailsManager(int productID);
    public BO.ProductItem GetProductDetailsCustomer(int productID, BO.Cart c);
    public void AddProductManager(BO.Product product);
    public void DeleteProductManager(int productID);
    public void UpdateProductManager(BO.Product product);
    public IEnumerable<BO.ProductItem?> GetProductsItem(Func<DO.Product?, bool>? predict = null);
    public BO.ProductItem GetProductItemDetails(int productID, BO.Cart c);
}
