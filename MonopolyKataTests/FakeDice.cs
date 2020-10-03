using MonopolyKata;
using System;

namespace MonopolyKataTests
{
    public class DoublesGenerator : IRandomGenerator
    {
        Random Generator = new Random();
        int LastRoll = 0;

        public int Generate(int numMax)
        {
            if (LastRoll == 0)
            {
                LastRoll = Generator.Next(numMax) + 1;
                return LastRoll;
            }
            else
            {
                int currentRoll = LastRoll;
                LastRoll = 0;
                return currentRoll;
            }
        }
    }

    public class NonDoublesGenerator : IRandomGenerator
    {
        private Random random = new Random();
        private int lastNum = 0;
        private int currNum = 0;
        public int Generate(int max)
        {
            while (currNum == lastNum)
                currNum = random.Next(max) + 1;
            lastNum = currNum;
            return currNum;
        }
    }

    public class AlwaysGenerateThree : IRandomGenerator
    {
        private int lastNum = 2;
        private int currNum = 0;
        public int Generate(int max)
        {
            if ( lastNum == 2)
                currNum = 1;
            else if ( lastNum == 1 )
                currNum = 2;

            lastNum = currNum;
            return currNum;
        }
    }

    public class AlwaysGenerateDubTwo : IRandomGenerator
    {
        public int Generate(int max) => 2;
    }

}