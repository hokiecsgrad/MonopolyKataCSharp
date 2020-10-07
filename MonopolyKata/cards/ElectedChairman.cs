using System;

namespace MonopolyKata.Cards
{
    public class ElectedChairman : Card
    {
        public override string Name { get; } = "Elected Chairman";
        public override string Description { get; } = 
            "You have been elected Chairman of the Board. Pay each player $50.";

        public override void Execute(Player player)
        {
            throw new NotImplementedException();
        }
    }
}