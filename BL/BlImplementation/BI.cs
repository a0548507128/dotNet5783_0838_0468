
using BlApi;

namespace BlImplementation;

sealed public class Bl : IBl
{
    public ICart Cart => new Cart();
    public BlApi.IOrder Order => new Order();
    public BlApi.IProduct Product => new Product();

}
