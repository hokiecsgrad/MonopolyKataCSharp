using System;
using System.Collections.Generic;
using MonopolyKata.Spaces;

namespace MonopolyKata.Cards
{
    public class AdvanceToRailroad : Card
    {
        public override string Name { get; } = "Advance To Nearest Railroad";
        public override string Description { get; } =
            "Advance token to the nearest Railroad and pay owner twice the " +
            "rental to which he/she is otherwise entitled. If Railroad " +
            "is unowned, you may buy it from the Bank.";

        public override void Execute(Player player)
        {
            // These four lines of code here are why Board.Move() exists and putting this
            // logic here feels bad.  However, Board.Move() ends by called the 
            // Space.LandedOnBy() method, which just does too much FOR THIS ONE INSTANCE.
            // So do I ruin some really nice, elegant code for this one divergence, or
            // do I live with shit like this?
            List<Property> properties = player.BoardRef?.GetPropertiesInGroup("Railroads");
            int distance = player.BoardRef?.FindDistanceToNearestProperty(player.Position, properties) ?? 0;
            player.Position += distance;
            player.Position = player.Position % player.BoardRef?.NumSpaces ?? 0;

            if (player.Position - distance < 0) player.Bank += 200;

            Railroad railroad = (Railroad)player.BoardRef?.GetSpace(player.Position);

            if (!railroad.IsOwned)
                railroad.SellTo(player);

            else if (railroad.Owner != player)
            {
                int rent = railroad.CalculateRent();
                player.Bank -= (rent * 2);
                railroad.Owner.Bank += (rent * 2);
            }
        }
    }
}