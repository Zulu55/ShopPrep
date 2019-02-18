namespace ThreeWays.Core.Services
{
    public class CalculationService : ICalculationService
    {
        public decimal TipAmount(decimal subTotal, double generosity)
        {
            var tip = subTotal * (decimal)(generosity / 100);
            return tip;
        }
    }
}
