namespace TEGFI_3
{
    public static class WealthEstimator
    {
        public static double EstimateTenYearsAmount_Monthly(double inputAmount)
        {
            if (inputAmount > 0.0)
            {
                double totalAmount = 0.0;

                for (int i = 0; i < 120; i++)
                {
                    totalAmount = totalAmount + totalAmount * 0.07 / 12.0 + inputAmount;
                }

                return totalAmount;
            }
            else throw new System.ArgumentOutOfRangeException("Wrong amount");
        }

        public static double EstimateTenYearsAmount_OneTime(double inputAmount)
        {
            if (inputAmount > 0.0)
            {
                double totalAmount = inputAmount;

            for (int i = 0; i < 120; i++)
            {
                totalAmount = totalAmount + totalAmount * 0.07 / 12.0;
            }

            return totalAmount;
            }
            else throw new System.ArgumentOutOfRangeException("Wrong amount");
        }
    }
}
