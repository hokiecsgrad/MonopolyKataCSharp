
using System;

namespace MonopolyKata
{
    public class SixSidedDie
    {
        Random RandomGenerator { get; set; }
        
        public SixSidedDie(Random generator)
        {
            RandomGenerator = generator;
        }

        public int Roll()
        {
            return RandomGenerator.Next(1,7);
        }
    }

}