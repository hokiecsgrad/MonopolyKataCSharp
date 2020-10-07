using System;

namespace MonopolyKata.Cards
{
    public class StreetRepairs : Card
    {
        public override string Name { get; } = "Street Repairs";
        public override string Description { get; } = 
            "You are assessed for street repairs: Pay $40 per house and $115 per hotel you own.";

        public override void Execute(Player player)
        {
            throw new NotImplementedException();
        }
    }
}