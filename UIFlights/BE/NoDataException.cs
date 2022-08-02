using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class NoDataException : Exception
    {
        public NoDataException() : base() { }
        public NoDataException(string message) :
            base(message){ }
    }
}
