using System;

namespace MonopolyKata.Cards
{
    public class GoBack3Spaces : Card
    {
        public override string Name { get; } = "Go Back Three Spaces";
        public override string Description { get; } = 
            "Go back three spaces.";

        public override void Execute(Player player)
        {
            player.Position -= 3;
            player.BoardRef.Move(player, -3);
        }
    }
}