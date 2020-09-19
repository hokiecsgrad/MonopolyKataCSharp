using System;

namespace MonopolyKata.Spaces
{
    public class GoToJail : Space
    {
        public override string Name { get => "Go To Jail"; }

        public override void LandsOn(Player player)
        {
            player.Position = BoardReference.GetBoardPositionOf("Jail");
            if (player.Position == -1)
                throw new InvalidOperationException("Jail is missing from the board!");
        }
    }
}