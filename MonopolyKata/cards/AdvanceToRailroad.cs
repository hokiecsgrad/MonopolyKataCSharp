using System;

namespace MonopolyKata.Cards
{
    public class AdvanceToRailroad : Card
    {
        public override string Name { get; } = "Advance To Nearest Railroad";
        public override string Description { get; } = 
            "Advance token to the nearest Railroad and pay owner twice the " +
            "rental to which he/she {he} is otherwise entitled. If Railroad " +
            "is unowned, you may buy it from the Bank.";

        public override void Execute(Player player)
        {
            throw new NotImplementedException();
            //player.MoveToNearestSpaceInGroup("Railroads");
        }
    }
}