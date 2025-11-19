using Microsoft.Extensions.Logging;
using System;

namespace MonopolyKata
{
    public class Dice : IDice
    {
        private readonly ILogger<Dice>? _logger;
        private readonly IRandomGenerator RandomGenerator;

        public int NumSides { get; }
        public bool LastRollWasDoubles { get; private set; }

        private class DefaultGenerator : IRandomGenerator
        {
            private readonly Random Generator = new();
            public int Generate(int max)
                => Generator.Next(max) + 1;
        }

        public Dice(int numSides, IRandomGenerator generator, ILoggerFactory? loggerFactory = null)
        {
            _logger = loggerFactory?.CreateLogger<Dice>();

            RandomGenerator = generator;
            NumSides = numSides;
            LastRollWasDoubles = false;
        }

        public Dice(ILoggerFactory? loggerFactory = null)
            : this(6, new DefaultGenerator(), loggerFactory)
        { }

        public (int, int) Roll()
        {
            int roll1 = RandomGenerator.Generate(NumSides);
            int roll2 = RandomGenerator.Generate(NumSides);
            
            if (roll1 == roll2) LastRollWasDoubles = true;
            if (roll1 != roll2) LastRollWasDoubles = false;

            return (roll1, roll2);
        }

        private void LogRoll(int roll1, int roll2)
        {
            _logger?.LogInformation($"Rolled {roll1 + roll2}, a {roll1} and a {roll2}.");
            if ( LastRollWasDoubles ) _logger?.LogInformation("Doubles!");
        }
    }
}