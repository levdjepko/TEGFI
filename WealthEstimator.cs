namespace TEGFI_3
{
    public static class WealthEstimator
    {
        public static double EstimateTenYearsAmount_Monthly(double inputAmount)
        {
            double totalAmount = 0.0;

            for (int i = 0; i < 120; i++)
            {
                totalAmount = totalAmount + totalAmount * 0.07 / 12.0 + inputAmount;
            }

            return totalAmount;
        }

        public static double EstimateTenYearsAmount_OneTime(double inputAmount)
        {
            double totalAmount = inputAmount;

            for (int i = 0; i < 120; i++)
            {
                totalAmount = totalAmount + totalAmount * 0.07 / 12.0;
            }

            return totalAmount;
        }
    }
}
