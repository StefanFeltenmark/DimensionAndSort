namespace GreenOptimizer.DimensionAndSort
{
    public class Length : QuantityBase
    {

        public Length(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, new Metre(), prefix) { }

        public Length(double val, Unit unit, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, unit, prefix) { }

        public Length()
        {

        }
        public static implicit operator Length(double val) { return new Length(val); }
        public static implicit operator Length(Quantity mq)
        {
            if (mq.Unit.SameDimension(new Metre()))
            {
                return new Length(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }

        }
        public static Length operator +(Length q1, Length q2)
        {
            return new Length(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Length operator -(Length q1, Length q2)
        {
            return new Length(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Length operator -(Length q1)
        {
            return new Length(-q1.Value, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Length(_value, _unit, _prefixIndex);
        }
    }

    public class Time : QuantityBase
    {
        public static TimeUnit _second = Units.Second;
        public Time(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _second, prefix) { }
        public Time(double val, Unit unit, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, unit, prefix) { }
        public Time()
        {

        }
        public static implicit operator Time(double val) { return new Time(val, _second); }
        public static implicit operator Time(Quantity mq)
        {
            if (mq.Unit.SameDimension(_second))
            {
                return new Time(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Time operator +(Time q1, Time q2)
        {
            return new Time(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Time operator -(Time q1, Time q2)
        {
            return new Time(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Time(_value, _unit, _prefixIndex);
        }
    }

    public class Mass : QuantityBase
    {
        protected static Kilogram _kilo = new Kilogram();
        public Mass(double val) : base(val, _kilo) { }
        public Mass(double val, Unit unit) : base(val, unit) { }
        public Mass(double val, Unit unit, Unit.SI_PrefixEnum prefix) : base(val, unit, prefix) { }

        public Mass()
        {

        }


        public static implicit operator Mass(double val)
        {
            return new Mass(val);
        }
        public static implicit operator Mass(Quantity mq)
        {
            if (mq.Unit.SameDimension(_kilo))
            {
                return new Mass(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Mass operator +(Mass q1, Mass q2)
        {
            return new Mass(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Mass operator -(Mass q1, Mass q2)
        {
            return new Mass(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Mass(_value, _unit, _prefixIndex);
        }
    }

   

    public class DimensionlessQuantity : QuantityBase
    {
        protected static Dimensionless _dimless = Units.Dimensionless;
        public DimensionlessQuantity(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, new Dimensionless(), prefix) { }
        public static implicit operator DimensionlessQuantity(double val) { return new DimensionlessQuantity(val); }
        public static implicit operator DimensionlessQuantity(Quantity mq)
        {
            if (mq.Unit.SameDimension(_dimless))
            {
                return new DimensionlessQuantity(mq.Value, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }

        public override QuantityBase Clone()
        {
            return new DimensionlessQuantity(_value, _prefixIndex);
        }
    }

    public class Percentage : QuantityBase
    {
        protected static Percent _perc = Units.Percent;
        public Percentage(double val) : base(val, Units.Percent, Unit.SI_PrefixEnum.unity, "%") { }

        public static implicit operator Percentage(double val) { return new Percentage(val); }
        public static implicit operator Percentage(Quantity mq)
        {
            if (mq.Unit.SameDimension(_perc))
            {
                return new Percentage(mq.Value);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }

        public override QuantityBase Clone()
        {
            return new Percentage(_value);
        }


    }

    public class Volume : QuantityBase
    {
        private static QubicMetre _qubicMetreUnit = Units.QubicMetre;
        public Volume(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _qubicMetreUnit, prefix) { }
        public Volume(double val, Unit unit, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, unit, prefix) { }

        public Volume()
        {

        }
        public static implicit operator Volume(double val)
        {
            return new Volume(val);
        } // NB lose prefix!
        public static implicit operator Volume(Quantity mq)
        {
            if (mq.Unit.SameDimension(_qubicMetreUnit))
            {
                return new Volume(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Volume operator +(Volume q1, Volume q2)
        {
            return new Volume(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits), q1.Unit, q1.PrefixIndex);
        }

        public static Volume operator -(Volume q1, Volume q2)
        {
            return new Volume(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits), q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Volume(_value, _unit, _prefixIndex);
        }

    }

    public class VolumeFlow : QuantityBase
    {
        private QubicMetrePerSecond _qubicMetrePerSecondUnit = new QubicMetrePerSecond();

        public VolumeFlow(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, new QubicMetrePerSecond(), prefix) { }
        public VolumeFlow(double val, Unit unit, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, unit, prefix) { }

        public VolumeFlow()
        {

        }
        public static implicit operator VolumeFlow(double val) { return new VolumeFlow(val); }
        public static implicit operator VolumeFlow(Quantity mq)
        {
            if (mq.Unit.SameDimension(new QubicMetrePerSecond()))
            {
                return new VolumeFlow(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static VolumeFlow operator +(VolumeFlow q1, VolumeFlow q2)
        {
            return new VolumeFlow(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static VolumeFlow operator -(VolumeFlow q1, VolumeFlow q2)
        {
            return new VolumeFlow(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new VolumeFlow(_value, _unit, _prefixIndex);
        }

    }

    public class MassFlow : QuantityBase
    {
        protected static KilogramPerSecond _kilogramPerSecondUnit = Units.KilogramPerSecond;

        public MassFlow()
        {
            
        }
        public MassFlow(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _kilogramPerSecondUnit, prefix) { }
        public MassFlow(double val, Unit unit, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, unit, prefix) { }
        public static implicit operator MassFlow(double val) { return new MassFlow(val); }
        public static implicit operator MassFlow(Quantity mq)
        {
            if (mq.Unit.SameDimension(_kilogramPerSecondUnit))
            {
                return new MassFlow(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static MassFlow operator +(MassFlow q1, MassFlow q2)
        {
            return new MassFlow(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static MassFlow operator -(MassFlow q1, MassFlow q2)
        {
            return new MassFlow(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new MassFlow(_value, _unit, _prefixIndex);
        }

    }

    public class Area : QuantityBase
    {
        protected static SquareMetre _squareMetreUnit = new SquareMetre();

        public Area(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, new SquareMetre(), prefix) { }
        public Area(double val, Unit unit, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, unit, prefix) { }

        public Area() : base(0, new SquareMetre(), Unit.SI_PrefixEnum.unity)
        {

        }

        public static implicit operator Area(double val) { return new Area(val); }
        public static implicit operator Area(Quantity mq)
        {
            if (mq.Unit.SameDimension(_squareMetreUnit))
            {
                return new Area(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Area operator +(Area q1, Area q2)
        {
            return new Area(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Area operator -(Area q1, Area q2)
        {
            return new Area(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Area(_value, _unit, _prefixIndex);
        }


    }

    public class Speed : QuantityBase
    {
        private static Unit _metrePerSecond = new Unit(1, 0, -1);

        public Speed()
        {
            
        }
        public Speed(double val) : base(val, _metrePerSecond) { }
        public Speed(double val, Unit unit) : base(val, unit) { }
        public Speed(double val, Unit unit, Unit.SI_PrefixEnum prefix) : base(val, unit, prefix) { }
        public static implicit operator Speed(double val) { return new Speed(val); }
        public static implicit operator Speed(Quantity mq)
        {
            if (mq.Unit.SameDimension(_metrePerSecond))
            {
                return new Speed(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }

        public static Speed operator +(Speed q1, Speed q2)
        {
            return new Speed(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Speed operator -(Speed q1, Speed q2)
        {
            return new Speed(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Speed(_value, _unit, _prefixIndex);
        }

    }

    public class Acceleration : QuantityBase
    {
        private static Unit _metrePerSecond2 = new Unit(1, 0, -2);

        public Acceleration()
        {
            
        }

        public Acceleration(double val) : base(val, _metrePerSecond2) { }
        public Acceleration(double val, Unit unit) : base(val, unit) { }
        public Acceleration(double val, Unit unit, Unit.SI_PrefixEnum prefix) : base(val, unit, prefix) { }
        public static implicit operator Acceleration(double val) { return new Acceleration(val); }
        public static implicit operator Acceleration(Quantity mq)
        {
            if (mq.Unit.SameDimension(_metrePerSecond2))
            {
                return new Acceleration(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Acceleration operator +(Acceleration q1, Acceleration q2)
        {
            return new Acceleration(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Acceleration operator -(Acceleration q1, Acceleration q2)
        {
            return new Acceleration(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Acceleration(_value, _unit, _prefixIndex);
        }

    }

    public class Energy : QuantityBase
    {
        private static Joule _jouleUnit = new Joule();
        public Energy(Energy e) : base(e.Value, e.Unit, e.PrefixIndex) { }
        public Energy(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _jouleUnit, prefix) { }
        public Energy(double val, Unit u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator Energy(double val) { return new Energy(val); }

        public Energy()
        {

        }
        public static implicit operator Energy(Quantity mq)
        {
            if (mq.Unit.SameDimension(_jouleUnit))
            {
                if (mq.Unit == _jouleUnit)
                {
                    return new Energy(mq.Value, _jouleUnit, mq.PrefixIndex);
                }
                else
                {
                    Energy e = new Energy(mq.Value, mq.Unit, mq.PrefixIndex);
                    return e;
                }

            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Energy operator +(Energy q1, Energy q2)
        {
            return new Energy(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Energy operator -(Energy q1, Energy q2)
        {
            return new Energy(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Energy(_value, _unit, _prefixIndex);
        }

    }

    public class EnergyEquivalent : QuantityBase
    {
        private static Unit _energyPerVolume = new Joule() / new QubicMetre();

        public EnergyEquivalent(EnergyEquivalent e) : base(e.Value, e.Unit, e.PrefixIndex) { }
        public EnergyEquivalent() { }
        public EnergyEquivalent(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _energyPerVolume, prefix) { }
        public EnergyEquivalent(double val, Unit u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator EnergyEquivalent(double val) { return new EnergyEquivalent(val); }
        public static implicit operator EnergyEquivalent(Quantity mq)
        {
            if (mq.Unit.SameDimension(_energyPerVolume))
            {
                if (mq.Unit == _energyPerVolume)
                {
                    return new EnergyEquivalent(mq.Value, _energyPerVolume, mq.PrefixIndex);
                }
                else
                {
                    EnergyEquivalent e = new EnergyEquivalent(mq.Value, mq.Unit, mq.PrefixIndex);
                    //   e.SetUnit(_energyPerVolume); // why?
                    return e;
                }

            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static EnergyEquivalent operator +(EnergyEquivalent q1, EnergyEquivalent q2)
        {
            return new EnergyEquivalent(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static EnergyEquivalent operator -(EnergyEquivalent q1, EnergyEquivalent q2)
        {
            return new EnergyEquivalent(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static EnergyEquivalent operator -(EnergyEquivalent q1)
        {
            return new EnergyEquivalent(q1.Unit.FromSIUnit(-q1.ValueInSIUnits), q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new EnergyEquivalent(_value, _unit, _prefixIndex);
        }

    }

    public class HeatingValue : QuantityBase
    {
        private static Joule _energyUnit = new Joule(Unit.SI_PrefixEnum.kilo);
        private static Kilogram _weightUnit = new Kilogram();
        private static Unit _heatingValueUnit = _energyUnit / _weightUnit;
        public HeatingValue(HeatingValue e) : base(e.Value, e.Unit, e.PrefixIndex) { }
        public HeatingValue(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _heatingValueUnit, prefix) { }
        public HeatingValue(double val, Unit u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator HeatingValue(double val) { return new HeatingValue(val); }
        public static implicit operator HeatingValue(Quantity mq)
        {
            if (mq.Unit.SameDimension(_heatingValueUnit))
            {
                if (mq.Unit == _heatingValueUnit)
                {
                    return new HeatingValue(mq.Value, _heatingValueUnit, mq.PrefixIndex);
                }
                else
                {
                    HeatingValue e = new HeatingValue(mq.Value, mq.Unit, mq.PrefixIndex);
                    //   e.SetUnit(_energyPerVolume); // why?
                    return e;
                }

            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static HeatingValue operator +(HeatingValue q1, HeatingValue q2)
        {
            return new HeatingValue(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static HeatingValue operator -(HeatingValue q1, HeatingValue q2)
        {
            return new HeatingValue(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new HeatingValue(_value, _unit, _prefixIndex);
        }

    }

    public class SpecificEnergy : QuantityBase
    {
        private static Joule _energyUnit = new Joule();
        private static Kilogram _weightUnit = new Kilogram();
        private static Unit _specificEnergyUnit = _energyUnit / _weightUnit;
        public SpecificEnergy(SpecificEnergy e) : base(e.Value, e.Unit, e.PrefixIndex) { }
        public SpecificEnergy(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _specificEnergyUnit, prefix) { }
        public SpecificEnergy(double val, Unit u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator SpecificEnergy(double val) { return new SpecificEnergy(val); }
        public static implicit operator SpecificEnergy(Quantity mq)
        {
            if (mq.Unit.SameDimension(_specificEnergyUnit))
            {
                if (mq.Unit == _specificEnergyUnit)
                {
                    return new SpecificEnergy(mq.Value, _specificEnergyUnit, mq.PrefixIndex);
                }
                else
                {
                    SpecificEnergy e = new SpecificEnergy(mq.Value, mq.Unit, mq.PrefixIndex);
                    return e;
                }

            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static SpecificEnergy operator +(SpecificEnergy q1, SpecificEnergy q2)
        {
            return new SpecificEnergy(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static SpecificEnergy operator -(SpecificEnergy q1, SpecificEnergy q2)
        {
            return new SpecificEnergy(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new SpecificEnergy(_value, _unit, _prefixIndex);
        }

    }

    public class Force : QuantityBase
    {
        static Newton _newton = Units.Newton;

        public Force()
        {
            
        }

        public Force(double val) : base(val, _newton) { }
        public Force(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _newton, prefix) { }
        public Force(double val, Unit u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator Force(double val) { return new Force(val); }
        public static implicit operator Force(Quantity mq)
        {
            if (mq.Unit.SameDimension(_newton))
            {
                if (mq.Unit == _newton)
                {
                    return new Force(mq.Value, _newton, mq.PrefixIndex);
                }
                else
                {
                    Force f = new Force(mq.Value, mq.Unit, mq.PrefixIndex);
                    f.SetUnit(_newton);
                    return f;
                }

            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Force operator +(Force q1, Force q2)
        {
            return new Force(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Force operator -(Force q1, Force q2)
        {
            return new Force(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Force(_value, _unit, _prefixIndex);
        }

    }

    

    public class Density : QuantityBase
    {
        static Unit _densityUnit = new Unit(-3, 1, 0);
        public Density(Density p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Density(double val) : base(val, _densityUnit) { }
        public Density(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _densityUnit, prefix) { }
        public Density(double val, Unit u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator Density(double val) { return new Density(val); }
        public static implicit operator Density(Quantity mq)
        {
            if (mq.Unit.SameDimension(_densityUnit))
            {

                return new Density(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Density operator +(Density q1, Density q2)
        {
            return new Density(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Density operator -(Density q1, Density q2)
        {
            return new Density(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Density(_value, _unit, _prefixIndex);
        }

    }

    

    public class PowerRampRate : QuantityBase
    {
        static Unit _wattsPerSecond = Units.Watt / Units.Second;

        public PowerRampRate(PowerRampRate p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public PowerRampRate(double val) : base(val, _wattsPerSecond) { }
        public PowerRampRate(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _wattsPerSecond, prefix) { }
        public PowerRampRate(double val, Unit u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator PowerRampRate(double val) { return new PowerRampRate(val); }
        public static implicit operator PowerRampRate(Quantity mq)
        {
            if (mq.Unit.SameDimension(_wattsPerSecond))
            {
                if (mq.Unit == _wattsPerSecond)
                {
                    return new PowerRampRate(mq.Value, _wattsPerSecond, mq.PrefixIndex);
                }
                else
                {
                    PowerRampRate p = new PowerRampRate(mq.Value, mq.Unit, mq.PrefixIndex);
                    p.SetUnit(_wattsPerSecond);
                    return p;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static PowerRampRate operator +(PowerRampRate q1, PowerRampRate q2)
        {
            return new PowerRampRate(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static PowerRampRate operator -(PowerRampRate q1, PowerRampRate q2)
        {
            return new PowerRampRate(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new PowerRampRate(this);
        }
    }

    public class Power : QuantityBase
    {
        static Watt _watt = new Watt();
        public Power(Power p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Power(double val = 0.0) : base(val, _watt) { }
        public Power(double val = 0.0, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _watt, prefix) { }
        public Power(double val = 0.0, Unit u = null, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }

        public Power()
        {

        }
        public static implicit operator Power(double val) { return new Power(val); }
        public static implicit operator Power(Quantity mq)
        {
            if (mq.Unit.SameDimension(_watt))
            {
                if (mq.Unit == _watt)
                {
                    return new Power(mq.Value, _watt, mq.PrefixIndex);
                }
                else
                {
                    Power p = new Power(mq.Value, mq.Unit, mq.PrefixIndex);
                    p.SetUnit(_watt);
                    return p;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Power operator +(Power q1, Power q2)
        {
            return new Power(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Power operator -(Power q1, Power q2)
        {
            return new Power(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Power(this);
        }
    }

   
}
