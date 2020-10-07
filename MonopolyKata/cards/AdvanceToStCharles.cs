namespace MonopolyKata.Cards
{
    public class AdvanceToStCharles : Card
    {
        public override string Name { get; } = "Advance To St. Charles Place";
        public override string Description { get; } = 
            "Advance to St. Charles Place. If you pass Go, collect $200.";

        public override void Execute(Player player)
        {
            player.MoveToSpaceNamed("St. Charles Place");
        }
    }
}