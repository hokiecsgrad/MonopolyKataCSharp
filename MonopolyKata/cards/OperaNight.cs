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
            if (player.GameRef == null) { return; }

            int total = 0;
            foreach (Player payer in player.GameRef.Players)
            {
                if (payer != player)
                {
                    payer.Bank -= 50;
                    total += 50;
                }
            }

            player.Bank += total;
        }
    }
}