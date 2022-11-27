using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation
{
    sealed public class Bl : IBl
    {
        public IProduct BoEntity => new Product();

    }
}
