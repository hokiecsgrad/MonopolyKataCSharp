using System;

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
            player.MoveToNearestSpaceInGroup("Utilities");
        }
    }
}