
using System;

namespace MonopolyKata
{
    public class Dice
    {
        private readonly IRandomGenerator RandomGenerator;
        public int NumSides { get; }
        public bool LastRollWasDoubles { get; private set; }

        private class DefaultGenerator : IRandomGenerator
        {
            private Random Generator = new Random();
            public int Generate(int max)
                => Generator.Next(max) + 1;
        }

        public Dice(IRandomGenerator generator, int numSides)
        {
            RandomGenerator = generator;
            NumSides = numSides;
        }

        public Dice(int numSides) 
            : this(new DefaultGenerator(), numSides) 
        { }

        public (int, int) Roll()
        {
            int roll1 = RandomGenerator.Generate(NumSides);
            int roll2 = RandomGenerator.Generate(NumSides);
            if (roll1 == roll2) LastRollWasDoubles = true;
            if (roll1 != roll2) LastRollWasDoubles = false;
            return (roll1, roll2);
        }
    }
}