using DimensionAndSort;

namespace GreenOptimizer.DimensionAndSort
{
    public static class Extensions
    {
        public static Unit ToBaseUnit(this Unit unit)
        {
            return Unit.AsBaseUnit(unit);
        }

        public static Unit ToDerivedUnit(this Unit unit)
        {
            return Unit.AsDerivedUnit(unit);
        }

        public static Quantity ToUnit(this Quantity q, Unit u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity)
        {
            Quantity qu = new Quantity(q);
            qu.SetUnit(u);
            qu.SetPrefix(prefix);
            return qu;
        }


        public static Quantity ToPrefix(this Quantity q, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity)
        {
            Quantity qu = new Quantity(q);
            qu.SetPrefix(prefix);
            return qu;
        }

    }
}