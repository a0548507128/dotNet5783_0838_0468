using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T>
    {
        public int Add (T IdAdd);
        public int Update (T IdUpdate);
        public void Delete (int IdDelete);
        public T Get (int IdGet);

    }
}
