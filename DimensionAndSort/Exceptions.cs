using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimensionAndSort
{
    public class Exceptions
    {

    }

    public class IncompatibleUnits : Exception
    {
        public IncompatibleUnits(string message) : base(message)
        {
        }

        public IncompatibleUnits() : base()
        {
        }
    }

}
