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
            foreach (Player payer in player.GameRef.Players)
            {
                payer.Bank -= 10;
                player.Bank += 10;   
            }
        }
    }
}