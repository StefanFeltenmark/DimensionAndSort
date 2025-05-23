using System;
using DimensionAndSort;
using Xunit;
using System.Reflection;

namespace UnitTests
{
    /// <summary>
    ///     Summary description for PhysicalUnitsTest
    /// </summary>
    public class PhysicalUnitsTest
    {
        [Fact]
        public void TestLength1()
        {
            var l1 = new Length(100, Unit.SI_PrefixEnum.milli);

            var l2 = new Length(1);

            var l3 = l1 + l2;

            Assert.True(l3.Unit.Equals(Units.Metre));
        }

        [Fact]
        public void TestLength2()
        {
            var l1 = new Length(100, Unit.SI_PrefixEnum.milli);

            var l2 = new Length(1);

            var l4 = l2 + l1;

            l4.SetPrefix(Unit.SI_PrefixEnum.milli);

            Assert.True(l4.PrefixIndex == Unit.SI_PrefixEnum.milli);

        }

        [Fact]
        public void TestLength3()
        {
            var l1 = new Length(100, Unit.SI_PrefixEnum.milli);

            var l2 = new Length(1);

            var l5 = l2 - l1;

            var l6 = l1 - l2;

            Assert.True(l5.Equals(-l6));

        }

        [Fact]
        public void TestLength5()
        {
            var l1 = new Length(100, Units.Lengths.Centimetre);

            var l2 = new Length(1);

            var l5 = l2 + l1;

          

            Assert.True(l5.Unit.Equals(Units.Metre));

        }

        [Fact]
        public void TestLength6()
        {
            var l1 = new Length(1000, Units.Lengths.Millimetre);

            var l2 = new Length(1);

            var l5 = l2 + l1;

            Assert.True(l5.Unit.Equals(Units.Metre));

            var l6 = l5.ConvertToUnit(Units.Lengths.Kilometre);

            string text = $"l6 = {l6}";

        }

        [Fact]
        public void TestVolume1()
        {
            var l1 = new Length(100, Unit.SI_PrefixEnum.milli);

            var l2 = new Length(1);

            var l3 = l1 + l2;

            var l4 = l2 + l1;

            Volume v1 = l1 * l2 * l3;
            
            Assert.True(v1.Unit.Equals(Units.QubicMetre));

            Assert.Throws<IncompatibleUnits>(() =>
                {
                    var tests = v1.ConvertToDerivedUnit();
                    return tests;
                }
            );
        }


        [Fact]
        public void TestVolume2()
        {
            Volume v1 = new Volume(100);

            v1.SetUnit(Units.HourEquivalent);
        }

        [Fact]
        public void TestLength4()
        {
            var l1 = new Length(100, Unit.SI_PrefixEnum.milli);

            var l2 = new Length(1);

            DimensionlessQuantity r = l2 / l1;

            Assert.True(r.Unit.SameDimension(Units.Dimensionless));
        }

        [Fact]
        public void TestArea()
        {
            var l1 = new Length(100, Unit.SI_PrefixEnum.milli);

            var l2 = new Length(1); // meter

            var a = l1 * l2;

            Assert.True(a.Value.Equals(0.1));
            Assert.True(a.Unit.Equals(Units.SquareMetre));

        }

        [Fact]
        public void TestTime1()
        {
            var t1 = new Time(1000, Units.Second, Unit.SI_PrefixEnum.milli);
            var t2 = new Time(2000, Units.Second, Unit.SI_PrefixEnum.milli);

            var t3 = t1 + t2;
            var t4 = t1 - t2;

            Assert.True(t3 > t4);

        }

        [Fact]
        public void TestEinstein()
        {
            Mass m = new Mass(90);

            var e = (m * QuantityBase.Pow(Constants.SpeedOfLight, 2)).AdjustPrefix(); // todo

            e.AdjustPrefix();

            
            e.SetPrefix(Unit.SI_PrefixEnum.giga);

            Assert.True(e.Unit.SameDimension(Units.WattHour));

        }


        [Fact]
        public void TestAcceleration()
        {
            var t1 = new Time(1000, Units.Second, Unit.SI_PrefixEnum.milli);
            var t2 = new Time(2000, Units.Second, Unit.SI_PrefixEnum.milli);

            var r = t1 * t2;

            var l1 = new Length(100, Units.Metre, Unit.SI_PrefixEnum.milli);

            Acceleration? a = null;
            try
            {
                a = l1 / r;
            }
            catch (IncompatibleUnits)
            {
                Console.Out.Write("Mäh");
            }

            Assert.True(a is Acceleration);

        }

        [Fact]
        public void TestSpeed1()
        {
            var t1 = new Time(1, Units.Second);
            var l1 = new Length(100, Units.Metre);
            
            Speed s1 = l1 / t1;

            s1.SetUnit(Units.Mph);


            // should be 
            Assert.Equal(223.693629, s1.Value, 6);
        }

        [Fact]
        public void TestSpeed2()
        {
            var t1 = new Time(1000, Units.Second, Unit.SI_PrefixEnum.milli);
            var t2 = new Time(2000, Units.Second, Unit.SI_PrefixEnum.milli);
            var l1 = new Length(100, Units.Metre, Unit.SI_PrefixEnum.milli);

            Speed s1 = l1 / t1;
            Speed s2 = l1 / t2;

            var s3 = s1 + s2;

            var s = s3.ToString();
        }

        [Fact]
        public void TestVolume()
        {
            var v1 = new Volume(2, Units.HourEquivalent);
            var v2 = new Volume(1000, Unit.SI_PrefixEnum.hekto);
            var v3 = v1 + v2;
        }

        [Fact]
        public void TestPowerOf()
        {
            Length l = new Length(10, Unit.SI_PrefixEnum.centi);
            
            Volume v = QuantityBase.Pow(l, 3).ToUnit(Units.Litre);
            Assert.True(v != null);
        }

        [Fact]
        public void TestGravity()
        {
            var m = new Mass(100);

            var v = new Volume(1000);

            Density d = null;

            try
            {
                d = m / v;
            }
            catch (IncompatibleUnits)
            {
                Console.Out.WriteLine("test");
            }

            var g = new Acceleration(9.82);

            Force f = m * g;
            
            Console.Out.Write("f = " + f);
        }

        [Fact]
        public void TestTemperature()
        {
            var t1 = new Temperature(0, Units.Celsius);
            var t2 = new Temperature(32, Units.Farenheit);
            var t3 = t1 + t2; // gives 273,15 degrees Celsius!

            Assert.Equal(273.15, t3.Value, 2);
        }

        [Fact]
        public void TestVolume7()
        {
            var l1 = new Length(3);
            var l2 = new Length(4);
            var l3 = new Length(5);

            Volume v1 = QuantityBase.Pow(l1, 3);

            Volume v2 = l1 * l2 * l3;

            var vdiff = v1 - v2;

            Area a = v2 / l1;
        }

        [Fact]
        public void TestTime()
        {
            var t1 = new Time(30, Units.Minute);

            var t2 = new Time(3, Units.Hour);

            var t3 = new Time(120);

            var t4 = t1 + t2 + t3;

            var multiplier = new DimensionlessQuantity(5);

            Time t5 = multiplier * t1;

            t5.SetUnit(t1.Unit);
        }

        [Fact]
        public void TestPotentialKineticEnergy()
        {
            Energy kinetic = (new Mass(10)) * QuantityBase.Pow(new Speed(10), 2) / (new DimensionlessQuantity(2));

            Energy potential = null;

            var m2 = new Mass(91);
            var gravity = new Acceleration(9.82);
            var heightAboveEarth = new Length(100);

            try
            {
                potential = m2 * gravity * heightAboveEarth;
            }
            catch (IncompatibleUnits e)
            {
                Console.Out.WriteLine(e.Message);
            }

            //  potential.SetUnit(Units.Joule);
            potential.SetPrefix(Unit.SI_PrefixEnum.kilo);
        }

        [Fact]
        public void TestOhmsLaw()
        {
            //trying ohms famous law...
            var R = new Resistance(1);
            var I = new Current(5);

            Voltage U = R * I;

            Assert.Equal(5,U.Value,2);
            Assert.True(U.Unit.Equals(Units.Volt));
        }

        [Fact]
        public void TestPower1()
        {
            var U = new Voltage(20, Unit.SI_PrefixEnum.milli);
            var I = new Current(5, Unit.SI_PrefixEnum.milli);

            Power P = U * I;

            P.SetPrefix(Unit.SI_PrefixEnum.mikro);
        }

        [Fact]
        public void TestPower2()
        {
            var U = new Voltage(20, Unit.SI_PrefixEnum.milli);
            var I = new Current(5, Unit.SI_PrefixEnum.milli);
            var R = new Resistance(1, Unit.SI_PrefixEnum.hekto);

            Power P2 = (R * QuantityBase.Pow(I, 2)).ToPrefix(Unit.SI_PrefixEnum.mega);

            Power P3 = (QuantityBase.Pow(U, 2) / R).ToPrefix(Unit.SI_PrefixEnum.kilo);

            var t = new Time(1, Units.Hour);
            
            Energy e = (P2 - P3) * t;

        }

        [Fact]
        public void TestImpulse1()
        {
            var q = new MyImpulseQuantity(10, MyImpulseUnit.myImpulseUnit);
            var s = new Speed(10);

            Energy e = (q * s).ToPrefix(Unit.SI_PrefixEnum.kilo);
        }

        [Fact]
        public void TestIdealGasLaw()
        {
            // Ideal gas law
            var p = new Pressure(1013, Units.Bar, Unit.SI_PrefixEnum.milli);

            p.SetUnit(Units.mmHg);

            var T = new Temperature(28, Units.Celsius); // 28 degrees celsius in Kelvin
            var n = new AmountOfSubstance(100);

            Volume V = (Constants.GasConstant * n * T) / p;
        }


        [Fact]
        public void TestVolueFlow()
        {
            var f = new VolumeFlow(10);

            var v = new Volume(100);

            var he = new HourEquivalent();

            var h = new Hour();

            var t = new Time(60);

            VolumeFlow f2 = v / t;

            var str = f2.ToString();
        }

        [Fact]
        public void TestVolumes()
        {
            var v1 = new Volume(10, Units.QubicMetre, Unit.SI_PrefixEnum.mega);

            var v2 = new Volume(10, Units.HourEquivalent);

            var v3 = v1 + v2;

            var test = v1.ToString();

            var v4 = v1.ValueInSIUnits;

            v1.SetUnit(Units.HourEquivalent);

            var q = new Quantity(1, Units.HourEquivalent);

            Volume q2 = q.ToUnit(Units.QubicHectoMetre);

            Volume q3 = q2.ConvertToUnit(Units.HourEquivalent);
        }


        [Fact]
        public void TestMAssFlow()
        {
            var f1 = new MassFlow(100);
            f1.SetUnit(new TonnesPerHour());

            var f2 = new MassFlow(100);

            var f3 = f1 + f2;
        }

        [Fact]
        public void TestToDerivedUnit1()
        {
            var u1 = (Units.Joule / Units.Second).ToDerivedUnit();

            var p1 = new Power(100, u1);

            Assert.True(p1.Unit.Equals(Units.Watt));
        }

        [Fact]
        public void TestToDerivedUnit2()
        {
            var u = (Units.WattHour / Units.Hour).ToDerivedUnit();

            var p2 = new Power(100, u, Unit.SI_PrefixEnum.mega);

            Assert.True(p2.Unit.Equals(Units.Watt));
        }

        [Fact]
        public void TestToDerivedUnit3()
        {
            var u = Units.Watt*Units.Hour;

            Assert.True(u.Equals(Units.WattHour));
        }

        [Fact]
        public void TestWaterValue1()
        {
            var p = new MonetaryAmount(100);

            var v = new Volume(40);

            var WaterValue = p / v;

            string name = WaterValue.Unit.ToString();
        }

        [Fact]
        public void TestCurrencyExchange()
        {
            Currencies.NorwegianCrown.ExchangeRateToEur = 0.122098533;
            Currencies.SwedishCrown.ExchangeRateToEur = 0.108485427;

            var amount1 = new MonetaryAmount(100, Currencies.NorwegianCrown);
            var amount2 = new MonetaryAmount(100, Currencies.SwedishCrown);

            MonetaryAmount amount3 = (amount1 + amount2).ConvertToUnit(Currencies.USDollar);
            var diff = amount1 - amount2;

            MonetaryAmount amount4 = amount1.ConvertToUnit(Currencies.Euro);

        }

        [Fact]
        public void TestUnitPrice1()
        {
            var p1 = new UnitPrice(100, new PriceUnit(Currencies.Euro, new BTU(Unit.SI_PrefixEnum.mega)));

            var p2 = new UnitPrice(100, new PriceUnit(Currencies.USDollar, new WattHour(Unit.SI_PrefixEnum.mega)));


            if (p1.Price > p2.Price)
            {
                Console.Out.Write("P1 is cheaper");
            }
            var s = p1.ToString();

            var p3 = p1.ConvertToUnit(p2.PriceUnit);
        }

        [Fact]
        public void TestToDerivedunit2()
        {
            var u1 = Units.Watt * Units.Hour;

            var test = u1 == Units.WattHour;

            var u = u1.ToDerivedUnit();

            var e1 = new Energy(100, u, Unit.SI_PrefixEnum.kilo);
            var e2 = new Energy(100, Units.WattHour);

            var e3 = e1 + e2;
        }

        [Fact]
        public void TestGravitation1()
        {
            var m = new Mass(90);

            Force f = Constants.GravityOfEarth * m;

            var h = new Length(10);

            Energy e = (f * h).ToUnit(Units.WattHour);

            e.SetPrefix(Unit.SI_PrefixEnum.kilo);

            Energy e2 = e.ConvertToUnit(Units.WattHour);
        }

        [Fact]
        public void TestNewtonsSecondLaw()
        {
            var m = new Mass(90, new Kilogram(Unit.SI_PrefixEnum.mega));
            var a = new Acceleration(10);

            Force f =  m * a;

            f.AdjustPrefix();
            
        }

        [Fact]
        public void TestHeatingValue()
        {
            var h1 = new HeatingValue(46858);
            var d = new Density(0.0008);

            var u = new WattHour(Unit.SI_PrefixEnum.mega) / new QubicMetre();

            var q = h1 * d;

            var q2 = q.ConvertToUnit(u).AdjustPrefix();

            var q3 = q.ToUnit(u);
        }

        [Fact]
        public void TestEnergy()
        {
            var e1 = new Energy(1, Unit.SI_PrefixEnum.mega);
            var e2 = new Energy(1002, Unit.SI_PrefixEnum.kilo);

            Assert.True(e1 <= e2);
        }

        [Fact]
        public void TestPowerToEnergy()
        {
            Power p1 = new (1, Unit.SI_PrefixEnum.mega);
            Time t = new Time(3600);

            Unit MWh = new MegaWattHour();

            Energy e = p1 * t;

            e.SetUnit(MWh);

            var e1 = e.ConvertToUnit(MWh);
            
            Assert.True(Math.Abs(e1.Value - 1.0) < 1e-6);

            Assert.True(e1.Unit.Equals(MWh));
        }

        [Fact]
        public void TestToDerived()
        {
            Force f = new Force(100, Unit.SI_PrefixEnum.kilo);
            Length l = new Length(100);
            Time t = new Time(3600);

            var P = (f * l / t).ConvertToDerivedUnit();

            

            Assert.True(P.Unit.Equals(Units.Watt));

        }

        [Fact]
        public void TestEnergy2()
        {
            Unit GWh = new WattHour(Unit.SI_PrefixEnum.giga);
            Unit MWh = new WattHour(Unit.SI_PrefixEnum.mega);
            Energy e1 = new(300, GWh);
            var e2 = e1.ConvertToUnit(MWh);

            Assert.True(e2.Value.Equals(3e5));

        }


        [Fact]
        public void PercentageTest()
        {
            var e1 = new Energy(1, Unit.SI_PrefixEnum.mega);
            var p1 = new Percentage(10);
            var p2 = new Percentage(25);

            Energy e3 = ((p1 + p2) * e1).ConvertToUnit(Units.MegaWattHour);

            Assert.Equal(3.5e5, e3.ValueInSIUnits,3);
        }

        [Fact]
        public void MegaWattHourTest()
        {
            var e1 = new Energy(1, Unit.SI_PrefixEnum.mega);
            Energy e2 = e1.ConvertToUnit(Units.WattHour);

            Energy e3 = e1.ConvertToUnit(new WattHour(Unit.SI_PrefixEnum.mega));
        }

        [Fact]
        public void HeatingValueTest()
        {
            var lhv1 = new HeatingValue(1);

            var u = (new WattHour(Unit.SI_PrefixEnum.mega)) / (new Kilogram());

            HeatingValue lhv2 = lhv1.ConvertToUnit(u);
        }

        [Fact]
        public void SpecificEnergyTest()
        {
            var u = (new WattHour(Unit.SI_PrefixEnum.kilo)) / (new Kilogram(Unit.SI_PrefixEnum.kilo)); // kW/ton

            var e = new SpecificEnergy(7560.0, u);

            var u1 = (new Joule(Unit.SI_PrefixEnum.giga)) / (new Kilogram(Unit.SI_PrefixEnum.kilo)); // kW/ton

            SpecificEnergy e2 = e.ConvertToUnit(u1);
        }

        [Fact]
        public void EnergyEquivalentTest()
        {
            var u = (new WattHour(Unit.SI_PrefixEnum.kilo)) / (new QubicMetre()); // kWh/m^3

            var e = new EnergyEquivalent(1.0, u);

            var u1 = (new WattHour(Unit.SI_PrefixEnum.mega)) / (new QubicHectoMetre()); // MWh/MM3

            EnergyEquivalent e2 = e.ConvertToUnit(u1);
        }

        [Fact]
        public void EnergyEquivalentSumTest()
        {
            var u = (new WattHour(Unit.SI_PrefixEnum.mega)) / (new QubicHectoMetre()); // MWh/MM3

            var e1 = new EnergyEquivalent(0.0, u);
            var e2 = new EnergyEquivalent(0.41, u);


            EnergyEquivalent sum = e1 + e2;

            Assert.True(sum.Value.Equals(0.41));
        }

        [Fact]
        public void AreaLengthVolumeTest()
        {
            var v = new Volume(1, new QubicHectoMetre());

            var l = new Length(10);

            Area a = v / l;

            a = a.ConvertToUnit(new SquareKilometer());


            Assert.True(Math.Abs(a.Value - 0.1) < 1e-3);
        }

        [Fact]
        public void ScaleTest()
        {
            var v = new Volume(1, new QubicHectoMetre());

            var v2 = new Volume(10, Units.Litre);

            var fac = v / v2;
        }

        [Fact]
        public void TestIncompatibleUnits()
        {

            Length l = new Length(10);

            Time t = new Time(5, Units.Hour);

            Assert.Throws<IncompatibleUnits>(() =>
            {
                var test = l + t;
            });


        }

        [Fact]
        public void TestIncompatibleUnits2()
        {

            Length l = new Length(10);

            Force f = new Force(100, Units.Newton, Unit.SI_PrefixEnum.kilo);

            Assert.Throws<IncompatibleUnits>(() =>
            {
                Power p = f * l;
            });


        }

        [Fact]
        public void ReflectionTest()
        {
            Length l = new Length(10, new Metre(), Unit.SI_PrefixEnum.milli);
            Unit matched = null;
            foreach (Unit unit in Units.UnitList)
            {
                if (l.Unit == unit)
                {
                    matched = unit; 
                    break;
                }
            }

            Assert.True(matched == Units.Metre);
        }

        [Fact]
        public void ReflectionTest2()
        {
            Mass m = new Mass(2);
            Speed s = new Speed(3);
            var kinetic = 0.5 * m * s * s;

            Unit matched = null;
            foreach (Unit unit in Units.UnitList)
            {
                if (kinetic.Unit == unit)
                {
                    matched = unit;
                    break;
                }
            }

            Assert.True(matched == Units.Joule);
        }

        [Fact]
        public void TestHorsePower()
        {
            var EnginePower1 = new Power(450, Units.HorsePower);
            var EnginePower2 = new Power(450, new Watt(Unit.SI_PrefixEnum.kilo));

            bool test = EnginePower2 > EnginePower1;

            Assert.True(test);

        }

        public class MyImpulseUnit : Unit
        {
            public static readonly MyImpulseUnit myImpulseUnit = new MyImpulseUnit();

            private MyImpulseUnit() : base(1, 1, -1, 0, 0, 0, 0)
            {
                Scale = 3.1415;
            }
        }

        private class MyImpulseQuantity : QuantityBase
        {
            public MyImpulseQuantity(double val, Unit u) : base(val, u)
            {
            }
        }

        [Fact]
        public void TestDetectingUnits()
        {
            Length lc = new Length(10, Unit.SI_PrefixEnum.centi);
            Length lm = new Length(10, Unit.SI_PrefixEnum.milli);
            Length lk = new Length(10, Unit.SI_PrefixEnum.hekto);

            Length L = lk + 2 * lm + 5 * lc;

            var test1 = L.Unit.ToDerivedUnit();
            var test2 = L.Unit.ToBaseUnit();
            string label = L.ToString();

        }




        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion
    }
}