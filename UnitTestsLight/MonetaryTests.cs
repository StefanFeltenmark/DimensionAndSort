using System;
using System.Globalization;
using Xunit;
using GreenOptimizer.DimensionAndSort;

namespace UnitTestsLight
{
    public class MonetaryTests
    {
        [Fact]
        public void Currency_ToString_ReturnsName()
        {
            var currency = new Currency("USD");
            Assert.Equal("USD", currency.ToString());
        }

        [Fact]
        public void Currency_Clone_CreatesCopy()
        {
            var currency = new Currency("USD") { ExchangeRateToEur = 1.2 };
            var clone = (Currency)currency.Clone();

            Assert.Equal(currency.Name, clone.Name);
            Assert.Equal(currency.ExchangeRateToEur, clone.ExchangeRateToEur);
            Assert.Equal(currency.Scale, clone.Scale);
            Assert.NotSame(currency, clone);
        }

        [Fact]
        public void Currency_ExchangeRateToEur_UpdatesScale()
        {
            var currency = new Currency("USD");
            currency.ExchangeRateToEur = 1.5;
            Assert.Equal(1.5, currency.Scale);
        }

        [Fact]
        public void Currency_ConversionRate_CalculatesCorrectly()
        {
            var usd = new Currency("USD") { ExchangeRateToEur = 1.2 };
            var eur = new Currency("EUR") { ExchangeRateToEur = 1.0 };
            double rate = Currency.ConversionRate(usd, eur);
            Assert.Equal(1.2, rate, 5);
        }

        [Fact]
        public void MonetaryAmount_ImplicitDoubleConversion_Works()
        {
            MonetaryAmount amount = 42.5;
            Assert.Equal(42.5, amount.Value);
            Assert.Equal(Currencies.Euro, amount.Unit);
        }

        [Fact]
        public void MonetaryAmount_ImplicitQuantityConversion_SameDimension_Works()
        {
            var quantity = new Quantity(10, Currencies.Euro, Unit.SI_PrefixEnum.unity);
            MonetaryAmount amount = quantity;
            Assert.Equal(10, amount.Value);
            Assert.Equal(Currencies.Euro, amount.Unit);
        }

        [Fact]
        public void MonetaryAmount_ImplicitQuantityConversion_DifferentDimension_Throws()
        {
            var quantity = new Quantity(10, Units.Metre, Unit.SI_PrefixEnum.unity);
            Assert.Throws<IncompatibleUnits>(() => { MonetaryAmount amount = quantity; });
        }

        [Fact]
        public void MonetaryAmount_AdditionOperator_Works()
        {
            var a = new MonetaryAmount(10);
            var b = new MonetaryAmount(15);
            var sum = a + b;
            Assert.True(sum.Value > 24.9 && sum.Value < 25.1);
        }

        [Fact]
        public void MonetaryAmount_SubtractionOperator_Works()
        {
            var a = new MonetaryAmount(20);
            var b = new MonetaryAmount(5);
            var diff = a - b;
            Assert.True(diff.Value > 14.9 && diff.Value < 15.1);
        }

        [Fact]
        public void MonetaryAmount_Clone_CreatesCopy()
        {
            var amount = new MonetaryAmount(100, Currencies.Euro, Unit.SI_PrefixEnum.unity);
            var clone = (MonetaryAmount)amount.Clone();
            Assert.Equal(amount.Value, clone.Value);
            Assert.Equal(amount.Unit, clone.Unit);
            Assert.Equal(amount.PrefixIndex, clone.PrefixIndex);
            Assert.NotSame(amount, clone);
        }

        [Theory]
        [InlineData("en-US", "12.5 EUR/m")]
        [InlineData("de-DE", "12,5 EUR/m")]
        public void UnitPrice_ToString_RespectsLocale(string cultureName, string expectedNumber)
        {
            var originalCulture = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo(cultureName);
                var priceUnit = new PriceUnit(Currencies.Euro, Units.Metre);
                var unitPrice = new UnitPrice(12.5m, priceUnit);
                var str = unitPrice.ToString();
                Assert.Contains(expectedNumber, str);
                Assert.Contains("EUR", str);
            }
            finally
            {
                CultureInfo.CurrentCulture = originalCulture;
            }
        }

        [Fact]
        public void UnitPrice_Clone_CreatesCopy()
        {
            var priceUnit = new PriceUnit(Currencies.Euro, Units.Metre);
            var unitPrice = new UnitPrice(12.5m, priceUnit);
            var clone = (UnitPrice)unitPrice.Clone();
            Assert.Equal(unitPrice.Price, clone.Price);
            Assert.NotSame(unitPrice.PriceUnit, clone.PriceUnit);
        }

        [Fact]
        public void UnitPrice_ConvertToUnit_SameDimension_ConvertsCorrectly()
        {
            var priceUnit1 = new PriceUnit(Currencies.Euro, Units.Metre);
            var priceUnit2 = new PriceUnit(Currencies.USDollar, Units.Metre);
            priceUnit2.Currency.ExchangeRateToEur = 2.0; // 1 EUR = 2 USD
            var unitPrice = new UnitPrice(10m, priceUnit1);

            var converted = unitPrice.ConvertToUnit(priceUnit2);

            // 10 EUR * (1/2) = 5 USD
            Assert.Equal(5m, converted.Price, 2);
            Assert.Equal(priceUnit2.Currency.Name, converted.PriceUnit.Currency.Name);
        }

        [Fact]
        public void UnitPrice_ConvertToUnit_DifferentDimension_Throws()
        {
            var priceUnit1 = new PriceUnit(Currencies.Euro, Units.Metre);
            var priceUnit2 = new PriceUnit(Currencies.Euro, Units.Kilogram);
            var unitPrice = new UnitPrice(10m, priceUnit1);

            Assert.Throws<IncompatibleUnits>(() => unitPrice.ConvertToUnit(priceUnit2));
        }

        [Theory]
        [InlineData("en-US", "EUR/m")]
        [InlineData("de-DE", "EUR/m")]
        public void PriceUnit_ToString_RespectsLocale(string cultureName, string expectedCurrencyUnit)
        {
            var originalCulture = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo(cultureName);
                var priceUnit = new PriceUnit(Currencies.Euro, Units.Metre);
                var str = priceUnit.ToString();
                Assert.Contains(expectedCurrencyUnit, str);
                Assert.Contains("/", str);
            }
            finally
            {
                CultureInfo.CurrentCulture = originalCulture;
            }
        }

        [Fact]
        public void PriceUnit_Clone_CreatesCopy()
        {
            var priceUnit = new PriceUnit(Currencies.Euro, Units.Metre);
            var clone = (PriceUnit)priceUnit.Clone();
            Assert.Equal(priceUnit.Currency.Name, clone.Currency.Name);
            Assert.Equal(priceUnit.Unit.ToString(), clone.Unit.ToString());
            Assert.NotSame(priceUnit, clone);
        }
    }
}
