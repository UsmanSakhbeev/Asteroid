using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid
{
    class BaseObjectException:ArgumentOutOfRangeException
    {
        public BaseObjectException(string paramName, string message):base(paramName,message)
        { }
    }
}
