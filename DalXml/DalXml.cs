using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed internal class DalXml : IDal
    {
        #region singelton
        static readonly DalXml instance = new DalXml();
        static DalXml() { }// static ctor to ensure instance init is done just before first usage
        DalXml() { } // default => private
        public static DalXml Instance { get => instance; }
        #endregion


        public IOrder Order { get; } = new Dal.DalOrder();

        public IOrderItem OrderItem { get; } = new Dal.DalOrderItem();

        public IProduct Product { get; } = new Dal.DalProduct();

    }
}
