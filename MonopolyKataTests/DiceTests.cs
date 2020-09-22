using System;
using MonopolyKata;
using MonopolyKata.Spaces;
using Xunit;

namespace MonopolyKataTests
{
    public class DiceTests
    {
        Dice die;

        public DiceTests()
        {
            die = new Dice(6);
        }

        [Fact]
        public void Roll_Normal_ShouldAlwaysBeBetween2And12()
        {
            int total = 0;
            int min = int.MaxValue;
            int max = int.MinValue;

            for (int i = 0; i < 250; i++)
            {
                var rolls = die.Roll();
                total = rolls.Item1 + rolls.Item2;
                if (total < min) min = total;
                if (total > max) max = total;
            }

            Assert.Equal(2, min);
            Assert.Equal(12, max);
        }

        private class NumberFourGenerator : IRandomGenerator
        {
            public int Generate(int whoCares) => 4;
        }

        [Fact]
        public void LastRollWasDoubles_BothRollsAreSameNumber_ShouldReturnTrue()
        {
            Dice theNumberFour = new Dice(new NumberFourGenerator(), 6);
            
            theNumberFour.Roll();

            Assert.True(theNumberFour.LastRollWasDoubles);
        }
    }
}
