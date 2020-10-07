using System;

namespace MonopolyKata.Spaces
{
    public class GoToJail : Space
    {
        public override string Name { get => "Go To Jail"; }

        public override void LandedOnBy(Player player)
        {
            player.SendToJail();
        }
    }
}