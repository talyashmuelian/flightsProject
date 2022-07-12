using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class DLImp:IDL
    {
        static DLImp instance;//=new DLImp();
        private DLImp()
        {
               
        }
        static public DLImp theInstance()
        {
            if (instance == null)
                instance = new DLImp();
            return instance;
        }
    }
}
