using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace GreenOptimizer.DimensionAndSort
{

    public class Scalings
    {
        // length
        public static Unit.Scaling metre = new("m");
        
        // weight
        public static Unit.Scaling kilogram = new("kg");
        public static Unit.Scaling ton = new("ton", 1000);

        // time
        public static Unit.Scaling second = new("s");
        public static Unit.Scaling hour = new("h", 3600);
        public static Unit.Scaling minute = new("min", 60);
        public static Unit.Scaling dayAndNight = new("d", 86400);
        public static Unit.Scaling week = new("w", 604800);
    }


    public class Unit : IEquatable<Unit>
    {
        private static SIprefix[] _prefixes = new SIprefix[12] {
                new SIprefix(-9,"n","nano"),
                new SIprefix(-6,"mu","mikro"),
                new SIprefix(-3,"m","milli"),
                new SIprefix(-2,"c","centi"),
                new SIprefix(-1,"d","deci"),
                new SIprefix(0,"",""),
                new SIprefix(1,"da","deka"),
                new SIprefix(2,"h","hekto"),
                new SIprefix(3,"k","kilo"),
                new SIprefix(6,"M","mega"),
                new SIprefix(9,"G","giga"),
                new SIprefix(12,"T","tera"),
                };

        private static Unit[] _baseUnits = new Unit[3] { new Metre(), new Kilogram(), new Second()}; 
        private static Unit[] _derivedUnits = new Unit[3] { new Newton(), new Joule(), new Watt()  };


        private Dictionary<int, Scaling> _scalings = new Dictionary<int, Scaling>();
        private Dictionary<int, SIprefix> _prefix = new Dictionary<int, SIprefix>();
        private static string[] _symbols = new string[3] { "m", "kg", "s" };
        

        public class SIprefix : IEquatable<SIprefix>
        {
            #region memberfields
            private string _symbol;
            private string _name;
            private double _factor;
            #endregion

            public string Symbol
            {
                get { return _symbol; }
                set { _symbol = value; }
            }

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public double Factor
            {
                get { return _factor; }
                set { _factor = value; }
            }

            public SIprefix(int exp, string symb, string name)
            {
                _symbol = symb;
                _name = name;
                _factor = Math.Pow(10, exp);
            }

            public SIprefix()
            {
                _symbol = "nosymbol";
                _name = "noname";
                _factor = 1.0;
            }


            public bool Equals(SIprefix other)
            {
                bool equal = true;
                equal = Math.Abs(_factor - other.Factor) < 1.0e-6;
                equal = equal && _symbol.Equals(other.Symbol);
                equal = equal && _name.Equals(other.Name);
                return equal;
            }

            public override string ToString()
            {
                return _symbol;
            }

        }

        public struct Scaling : IEquatable<Scaling>
        {
            private double _factor; // say 60 for a minute, 3600 for hourly equivalent
            private string _symbol; // "HE", "SqFt"

            public double Factor
            {
                get { return _factor; }
                set { _factor = value; }
            }

            public string Symbol
            {
                get { return _symbol; }
                set { _symbol = value; }
            }

            public Scaling(string symbol, double factor = 1.0)
            {
                _symbol = symbol;
                _factor = factor;
            }

            public Scaling()
            {
                _symbol = "";
                _factor = 1.0;
            }

            public bool Equals(Scaling other)
            {
                return (Math.Abs(_factor - other.Factor) < 1e-6) && (_symbol.Equals(other.Symbol));
            }

            public override string ToString()
            {
                return _symbol;
            }

        }

        

        public enum BaseUnitEnum { metre, kilogram, second }

        public enum SI_PrefixEnum {  nano, mikro, milli, centi, deci, unity, deka, hekto, kilo, mega, giga, tera };

        #region memberVariables
        [JsonInclude]
        public int[] _dimensions; // = new int[3];
        protected double _scale;  // To SI-units
        private double _offset;  // To SI-units

        protected SI_PrefixEnum _prefixIndex;
        #endregion

        public double Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        public static SIprefix[] Prefixes
        {
            get { return _prefixes; }
            set { _prefixes = value; }
        }

        public double Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public Unit()
        {
            _dimensions = new int[3] { 0, 0, 0 };
        }

        protected void SetScaling(BaseUnitEnum baseunit, Scaling s)
        {
            _scalings[(int)baseunit] = s;
            RecomputeScale();
        }


        private void RecomputeScale()
        {
            double f = 1.0;
            for (int i = 0; i <= 2; ++i)
            {
                double f1 = 1;
                double f2 = 1;
                if(_scalings.TryGetValue(i, out var scaling))
                {
                    f1 = scaling.Factor;
                }
                if(_prefix.ContainsKey(i))
                {
                    f2 = _prefixes[i].Factor;
                }
                //f = f * Math.Pow(_dimensions[i].Scaling.Factor * _prefixes[(int)_dimensions[i].SI_prefix].Factor, _dimensions[i]);
                f = f * Math.Pow(f1 * f2, _dimensions[i]);
            }
            _scale = f; // look at this: we may have a scale that comes from some other unit change, like for mmHg or Watthour. This will overwrite that value!
        }


        [JsonIgnore]
        protected SIprefix Prefix
        {
            get { return _prefixes[(int)_prefixIndex]; }
        }

        protected SI_PrefixEnum PrefixIndex
        {
            get { return _prefixIndex; }
            set { _prefixIndex = value; }
        }

        public Unit(int exp_metre, int exp_kilogram, int exp_second, double scale = 1.0, double offset = 0.0, SI_PrefixEnum prefix = SI_PrefixEnum.unity)
        {
            
            _dimensions = new int[3] { exp_metre, exp_kilogram, exp_second };
            _scale = scale;
            _offset = offset;
            _prefixIndex = prefix;
            _scale *= Prefix.Factor;
        }


        public double FromSIUnit(double val)
        {
            return (val - _offset) / _scale;
        }


        public static Unit operator *(Unit q1, Unit q2)
        {
            Unit u = new Unit(q1._dimensions[(int)BaseUnitEnum.metre] + q2._dimensions[(int)BaseUnitEnum.metre],
                                q1._dimensions[(int)BaseUnitEnum.kilogram] + q2._dimensions[(int)BaseUnitEnum.kilogram],
                                q1._dimensions[(int)BaseUnitEnum.second] + q2._dimensions[(int)BaseUnitEnum.second]
                                )
            {
                Scale = q1.Scale * q2.Scale
            };


            return u;
        }

        public static Unit operator /(Unit q1, Unit q2)
        {
            Unit u = new Unit(q1._dimensions[(int)BaseUnitEnum.metre] - q2._dimensions[(int)BaseUnitEnum.metre],
                                q1._dimensions[(int)BaseUnitEnum.kilogram] - q2._dimensions[(int)BaseUnitEnum.kilogram],
                                q1._dimensions[(int)BaseUnitEnum.second] - q2._dimensions[(int)BaseUnitEnum.second]);
            u.Scale = q1.Scale / q2.Scale;
            return u;
        }

        public static Unit operator +(Unit q1, int n)
        {
            return new Unit(q1._dimensions[(int)BaseUnitEnum.metre] * n,
                                q1._dimensions[(int)BaseUnitEnum.kilogram] * n,
                                q1._dimensions[(int)BaseUnitEnum.second] * n);
        }

        public static bool operator ==(Unit u1, Unit u2)
        {
            if ((object)u1 == null) return false;
            return u1.Equals(u2);
        }

        public static bool operator !=(Unit u1, Unit u2)
        {
            if ((object)u1 == null ^ (object)u2 == null)
            {
                return true;
            }
            if ((object)u1 == null && (object)u2 == null)
            {
                return false;
            }
            else
            {
                return !u1.Equals(u2);
            }
        }

        public static bool IsBaseUnit(Unit u)
        {
            return _baseUnits.Contains(u, new UnitEqComp());
        }


        public static Unit AsBaseUnit(Unit u)
        {
            Unit bu = null;
            try
            {
                bu = _baseUnits.First(cu => cu.Equals(u));
            }
            catch (InvalidOperationException)
            {

            }
            return bu;
        }

        public static Unit AsDerivedUnit(Unit u)
        {
            Unit du = null;
            try
            {
                du = _derivedUnits.First(cu => cu.SameDimension(u));
            }
            catch (InvalidOperationException)
            {

            }
            du.Scale = u.Scale;
            du.PrefixIndex = u._prefixIndex;

            return du;
        }

        public static bool IsDerivedUnit(Unit u)
        {
            return _derivedUnits.Contains(u, new UnitEqComp());
        }

        public class UnitEqComp : IEqualityComparer<Unit>
        {
            public UnitEqComp()
            {

            }

            #region IEqualityComparer<Unit> Members

            public bool Equals(Unit x, Unit y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(Unit obj)
            {
                return obj._dimensions.Select(s => s).Sum();
            }

            #endregion
        }

        #region IEquatable<Unit> Members
        public bool SameDimension(Unit other)
        {
            bool equals = true;
            for (int i = 0; i <= 2; ++i)
            {
                if (_dimensions[i] != other._dimensions[i])
                {
                    equals = false;
                    break;
                }
            }
            return equals;
        }

        private bool SameScaling(Unit other)
        {
            bool equals = true;
            for (int i = 0; i <= 2; ++i)
            {
                if (!_scalings.ContainsKey(i) ^ !other._scalings.ContainsKey(i))
                {
                    equals = false;
                    break;
                }
                if (!_scalings.ContainsKey(i) && !other._scalings.ContainsKey(i))
                {
                    equals = true;
                    break;
                }
                if (!_scalings[i].Equals(other._scalings[i]))
                {
                    equals = false;
                    break;
                }
            }
            return equals;
        }

        public bool Equals(Unit other)
        {
            bool equals = true;
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            Unit p = other as Unit;
            if ((System.Object)p == null)
            {
                return false;
            }

            equals = SameDimension(other) && SameScaling(other) && (Math.Abs(other.Scale - Scale) < 1e-6);

            return equals;
        }

        public override bool Equals(object other_obj)
        {
            bool equals = true;
            Unit other = other_obj as Unit;
            @equals = other != null && this.Equals(other);
            return equals;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        public virtual Unit Clone()
        {
            return new Unit(_dimensions[0], _dimensions[1], _dimensions[2], _scale, _offset, _prefixIndex);
        }

        public override string ToString()
        {
            StringBuilder sbTaljare = new StringBuilder();
            for (int i = 0; i <= 2; ++i)
            {
                if (_dimensions[i] > 0)
                {
                    if(_prefix.TryGetValue(i, out var value))
                    {
                        sbTaljare.Append(value.Symbol);
                    }

                    string label = _symbols[i];
                    if (_scalings.TryGetValue(i, out var scaling))
                    {
                        label = scaling.Symbol;
                    }
                    if (_dimensions[i] == 1)
                    {
                        sbTaljare.Append(label);
                    }
                    else
                    {
                        sbTaljare.Append(label + _dimensions[i].ToString());
                    }
                    
                }
            }

            StringBuilder sbNamnare = new StringBuilder();
            for (int i = 0; i <= 2; ++i)
            {
                if (_dimensions[i] < 0)
                {
                    if(_prefix.ContainsKey(i))
                    {
                        sbNamnare.Append(_prefix[i].Symbol);
                    }


                    string label = _symbols[i];
                    if (_scalings.TryGetValue(i, out var scaling))
                    {
                        label = scaling.Symbol;
                    }
                    if (_dimensions[i] == -1)
                    {
                        sbNamnare.Append(label);
                    }
                    else
                    {
                        sbNamnare.Append(label + (-_dimensions[i]).ToString());
                    }
                    
                }
            }
            string str = Prefix + ((sbTaljare.Length > 0) ? sbTaljare.ToString() : "1") + ((sbNamnare.Length > 0) ? ("/" + sbNamnare) : "");

            return str;
        }

        public double ToSIUnit(double value)
        {
            return _scale * value + _offset;
        }
    }
}
