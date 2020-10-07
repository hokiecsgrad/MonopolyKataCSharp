namespace MonopolyKata.Cards
{
    public class AdvanceToReading : Card
    {
        public override string Name { get; } = "Advance To Reading Railroad";
        public override string Description { get; } = 
            "Take a trip to Reading Railroad. If you pass Go, collect $200.";

        public override void Execute(Player player)
        {
            player.MoveToSpaceNamed("Reading Railroad");
        }
    }
}