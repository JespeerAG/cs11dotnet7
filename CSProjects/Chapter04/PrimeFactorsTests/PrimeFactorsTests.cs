using PrimeFactorsLib;

namespace PrimeFactorsTests
{

    public class UnitTest1
    {
        [Fact]
        public void Test17()
        {
            // arrange
            int a = 17;
            string expected = "17";
            // act
            string actual = PrimeFactorCalculator.PrimeFactorsIter(a);
            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test24()
        {
            // arrange
            int a = 24;
            string expected = "2 x 2 x 2 x 3";
            // act
            string actual = PrimeFactorCalculator.PrimeFactorsIter(a);
            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test81()
        {
            // arrange
            int a = 81;
            string expected = "3 x 3 x 3 x 3";
            // act
            string actual = PrimeFactorCalculator.PrimeFactorsIter(a);
            // assert
            Assert.Equal(expected, actual);
        }
    }
}