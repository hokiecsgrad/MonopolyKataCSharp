using System;

namespace MonopolyKata.Cards
{
    public class Birthday : Card
    {
        public override string Name { get; } = "Birthday";
        public override string Description { get; } = 
            "It is your birthday. Collect $10 from every player.";

        public override void Execute(Player player)
        {
            throw new NotImplementedException();
        }
    }
}