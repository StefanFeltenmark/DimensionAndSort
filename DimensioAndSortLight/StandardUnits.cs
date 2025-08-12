namespace GreenOptimizer.DimensionAndSort
{
    public class Units
    {
        static Units()
        {

        }
        public static Metre Metre = new Metre();
        public static Kilogram Kilogram = new Kilogram();
        public static Second Second = new Second();
        
        public static Dimensionless Dimensionless = new Dimensionless();
        public static QubicMetre QubicMetre = new QubicMetre();
        public static QubicHectoMetre QubicHectoMetre = new QubicHectoMetre();
        public static QubicMetrePerSecond QubicMetrePerSecond = new QubicMetrePerSecond(); // volumeflow
        public static KilogramPerSecond KilogramPerSecond = new KilogramPerSecond();       // massflow
        public static SquareMetre SquareMetre = new SquareMetre();
        public static HourEquivalent HourEquivalent = new HourEquivalent();
        public static Newton Newton = new Newton();
        
        public static Joule Joule = new Joule();
        
        public static Minute Minute = new Minute();
        public static Hour Hour = new Hour();
        
        public static Watt Watt = new Watt();
        public static MegaWatt MegaWatt = new MegaWatt();
        
        public static Litre Litre = new Litre();
        
        public static WattHour WattHour = new WattHour();
        public static MegaWattHour MegaWattHour = new MegaWattHour();
        public static Percent Percent = new Percent();

        public static readonly MegaWattPerMinute MegaWattPerMinute = new MegaWattPerMinute();
    }

    public abstract class TimeUnit : Unit
    {
        protected TimeUnit() : base(0, 0, 1)
        {

        }
    }

    public abstract class LengthUnit : Unit
    {
        protected LengthUnit() : base(1, 0, 0)
        {

        }
    }

    public abstract class AreaUnit : Unit
    {
        protected AreaUnit() : base(2, 0, 0)
        {

        }
    }

    #region PredefinedUnits

    public class Dimensionless : Unit
    {
        public Dimensionless() : base(0, 0, 0) { }
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

    public class Masl : Metre
    {
        public Masl() { }

        public override string ToString()
        {
            return "masl";
        }
    }

    public class QubicMetre : Unit
    {
        public QubicMetre() : base(3, 0, 0) { }
    }

    public class QubicMetrePerSecond : Unit
    {
        public QubicMetrePerSecond() : base(3, 0, -1) { }
    }

    public class KilogramPerSecond : Unit
    {
        public KilogramPerSecond() : base(0, 1, -1) { }
    }

    public class TonnesPerHour : Unit
    {
        public TonnesPerHour() : base(0, 1, -1)
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
    public class Litre : Unit
    {
        public Litre() : base(3, 0, 0) { this.Scale = 1e-3; }
        public override string ToString() { return "l"; }
    }


    public class HourEquivalent : Unit
    {
        public HourEquivalent() : base(3, 0, 0) { this.Scale = 3600; }
        public override string ToString() { return "HE"; }
    }

    public class QubicHectoMetre : Unit
    {
        public QubicHectoMetre() : base(3, 0, 0, 1e6) { }
        public override string ToString()
        {
            return "hm3";
        }
    }
    #endregion

    #region SpeedUnits
    

    #endregion

    #region WeightUnits
    public class Kilogram : Unit
    {
        public Kilogram(SI_PrefixEnum prefixIndex = SI_PrefixEnum.unity) : base(0, 1, 0)
        {
            _prefixIndex = prefixIndex;
            _scale *= Prefix.Factor;
        }

        public Kilogram() : base(0, 1, 0)
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
        public Week() : base(0, 0, 1) { SetScaling(BaseUnitEnum.second, Scalings.week); }
    }
    #endregion

  
    

    #region ForceUnits
    public class Newton : Unit
    {
        public Newton() : base(1, 1, -2) { }
        public override string ToString() { return "N"; }

    }
    #endregion

    #region Pressureunits
  
   
    #endregion

    #region EnergyUnits
    public class Joule : Unit
    {
        public Joule(SI_PrefixEnum prefixIndex = SI_PrefixEnum.unity) : base(2, 1, -2)
        {
            _prefixIndex = prefixIndex;
            _scale *= Prefix.Factor;
        }

        public Joule() : base(2, 1, -2)
        {
        }
        public override string ToString() { return Prefix + "J"; }
    }

    public class WattHour : Unit
    {
        public WattHour(SI_PrefixEnum prefixIndex = SI_PrefixEnum.unity) : base(2, 1, -2)
        {
            Scale = 3600.0;
            _prefixIndex = prefixIndex;
            _scale *= Prefix.Factor;
        }

        public WattHour() : base(2, 1, -2)
        {
            Scale = 3600.0;
        }

        public override string ToString() { return Prefix + "Wh"; }
        public override Unit Clone()
        {
            return new WattHour(this.PrefixIndex);
        }
    }

   
  
    #endregion

    #region PowerUnits
    public class Watt : Unit
    {
        public Watt(SI_PrefixEnum prefixIndex = SI_PrefixEnum.unity) : base(2, 1, -3)
        {
            _prefixIndex = prefixIndex;
            _scale *= Prefix.Factor;
        }

        public Watt() : base(2, 1, -3)
        {

        }

        public override string ToString() { return "W"; }
    }

    public class MegaWatt : Unit
    {
        public MegaWatt() : base(2, 1, -3) { Scale = 1e6; }

        public override string ToString() { return "MW"; }
    }

    public class MegaWattHour : Unit
    {
        public MegaWattHour() : base(2, 1, -2)
        {
            Scale = 1e6;
        }
        public override string ToString() { return "MWh"; }
    }

    
    public class MegaWattPerMinute : Unit
    {
        public MegaWattPerMinute() : base(2, 1, -4)
        {
            Scale = 1000000.0/60.0;
        }

        public override string ToString()
        {
            return "MW/min";
        }
    }
    
    #endregion


   

    #endregion
}
