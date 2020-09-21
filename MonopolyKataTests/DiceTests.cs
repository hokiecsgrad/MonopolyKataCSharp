using System;
using MonopolyKata;
using MonopolyKata.Spaces;
using Xunit;

namespace MonopolyKataTests
{
    public class DiceTests
    {
        [Fact]
        public void Roll_Normal_ShouldAlwaysBeBetween2And12()
        {
            Random generator = new Random();
            int total = 0;
            int min = int.MaxValue;
            int max = int.MinValue;
            SixSidedDie die = new SixSidedDie(generator);

            for (int i = 0; i < 500; i++)
            {
                total = die.Roll();
                total += die.Roll();
                if ( total < min ) min = total;
                if ( total > max ) max = total;
            }

            Assert.Equal(2, min);
            Assert.Equal(12, max);
        }

        [Fact]
        public void Roll_BothRollsAreSameNumber_ShouldReturnTrue()
        {
        }
    }
}
