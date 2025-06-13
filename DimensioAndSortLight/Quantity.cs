using System.Text.Json.Serialization;

namespace GreenOptimizer.DimensionAndSort
{
    public class Quantity : QuantityBase
    {
        public Quantity(double val, Unit unit, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity, string symbol = "") : base(val, unit, prefix, symbol)
        {

        }

        public Quantity(Quantity q) : base(q.Value, q.Unit, q.PrefixIndex)
        {

        }

        public Quantity()
        {
            
        }

    }

    public class QuantityBase : IComparable<QuantityBase>
    {
        #region members
        protected Unit _unit;
        protected double _valueInSIUnits;
        protected Unit.SI_PrefixEnum _prefixIndex = Unit.SI_PrefixEnum.unity;
        protected string _symbol;
        #endregion

        protected bool Equals(QuantityBase other)
        {
            bool ok1 = _unit.Equals(other._unit);
            bool ok2 = Math.Abs(ValueInSIUnits - other.ValueInSIUnits) < 1e-9;
            return ok1 && ok2;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((QuantityBase)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_unit != null ? _unit.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ _valueInSIUnits.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)_prefixIndex;
                hashCode = (hashCode * 397) ^ (_symbol != null ? _symbol.GetHashCode() : 0);
                return hashCode;
            }
        }

        public Unit Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        public Unit.SI_PrefixEnum PrefixIndex
        {
            get { return _prefixIndex; }
            set { _prefixIndex = value; }
        }

        [JsonIgnore]
        protected Unit.SIprefix prefix
        {
            get { return Unit.Prefixes[(int)_prefixIndex]; }
        }

        public double ValueInSIUnits
        {
            get { return _valueInSIUnits; }
            set { _valueInSIUnits = value; }
        }

        public double ToSIUnit(double value, Unit unit, Unit.SI_PrefixEnum prefixIndex = Unit.SI_PrefixEnum.unity)
        {
            return Unit.Prefixes[(int) prefixIndex].Factor * (_unit.Scale * value + _unit.Offset);
        }

        [JsonIgnore]
        public virtual double Value
        {
            get { return (_valueInSIUnits/prefix.Factor - _unit.Offset)/_unit.Scale; }
           
        }
       

        public QuantityBase(double value, Unit unit, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity, string symbol = "")
        {
            _unit = unit;
            _valueInSIUnits = ToSIUnit(value, unit, prefix);
            _prefixIndex = prefix;
            _symbol = symbol;
        }

        public QuantityBase()
        {
            _unit = new Dimensionless();
            _symbol = string.Empty;
        }

        public void SetUnit(Unit newUnit)
        {
            if (newUnit.SameDimension(_unit))
            {
                _unit = newUnit;
                _prefixIndex = Unit.SI_PrefixEnum.unity;
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }

        public Quantity CovertToUnit(Unit newUnit)
        {
            Quantity q = null;
            if (newUnit.SameDimension(_unit))
            {
                double value = newUnit.FromSIUnit(ValueInSIUnits);
                Unit unit = newUnit;
                Unit.SI_PrefixEnum prefixIndex = Unit.SI_PrefixEnum.unity;
                q = new Quantity(value, unit, prefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
            return q;
        }

        public static Quantity operator *(QuantityBase q1, double f)
        {
            return new Quantity(q1.Value * f, q1.Unit, q1.PrefixIndex);
        }

        public static Quantity operator *(double f, QuantityBase q1)
        {
            return new Quantity(q1.Value * f, q1.Unit, q1.PrefixIndex);
        }

        public static Quantity operator /(QuantityBase q1, double f)
        {
            return new Quantity(q1.Value / f, q1.Unit, q1.PrefixIndex);
        }

        public static Quantity operator /(double f, QuantityBase q1)
        {
            return new Quantity(f / q1.Value, q1.Unit, q1.PrefixIndex);
        }

        public static Quantity operator +(QuantityBase q1, QuantityBase q2)
        {
            if (q1._unit.SameDimension(q2._unit))
            {
                return new Quantity(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits), q1.Unit);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }

        public static Quantity operator -(QuantityBase q1, QuantityBase q2)
        {
            if (q1._unit.SameDimension(q2._unit))
            {
                return new Quantity(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits), q1.Unit);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }

        public static Quantity operator *(QuantityBase q1, QuantityBase q2)
        {
            Unit prodUnit = q1.Unit * q2.Unit;

            double val = (q1.ValueInSIUnits * q2.ValueInSIUnits) / prodUnit.Scale;

            return new Quantity(val, prodUnit);
        }

        public static Quantity operator /(QuantityBase q1, QuantityBase q2)
        {
            Unit divUnit = q1.Unit / q2.Unit;

            double divVal = (q1.ValueInSIUnits / q2.ValueInSIUnits) / divUnit.Scale;

            return new Quantity(divVal, divUnit);
        }

        public static bool operator <=(QuantityBase q1, QuantityBase q2)
        {
            return q1.ValueInSIUnits <= q2.ValueInSIUnits;
        }

        public static bool operator >=(QuantityBase q1, QuantityBase q2)
        {
            return q1.ValueInSIUnits >= q2.ValueInSIUnits;
        }

        public static bool operator <(QuantityBase q1, QuantityBase q2)
        {
            return q1.ValueInSIUnits < q2.ValueInSIUnits;
        }

        public static bool operator >(QuantityBase q1, QuantityBase q2)
        {
            return q1.ValueInSIUnits > q2.ValueInSIUnits;
        }



        public static Quantity Pow(QuantityBase q1, int n)
        {
            return new Quantity(Math.Pow(q1.ValueInSIUnits, (double)n), q1.Unit + n);
        }

        /// <summary>
        /// Find prefix that gives a value as clode to 1 a possible
        /// </summary>
        public QuantityBase AdjustPrefix()
        {
            
            double val = Value;
            double minval = Math.Abs(val - 1); 
            Unit.SI_PrefixEnum minind = _prefixIndex;
            foreach (Unit.SI_PrefixEnum sIprefix in Enum.GetValues(typeof(Unit.SI_PrefixEnum)))
            {
                Unit.SIprefix  pref = Unit.Prefixes[(int) sIprefix];
                double newValue =  Math.Abs(val*prefix.Factor / pref.Factor - 1);
                if (newValue < minval)
                {
                    minval = newValue;
                    minind = sIprefix;
                }
            }

            if (minind != _prefixIndex)
            {
                SetPrefix(minind);
            }

            return this;
        }

        public void SetPrefix(Unit.SI_PrefixEnum newprefix)
        {
            _prefixIndex = newprefix;
        }

        public override string ToString()
        {
            string str = "";

            if (Value >= 0.01)
            {
                str = Value.ToString("0.00") + " " + prefix.Symbol + _unit;
            }
            else
            {
                str = Value.ToString("e2") + " " + prefix.Symbol + _unit;
            }

            return str;
        }


        public virtual QuantityBase Clone()
        {
            return new QuantityBase(Value, _unit, _prefixIndex);
        }



        #region IComparable<QuantityBase> Members

        public int CompareTo(QuantityBase other)
        {
            return ValueInSIUnits.CompareTo(other.ValueInSIUnits);
        }

        #endregion
    }
}
