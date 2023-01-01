using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T : struct 
    {
        public int Add (T IdAdd);
        public int Update (T IdUpdate);
        public void Delete (int IdDelete);
        public T? Get (int IdGet);
        public IEnumerable<T?> GetAll (Predicate<T?>? predict = null);
    }
}
