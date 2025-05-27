using System;

namespace GreenOptimizer.DimensionAndSort
{
    public class Currencies
    {
        public static Euro Euro = new Euro();
        public static USDollar USDollar = new USDollar();
        public static SwedishCrown SwedishCrown = new SwedishCrown();
        public static NorwegianCrown NorwegianCrown = new NorwegianCrown();
        public static TurkishLira TurkishLira = new TurkishLira();
    }

    public class Currency : Unit, ICloneable
    {
        private double _exchangeRateToEUR;

        public Currency(string code) : base(0, 0, 0, 0, 0, 0, 0, 0)
        {
            Name = code;
            _exchangeRateToEUR = 1.0;
            Scale = _exchangeRateToEUR;
        }

        public string Name { get; set; }

        // This number changes every day!
        public double ExchangeRateToEur
        {
            get { return _exchangeRateToEUR; }
            set
            {
                _exchangeRateToEUR = value;
                Scale = _exchangeRateToEUR;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public new object Clone()
        {
            Currency clone = new Currency(Name);
            clone.ExchangeRateToEur = _exchangeRateToEUR;
            clone.Scale = _scale;
            return clone;
        }

        public static double ConversionRate(Currency fromCurrency, Currency toCurrency)
        {
            return fromCurrency.ExchangeRateToEur / toCurrency.ExchangeRateToEur;
        }
    }

    public class USDollar : Currency
    {
        public USDollar() : base("USD")
        {
        }
    }

    public class SwedishCrown : Currency
    {
        public SwedishCrown()
            : base("SEK")
        {
        }
    }

    public class NorwegianCrown : Currency
    {
        public NorwegianCrown()
            : base("NOK")
        {
        }
    }

    public class Euro : Currency
    {
        public Euro()
            : base("EUR")
        {
        }
    }

    public class TurkishLira : Currency
    {
        public TurkishLira()
            : base("TRY")
        {
        }
    }

    public class MonetaryAmount : QuantityBase
    {
        protected static Currency _euro = Currencies.Euro;

        public MonetaryAmount(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity)
            : base(val, _euro, prefix)
        {
        }

        public MonetaryAmount(double val, Unit unit, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity)
            : base(val, unit, prefix)
        {
        }

        public MonetaryAmount()
        {

        }

        public static implicit operator MonetaryAmount(double val)
        {
            return new MonetaryAmount(val);
        }

        public static implicit operator MonetaryAmount(Quantity mq)
        {
            if (mq.Unit.SameDimension(_euro))
            {
                return new MonetaryAmount(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            throw new IncompatibleUnits();
        }

        public static MonetaryAmount operator +(MonetaryAmount q1, MonetaryAmount q2)
        {
            return new MonetaryAmount(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor,
                q1.Unit, q1.PrefixIndex);
        }

        public static MonetaryAmount operator -(MonetaryAmount q1, MonetaryAmount q2)
        {
            return new MonetaryAmount(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor,
                q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new MonetaryAmount(_value, _unit, _prefixIndex);
        }
    }

    public class UnitPrice : ICloneable
    {
        public UnitPrice(decimal price, PriceUnit priceUnit)
        {
            Price = price;
            PriceUnit = priceUnit;
        }

        public decimal Price { get; set; }

        public PriceUnit PriceUnit { get; set; }

        public override string ToString()
        {
            return Price + " " + PriceUnit;
        }

        public object Clone()
        {
            return new UnitPrice(Price, (PriceUnit)PriceUnit.Clone());
        }

        public UnitPrice ConvertToUnit(PriceUnit newunit)
        {
            UnitPrice p = null;
            if (newunit.Unit.SameDimension(PriceUnit.Unit))
            {
                var factor1 = PriceUnit.Unit.FromSIUnit(1) / newunit.Unit.FromSIUnit(1);

                var priceConversionRate = Currency.ConversionRate(PriceUnit.Currency, newunit.Currency);

                var newValue = Price * ((decimal)priceConversionRate) * ((decimal)factor1);

                p = new UnitPrice(newValue, newunit);
            }
            else
            {
                throw new IncompatibleUnits();
            }
            return p;
        }

        //        Unit unit = newUnit;
        //        double value = newUnit.FromSIUnit(ValueInSIUnits);
        //    {
        //    if (newUnit.SameDimension(_unit))
        //    Quantity q = null;
        //{

        //public Quantity CovertToUnit(Unit newUnit)
        //        Unit.SI_PrefixEnum prefixIndex = Unit.SI_PrefixEnum.unity;
        //        q = new Quantity(value, unit, prefixIndex);
        //    }
        //    else
        //    {
        //        throw new IncompatibleUnits();
        //    }
        //    return q;
        //}
    }

    public class PriceUnit : ICloneable
    {
        public PriceUnit()
        {
            Currency = Currencies.Euro;
            Unit = Units.Dimensionless;
        }

        public PriceUnit(Currency currency, Unit quantityUnit)
        {
            Currency = currency;
            Unit = quantityUnit.Clone();
        }

        public Currency Currency { get; set; }

        public Unit Unit { get; set; }


        public override string ToString()
        {
            return Currency + "/" + Unit;
        }

        public object Clone()
        {
            return new PriceUnit((Currency)Currency.Clone(), Unit.Clone());
        }
    }
}