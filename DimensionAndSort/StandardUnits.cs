namespace GreenOptimizer.DimensionAndSort
{
    public class Units
    {
        public static List<Unit> UnitList;

        static Units()
        {
            UnitList = new List<Unit>()
            {
                Metre, Kilogram, Second, Ampere, Kelvin, Joule
            };
        }

        public static class Lengths
        {
            public static readonly Metre Metre = new();
            public static readonly Centimetre Centimetre = new();
            public static readonly Unit Millimetre = new Metre(){ PrefixIndex = Unit.SI_PrefixEnum.milli, Scale = 1e-3};
            public static readonly Unit Kilometre = new Metre() { PrefixIndex = Unit.SI_PrefixEnum.kilo, Scale = 1e3 };
        }

        public static readonly Metre Metre = new();
        public static Kilogram Kilogram = new Kilogram();
        public static Second Second = new Second();
        public static Ampere Ampere = new Ampere();
        public static Kelvin Kelvin = new Kelvin();
        public static Candela Candela = new Candela();
        public static Mole Mole = new Mole();
        public static Dimensionless Dimensionless = new Dimensionless();
        public static QubicMetre QubicMetre = new QubicMetre();
        public static QubicHectoMetre QubicHectoMetre = new QubicHectoMetre();
        public static QubicMetrePerSecond QubicMetrePerSecond = new QubicMetrePerSecond(); // volumeflow
        public static KilogramPerSecond KilogramPerSecond = new KilogramPerSecond();       // massflow
        public static SquareMetre SquareMetre = new SquareMetre();
        public static HourEquivalent HourEquivalent = new HourEquivalent();
        public static MPH Mph = new MPH();
        public static Knot Knot = new Knot();
        public static Newton Newton = new Newton();
        public static Pascal Pascal = new Pascal();
        public static Joule Joule = new Joule();
        public static Celsius Celsius = new Celsius();
        public static Farenheit Farenheit = new Farenheit();
        public static Minute Minute = new Minute();
        public static Hour Hour = new Hour();
        public static Ohm Ohm = new Ohm();
        public static Volt Volt = new Volt();
        public static Watt Watt = new Watt();
        public static MegaWatt MegaWatt = new MegaWatt();
        public static HorsePower HorsePower = new HorsePower();
        public static Bar Bar = new Bar();
        public static mmHg mmHg = new mmHg();
        public static Litre Litre = new Litre();
        public static Farad Farad = new Farad();
        public static Siemens Siemens = new Siemens();
        public static Weber Weber = new Weber();
        public static Henry Henry = new Henry();
        public static Katal Katal = new Katal();
        public static Tesla Tesla = new Tesla();
        public static BTU BTU = new BTU();
        public static WattHour WattHour = new WattHour();
        public static MegaWattHour MegaWattHour = new MegaWattHour();
        public static Percent Percent = new Percent();
    }

    public abstract class TimeUnit : Unit
    {
        protected TimeUnit() : base(0, 0, 1, 0, 0, 0, 0)
        {

        }
    }

    public abstract class LengthUnit : Unit
    {
        protected LengthUnit() : base(1, 0, 0, 0, 0, 0, 0)
        {

        }
    }

    public abstract class AreaUnit : Unit
    {
        protected AreaUnit() : base(2, 0, 0, 0, 0, 0, 0)
        {

        }
    }

    public abstract class VolumeUnit : Unit
    {
        protected VolumeUnit() : base(3, 0, 0, 0, 0, 0, 0)
        {

        }
    }

    #region PredefinedUnits

    public class Dimensionless : Unit
    {
        public Dimensionless() : base(0, 0, 0, 0, 0, 0, 0) { }
        public override string ToString() { return ""; }
    }

    public class Percent : Dimensionless
    {
        public Percent()
        {
            Scale = 0.01;
        }
        public override string ToString() { return "%"; }
    }


    public class Metre : LengthUnit
    {
        public Metre() { }
    }

    public class Centimetre : Metre
    {
        public Centimetre()
        {
            Scale = 0.01;
        }

        public override string ToString()
        {
            return "cm";
        }
    }

    public class Masl : Metre
    {
        public Masl() { }

        public override string ToString()
        {
            return "masl";
        }
    }

    public class QubicMetre : VolumeUnit
    {
        public QubicMetre() { }

    }

    public class QubicMetrePerSecond : Unit
    {
        public QubicMetrePerSecond() : base(3, 0, -1, 0, 0, 0, 0) { }
    }

    public class KilogramPerSecond : Unit
    {
        public KilogramPerSecond() : base(0, 1, -1, 0, 0, 0, 0) { }
    }

    public class TonnesPerHour : Unit
    {
        public TonnesPerHour() : base(0, 1, -1, 0, 0, 0, 0)
        {
            SetScaling(BaseUnitEnum.kilogram, Scalings.ton);
            SetScaling(BaseUnitEnum.second, Scalings.hour);
        }
        public override string ToString() { return "ton/h"; }
    }


    #region AreaUnits
    public class SquareMetre : AreaUnit
    {
        public SquareMetre() { }

    }


    public class Are : AreaUnit
    {
        public Are()
        {
            Scale = 100;
        }
        public override string ToString() { return "Are"; }
    }

    public class Hectare : AreaUnit
    {
        public Hectare()
        {
            Scale = 10000;
        }
        public override string ToString() { return "Hectare"; }
    }

    public class SquareKilometer : AreaUnit
    {
        public SquareKilometer()
        {
            Scale = 1000000;
        }
        public override string ToString() { return "km2"; }
    }
    #endregion

    #region VolumeUnits
    public class Litre : VolumeUnit
    {
        public Litre()
        {
            this.Scale = 1e-3; // compared to SI-unit m^3
        }
        public override string ToString() { return "l"; }
    }


    public class HourEquivalent : VolumeUnit
    {
        public HourEquivalent() { this.Scale = 3600; }
        public override string ToString() { return "HE"; }
    }

    public class QubicHectoMetre : VolumeUnit
    {
        public QubicHectoMetre()
        {
            Scale = 1e6;

        }
        public override string ToString()
        {
            return "hm3";
        }
    }
    #endregion

    #region SpeedUnits
    public class MPH : Unit
    {
        public MPH() : base(1, 0, -1, 0, 0, 0, 0)
        {
            SetScaling(BaseUnitEnum.metre, Scalings.mile);
            SetScaling(BaseUnitEnum.second, Scalings.hour);
        }
        public override string ToString() { return "MPH"; }
    }

    public class Knot : Unit
    {
        public Knot()
            : base(1, 0, -1, 0, 0, 0, 0)
        {
            Scale = 0.51444;
        }
        public override string ToString() { return "kt"; }
    }
    #endregion

    #region WeightUnits
    public class Kilogram : Unit
    {
        public Kilogram(SI_PrefixEnum prefixIndex = SI_PrefixEnum.unity) : base(0, 1, 0, 0, 0, 0, 0)
        {
            _prefixIndex = prefixIndex;
            _scale *= Prefix.Factor;
        }

        public Kilogram() : base(0, 1, 0, 0, 0, 0, 0)
        {

        }
    }
    #endregion

    #region TimeUnits
    public class Second : TimeUnit
    {
        public Second() { }
    }

    public class Minute : TimeUnit
    {
        public Minute() { SetScaling(BaseUnitEnum.second, Scalings.minute); }
    }

    public class Hour : TimeUnit
    {
        public Hour() { SetScaling(BaseUnitEnum.second, Scalings.hour); }
    }

    public class DayAndNight : TimeUnit
    {
        public DayAndNight() { SetScaling(BaseUnitEnum.second, Scalings.dayAndNight); }
    }

    public class Week : Unit
    {
        public Week() : base(0, 0, 1, 0, 0, 0, 0) { SetScaling(BaseUnitEnum.second, Scalings.week); }
    }
    #endregion

    #region ElectricallUnits
    public class Ampere : Unit
    {
        public Ampere() : base(0, 0, 0, 1, 0, 0, 0) { }
    }

    public class Coulomb : Unit
    {
        public Coulomb() : base(0, 0, 1, 1, 0, 0, 0) { }
        public override string ToString() { return "C"; }
    }

    public class AmpereHour : Coulomb
    {
        public AmpereHour()
        {
            _scale = 3600;

        }
        public override string ToString() { return "Ah"; }
    }

    public class Volt : Unit
    {
        public Volt() : base(2, 1, -3, -1, 0, 0, 0) { }
        public override string ToString() { return "V"; }
    }

    public class Farad : Unit
    {
        public Farad() : base(-2, -1, 4, 2, 0, 0, 0) { }
        public override string ToString() { return "F"; }
    }

    public class Ohm : Unit
    {
        public Ohm() : base(2, 1, -3, -2, 0, 0, 0) { }
        public override string ToString() { return "Ohm"; }
    }

    public class Siemens : Unit
    {
        public Siemens() : base(-2, -1, 3, 2, 0, 0, 0) { }
        public override string ToString() { return "S"; }
    }

    public class Weber : Unit
    {
        public Weber() : base(2, 1, -1, -1, 0, 0, 0) { }
        public override string ToString() { return "Wb"; }
    }

    public class Tesla : Unit
    {
        public Tesla() : base(0, 1, -2, -1, 0, 0, 0) { }
        public override string ToString() { return "T"; }
    }

    public class Henry : Unit
    {
        public Henry() : base(2, 1, -2, -2, 0, 0, 0) { }
        public override string ToString() { return "H"; }
    }
    #endregion

    #region TemperatureUnits
    public class Kelvin : Unit
    {
        public Kelvin() : base(0, 0, 0, 0, 1, 0, 0) { }
    }

    public class Celsius : Unit
    {
        public Celsius() : base(0, 0, 0, 0, 1, 0, 0) { Scale = 1; Offset = 273.15; }
        public override string ToString() { return "C"; }
    }

    public class Farenheit : Unit
    {
        public Farenheit() : base(0, 0, 0, 0, 1, 0, 0) { Scale = 5.0 / 9.0; Offset = 5 * 459.67 / 9; }

        public override string ToString() { return "F"; }
    }

    #endregion

    public class Candela : Unit
    {
        public Candela() : base(0, 0, 0, 0, 0, 1, 0) { }
    }

    public class Mole : Unit
    {
        public Mole() : base(0, 0, 0, 0, 0, 0, 1) { }
    }

    public class Hertz : Unit
    {
        public Hertz() : base(0, 0, -1, 0, 0, 0, 0) { }
        public override string ToString() { return "Hz"; }
    }

    #region ForceUnits
    public class Newton : Unit
    {
        public Newton() : base(1, 1, -2, 0, 0, 0, 0) { }
        public override string ToString() { return "N"; }

    }
    #endregion

    #region Pressureunits
    public class Pascal : Unit
    {
        public Pascal() : base(-1, 1, -2, 0, 0, 0, 0) { }
        public override string ToString() { return "Pa"; }
    }

    public class Bar : Unit
    {
        public Bar() : base(-1, 1, -2, 0, 0, 0, 0) { Scale = 1.0e5; }
        public override string ToString() { return "bar"; }

    }

    public class mmHg : Unit
    {
        public mmHg() : base(-1, 1, -2, 0, 0, 0, 0) { Scale = 133.322368; }
        public override string ToString() { return "mmHg"; }

    }
    #endregion

    #region EnergyUnits
    public class Joule : Unit
    {
        public Joule(SI_PrefixEnum prefixIndex = SI_PrefixEnum.unity) : base(2, 1, -2, 0, 0, 0, 0)
        {
            _prefixIndex = prefixIndex;
            _scale *= Prefix.Factor;
        }

        public Joule() : base(2, 1, -2, 0, 0, 0, 0)
        {
        }
        public override string ToString() { return Prefix + "J"; }
    }

    
    public class WattHour : Unit
    {
        public WattHour(SI_PrefixEnum prefixIndex = SI_PrefixEnum.unity) : base(2, 1, -2, 0, 0, 0, 0)
        {
            Scale = 3600.0;
            _prefixIndex = prefixIndex;
            _scale *= Prefix.Factor;
        }

        public WattHour() : base(2, 1, -2, 0, 0, 0, 0)
        {
            Scale = 3600.0;
        }

        public override string ToString() { return Prefix + "Wh"; }
        public override Unit Clone()
        {
            return new WattHour(this.PrefixIndex);
        }
    }

    public class BTU : Unit
    {
        public BTU(SI_PrefixEnum prefixIndex = SI_PrefixEnum.unity)
            : base(2, 1, -2, 0, 0, 0, 0)
        {
            Scale = 1055.05585;
            _prefixIndex = prefixIndex;
            _scale *= Prefix.Factor;
        }

        public BTU()
                : base(2, 1, -2, 0, 0, 0, 0)
        {
            Scale = 1055.05585;
        }
        public override string ToString() { return Prefix + "BTU"; }
        public override Unit Clone() { return new BTU(this.PrefixIndex); }
    }


    public class ElectronVolt : Unit
    {
        public ElectronVolt() : base(2, 1, -2, 0, 0, 0, 0)
        {
            Scale = 1.60217653e-19;
        }
        public override string ToString() { return "eV"; }
    }
    #endregion

    #region PowerUnits
    public class Watt : Unit
    {
        public Watt(SI_PrefixEnum prefixIndex = SI_PrefixEnum.unity) : base(2, 1, -3, 0, 0, 0, 0)
        {
            _prefixIndex = prefixIndex;
            _scale *= Prefix.Factor;
        }

        public Watt() : base(2, 1, -3, 0, 0, 0, 0)
        {

        }

        public override string ToString() { return "W"; }
    }

    public class MegaWatt : Watt
    {
        public MegaWatt()
        {
            Scale = 1e6;
        }

        public override string ToString() { return "MW"; }
    }

    public class MegaWattHour : Joule
    {
        public MegaWattHour() 
        {
            Scale = 3600*1e6;
        }
        public override string ToString() { return "MWh"; }
    }

    public class HorsePower : Watt
    {
        public HorsePower()
        {
            Scale = 745.7;
        }
        public override string ToString() { return "hp"; }
    }
    #endregion


    public class Lux : Unit
    {
        public Lux() : base(-2, 0, 0, 0, 0, 1, 0) { }
        public override string ToString() { return "lx"; }
    }

    public class Sievert : Unit
    {
        public Sievert() : base(2, 0, -2, 0, 0, 0, 0) { }
        public override string ToString() { return "Sv"; }
    }

    public class Katal : Unit
    {
        public Katal() : base(0, 0, -1, 0, 0, 0, 1) { }
        public override string ToString() { return "kat"; }
    }


    #endregion
}
