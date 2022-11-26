using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

public interface IProduct
{
    public IEnumerable<ProductForList> GetProductsList();
    public Product GetProductDetailsManager(int productID);

    public Product GetProductDetailsCustomer(int productID, Cart c);
    public void AddProductManager(Product product);
    public void DeleteProductManager(int productID);
    public void UpdateProductManager(Product product);

}
