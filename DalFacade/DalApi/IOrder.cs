﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public interface IOrder : ICrud<Order>
    {
        
        public IEnumerable<Order>? GetAll(Func<Order?, bool>? func = null);
        public int Add(Order _o);
        public Order Get(int IdNum);
        public void Delete(int IdNum);
        public int Update(Order upOrder);
    }
}
