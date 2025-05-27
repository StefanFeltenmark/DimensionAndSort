namespace GreenOptimizer.DimensionAndSort
{

    public static class Constants
    {
        public static Quantity PlanckConstant = new Quantity(6.6260695729e-34, new Unit(2, 1, -1), Unit.SI_PrefixEnum.unity, "h");
        public static Quantity GravitationalConstant = new Quantity(6.67384e-11, new Unit(3, -1, -2), Unit.SI_PrefixEnum.unity, "G");
        public static Quantity GravityOfEarth = new Quantity(9.81, new Unit(1, 0, -2), Unit.SI_PrefixEnum.unity, "g");
        
    }
    
}
