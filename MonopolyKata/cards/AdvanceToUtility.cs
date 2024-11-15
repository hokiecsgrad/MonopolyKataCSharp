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
            // These four lines of code here are why Board.Move() exists and putting this
            // logic here feels bad.  However, Board.Move() ends by called the 
            // Space.LandedOnBy() method, which just does too much FOR THIS ONE INSTANCE.
            // So do I ruin some really nice, elegant code for this one divergence, or
            // do I live with shit like this?
            List<Property> properties = player.BoardRef?.GetPropertiesInGroup("Utilities") ?? new List<Property>();
            int distance = player.BoardRef?.FindDistanceToNearestProperty(player.Position, properties) ?? 0;
            player.Position += distance;
            player.Position = player.Position % player.BoardRef?.NumSpaces ?? 0;

            if (player.Position - distance < 0) player.Bank += 200;

            Utility util = (Utility)player.BoardRef?.GetSpace(player.Position);

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