﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDal
    {
        IOrder Order { get; }
        IOrderItem OrderItem { get; }
        IProduct Product { get; }

    }
}
