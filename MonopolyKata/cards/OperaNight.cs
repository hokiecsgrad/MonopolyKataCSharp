using System;

namespace MonopolyKata.Cards
{
    public class OperaNight : Card
    {
        public override string Name { get; } = "Grand Opera Night";
        public override string Description { get; } = 
            "Collect $50 from every player for opening night seats.";

        public override void Execute(Player player)
        {
            throw new NotImplementedException();
        }
    }
}