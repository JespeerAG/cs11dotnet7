namespace PrimeFactorsLib
{
    public static class PrimeFactorCalculator
    {
        public static string PrimeFactors(int number)
        {
            if (number == 1)
            {
                return number.ToString();
            }
            else
            {
                for (int i = 2; i <= number; i++)
                {
                    if (number % i == 0)
                    {
                        return $"{i} x {PrimeFactors(number / i)}";
                    }

                }

                return "ERROR";
            }
        }

        public static string PrimeFactorsIter(int number)
        {
            if (number == 1)
            {
                return number.ToString();
            }
            else
            {
                string output = String.Empty;
                int i = 2;
                while (i <= number & number >= 2)
                {
                    if (number % i == 0)
                    {
                        if (output.Equals(String.Empty))
                        {
                            output += i.ToString();
                        }
                        else
                        {
                            output += $" x {i.ToString()}";
                        }
                        number = number / i;
                        i = 1; // Will become 2 in line 49.
                    }
                    i++;
                }

                return output;
            }
        }

        public static bool IsPrime(int number)
        {
            int maxBound = (int)Math.Floor(Math.Sqrt(number));
            for (int i = 2; i <= maxBound; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
