namespace MonopolyKata.Cards
{
    public class AdvanceToGo : Card
    {
        public override string Name { get; } = "Advance To Go";
        public override string Description { get; } = 
            "Advance to Go.  Collect $200.";

        public override void Execute(Player player)
        {
            player.MoveToSpaceNamed("Go");
        }
    }
}