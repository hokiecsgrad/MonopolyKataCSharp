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
            if (player.GameRef == null) { return; }

            int total = 0;
            foreach (Player payer in player.GameRef.Players)
            {
                if (payer != player)
                {
                    payer.Bank -= 10;
                    total += 10;
                }
            }

            player.Bank += total;
        }
    }
}