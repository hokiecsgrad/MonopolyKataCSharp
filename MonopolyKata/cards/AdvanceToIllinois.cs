namespace MonopolyKata.Cards
{
    public class AdvanceToIllinois : Card
    {
        public override string Name { get; } = "Advance To Illinois Ave";
        public override string Description { get; } = 
            "Advance to Illinois Ave.  If you pass Go, collect $200.";

        public override void Execute(Player player)
        {
            BoardReference.MoveToSpaceNamed(player, "Illinois Avenue");
        }
    }
}