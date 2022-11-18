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
        public T Add (T IdAdd);
        public T Update (T IdUpdate);
        public T Delete (T IdDelete);
        public T Get (int IdGet);

    }
}
