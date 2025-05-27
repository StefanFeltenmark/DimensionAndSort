using System;

namespace GreenOptimizer.DimensionAndSort
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
