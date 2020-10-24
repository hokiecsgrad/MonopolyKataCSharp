using System;
using System.Collections.Generic;
using MonopolyKata.Spaces;

namespace MonopolyKata.Cards
{
    public class AdvanceToUtility : Card
    {
        public override string Name { get; } = "Advance To Nearest Utility";
        public override string Description { get; } = 
            "Advance token to nearest Utility. If unowned, you may buy it from " +
            "the Bank. If owned, throw dice and pay owner a total 10 (ten) times " +
            "the amount thrown.";

        public override void Execute(Player player)
        {
            List<Property> properties = BoardReference.GetPropertiesInGroup("Utilities");
            int distance = BoardReference.FindDistanceToNearestProperty(player.Position, properties);
            player.Position += distance;

            if ( player.Position - distance < 0 ) player.Bank += 200;

            Utility util = (Utility)BoardReference.GetSpace(player.Position);

            if (!util.IsOwned)
                util.SellTo(player);
                
            else if (util.Owner != player)
            {
                Dice dice = new Dice();
                int roll1;
                int roll2;
                (roll1, roll2) = dice.Roll();
                int rent = (roll1 + roll2) * 10;
                player.Bank -= rent;
                util.Owner.Bank += rent;
            }
        }
    }
}