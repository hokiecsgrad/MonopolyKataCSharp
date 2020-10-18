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
            foreach (Player payer in player.GameRef.Players)
            {
                payer.Bank -= 50;
                player.Bank += 50;   
            }
        }
    }
}