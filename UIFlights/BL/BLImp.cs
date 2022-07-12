using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
namespace BL
{
    public class BLImp:IBL
    {
        private IDL dL=DLImp.theInstance();
        static BLImp instance;//=new DLImp();
        private BLImp()
        {

        }
        static public BLImp theInstance()
        {
            if (instance == null)
                instance = new BLImp();
            return instance;
        }
    }
}
