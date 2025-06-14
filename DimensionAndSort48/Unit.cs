﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenOptimizer.DimensionAndSort
{

    public class Scalings
    {
        // length
        public static Unit.Scaling metre = new Unit.Scaling("m");
        public static Unit.Scaling foot = new Unit.Scaling("ft", 0.3048);
        public static Unit.Scaling inch = new Unit.Scaling("in", 0.0254);
        public static Unit.Scaling mile = new Unit.Scaling("mi", 1609.344);

        // weight
        public static Unit.Scaling kilogram = new Unit.Scaling("kg");
        public static Unit.Scaling ton = new Unit.Scaling("ton", 1000);

        // time
        public static Unit.Scaling second = new Unit.Scaling("s");
        public static Unit.Scaling hour = new Unit.Scaling("h", 3600);
        public static Unit.Scaling minute = new Unit.Scaling("min", 60);
        public static Unit.Scaling dayAndNight = new Unit.Scaling("d", 86400);
        public static Unit.Scaling week = new Unit.Scaling("w", 604800);
            
        public static Unit.Scaling ampere = new Unit.Scaling("A");
        public static Unit.Scaling kelvin = new Unit.Scaling("K");
        public static Unit.Scaling candela = new Unit.Scaling("Ca"); 
        public static Unit.Scaling mole = new Unit.Scaling("M");
            

    }


    public class Unit : IEquatable<Unit>
    {
        private static SIprefix[] _prefixes = new SIprefix[21] {
                new SIprefix((int) SI_PrefixEnum.yokto,"y","yokto"),
                new SIprefix(-21,"z", "zepto"),
                new SIprefix(-18,"a","atto"),
                new SIprefix(-15,"f","femto"),
                new SIprefix(-12,"p","piko"),
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
                new SIprefix(15,"P","peta"),
                new SIprefix(18,"E","exa"),
                new SIprefix(21,"Z","zetta"),
                new SIprefix(24,"Y","yotta")};

        private static Unit[] _baseUnits = new Unit[7] { new Metre(), new Kilogram(), new Second(), new Ampere(), new Kelvin(), new Candela(), new Mole() };
        private static Unit[] _derivedUnits = new Unit[15] { new Hertz(), new Newton(), new Pascal(), new Joule(), new Watt(), new Coulomb(), new Volt(), new Farad(), new Ohm(), new Siemens(), new Weber(), new Tesla(), new Henry(), new Lux(), new Katal() };

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

           

            public bool Equals(Scaling other)
            {
                return (Math.Abs(_factor - other.Factor) < 1e-6) && (_symbol.Equals(other.Symbol));
            }

            public override string ToString()
            {
                return _symbol;
            }

        }

        public struct DimensionUnit
        {
            #region fields
            private int _exponent;
            private SI_PrefixEnum _SI_prefix;
            private Scaling _scaling;
            #endregion

            public int Exponent
            {
                get { return _exponent; }
                set { _exponent = value; }
            }

            public Scaling Scaling
            {
                get { return _scaling; }
                set { _scaling = value; }
            }

            public SI_PrefixEnum SI_prefix
            {
                get { return _SI_prefix; }
                set { _SI_prefix = value; }
            }

            public DimensionUnit(DimensionUnit u)
            {
                _exponent = u.Exponent;
                _SI_prefix = u.SI_prefix;
                _scaling = u.Scaling;
            }

            public void CopyTo(DimensionUnit u)
            {
                u.Exponent = _exponent;
                u._SI_prefix = _SI_prefix;
                u._scaling = _scaling;
            }

            public DimensionUnit(int exponent, Scaling scale, SI_PrefixEnum prefix)
            {
                _exponent = exponent;
                _SI_prefix = prefix;
                _scaling = scale;
            }

           

            public override string ToString()
            {
                return _prefixes[(int)_SI_prefix].Name;
            }
        }

        public enum BaseUnitEnum { metre, kilogram, second, ampere, kelvin, candela, mole }

        public enum DerivedUnitEnum { hertz, newton, pascal, joule, watt, coulomb, volt, farad, ohm, siemens, weber, tesla, henry, lux, katal }

        public enum SI_PrefixEnum { yokto, zepto, atto, femto, piko, nano, mikro, milli, centi, deci, unity, deka, hekto, kilo, mega, giga, tera, peta, exa, zetta, yotta };

        #region memberVariables
        protected DimensionUnit[] _dimensions = new Unit.DimensionUnit[7];
        protected double _scale;  // To SI-units
        protected double _offset;  // To SI-units

        protected SI_PrefixEnum _prefixIndex;
        #endregion

        public double Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        public static SIprefix[] Prefixes
        {
            get { return Unit._prefixes; }
            set { Unit._prefixes = value; }
        }

        public double Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public Unit()
        {

        }

        protected void SetScaling(BaseUnitEnum baseunit, Scaling s)
        {
            _dimensions[(int)baseunit].Scaling = s;
            RecomputeScale();
        }


        private void RecomputeScale()
        {
            double f = 1.0;
            for (int i = 0; i < 7; ++i)
            {
                f = f * Math.Pow(_dimensions[i].Scaling.Factor * _prefixes[(int)_dimensions[i].SI_prefix].Factor, _dimensions[i].Exponent);
            }
            _scale = f; // look at this: we may have a scale that comes from some other unit change, like for mmHg or Watthour. This will overwrite that value!
        }

        public static Unit[] BaseUnits
        {
            get { return Unit._baseUnits; }
        }

        public static Unit[] DerivedUnits
        {
            get { return Unit._derivedUnits; }
        }

        public DimensionUnit[] Dimensions
        {
            get { return _dimensions; }
            set { _dimensions = value; }
        }

        public SIprefix Prefix
        {
            get { return _prefixes[(int)_prefixIndex]; }
        }

        public SI_PrefixEnum PrefixIndex
        {
            get { return _prefixIndex; }
            set { _prefixIndex = value; }
        }

        public Unit(int exp_metre, int exp_kilogram, int exp_second, int exp_ampere, int exp_kelvin, int exp_candela, int exp_mole, double scale = 1.0, double offset = 0.0, SI_PrefixEnum prefix = SI_PrefixEnum.unity)
        {
            _dimensions[(int)Unit.BaseUnitEnum.metre] = new Unit.DimensionUnit(exp_metre, Scalings.metre, SI_PrefixEnum.unity);
            _dimensions[(int)Unit.BaseUnitEnum.kilogram] = new Unit.DimensionUnit(exp_kilogram, Scalings.kilogram, SI_PrefixEnum.unity);
            _dimensions[(int)Unit.BaseUnitEnum.second] = new Unit.DimensionUnit(exp_second, Scalings.second, SI_PrefixEnum.unity);
            _dimensions[(int)Unit.BaseUnitEnum.ampere] = new Unit.DimensionUnit(exp_ampere, Scalings.ampere, SI_PrefixEnum.unity);
            _dimensions[(int)Unit.BaseUnitEnum.kelvin] = new Unit.DimensionUnit(exp_kelvin, Scalings.kelvin, SI_PrefixEnum.unity);
            _dimensions[(int)Unit.BaseUnitEnum.candela] = new Unit.DimensionUnit(exp_candela, Scalings.candela, SI_PrefixEnum.unity);
            _dimensions[(int)Unit.BaseUnitEnum.mole] = new Unit.DimensionUnit(exp_mole, Scalings.mole, SI_PrefixEnum.unity);

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
            Unit u = new Unit(q1._dimensions[(int)BaseUnitEnum.metre].Exponent + q2._dimensions[(int)BaseUnitEnum.metre].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.kilogram].Exponent + q2._dimensions[(int)BaseUnitEnum.kilogram].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.second].Exponent + q2._dimensions[(int)BaseUnitEnum.second].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.ampere].Exponent + q2._dimensions[(int)BaseUnitEnum.ampere].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.kelvin].Exponent + q2._dimensions[(int)BaseUnitEnum.kelvin].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.candela].Exponent + q2._dimensions[(int)BaseUnitEnum.candela].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.mole].Exponent + q2._dimensions[(int)BaseUnitEnum.mole].Exponent);


            u.Scale = q1.Scale * q2.Scale;

            return u;
        }

        public static Unit operator /(Unit q1, Unit q2)
        {
            Unit u = new Unit(q1._dimensions[(int)BaseUnitEnum.metre].Exponent - q2._dimensions[(int)BaseUnitEnum.metre].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.kilogram].Exponent - q2._dimensions[(int)BaseUnitEnum.kilogram].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.second].Exponent - q2._dimensions[(int)BaseUnitEnum.second].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.ampere].Exponent - q2._dimensions[(int)BaseUnitEnum.ampere].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.kelvin].Exponent - q2._dimensions[(int)BaseUnitEnum.kelvin].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.candela].Exponent - q2._dimensions[(int)BaseUnitEnum.candela].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.mole].Exponent - q2._dimensions[(int)BaseUnitEnum.mole].Exponent);
            u.Scale = q1.Scale / q2.Scale;
            return u;
        }

        public static Unit operator +(Unit q1, int n)
        {
            return new Unit(q1._dimensions[(int)BaseUnitEnum.metre].Exponent * n,
                                q1._dimensions[(int)BaseUnitEnum.kilogram].Exponent * n,
                                q1._dimensions[(int)BaseUnitEnum.second].Exponent * n,
                                q1._dimensions[(int)BaseUnitEnum.ampere].Exponent * n,
                                q1._dimensions[(int)BaseUnitEnum.kelvin].Exponent * n,
                                q1._dimensions[(int)BaseUnitEnum.candela].Exponent * n,
                                q1._dimensions[(int)BaseUnitEnum.mole].Exponent * n);
        }

        public static bool operator ==(Unit u1, Unit u2)
        {
            if ((object)u1 == null) return false;
            return u1.Equals(u2);
        }

        public static bool operator !=(Unit u1, Unit u2)
        {
            if ((object)u1 == null || (object)u2 == null)
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
                return obj._dimensions.Select(s => s.Exponent).Sum();
            }

            #endregion
        }

        #region IEquatable<Unit> Members
        public bool SameDimension(Unit other)
        {
            bool equals = true;
            for (int i = 0; i <= 6; ++i)
            {
                if (_dimensions[i].Exponent != other._dimensions[i].Exponent)
                {
                    equals = false;
                    break;
                }
            }
            return equals;
        }

        public bool SameScaling(Unit other)
        {
            bool equals = true;
            for (int i = 0; i <= 6; ++i)
            {
                if (!_dimensions[i].Scaling.Equals(other._dimensions[i].Scaling))
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
            return new Unit(_dimensions[0].Exponent, _dimensions[1].Exponent, _dimensions[2].Exponent, _dimensions[3].Exponent, _dimensions[4].Exponent, _dimensions[5].Exponent, _dimensions[6].Exponent, _scale, _offset, _prefixIndex);
        }

        public override string ToString()
        {
            StringBuilder sbTaljare = new StringBuilder();
            for (int i = 0; i <= 6; ++i)
            {
                if (_dimensions[i].Exponent > 0)
                {
                    sbTaljare.Append(Unit.Prefixes[(int)_dimensions[i].SI_prefix].Symbol);
                    if (_dimensions[i].Exponent == 1)
                    {
                        sbTaljare.Append(_dimensions[i].Scaling);
                    }
                    else
                    {
                        sbTaljare.Append(_dimensions[i].Scaling + _dimensions[i].Exponent.ToString());
                    }
                }
            }

            StringBuilder sbNamnare = new StringBuilder();
            for (int i = 0; i <= 6; ++i)
            {
                if (_dimensions[i].Exponent < 0)
                {
                    sbNamnare.Append(Unit.Prefixes[(int)_dimensions[i].SI_prefix].Symbol);
                    if (_dimensions[i].Exponent == -1)
                    {
                        sbNamnare.Append(_dimensions[i].Scaling);
                    }
                    else
                    {
                        sbNamnare.Append(_dimensions[i].Scaling + (-_dimensions[i].Exponent).ToString());
                    }
                }
            }
            string str = Prefix + ((sbTaljare.Length > 0) ? sbTaljare.ToString() : "1") + ((sbNamnare.Length > 0) ? ("/" + sbNamnare) : "");

            return str;
        }
    }
}
