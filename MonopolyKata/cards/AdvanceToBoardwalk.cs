namespace MonopolyKata.Cards
{
    public class AdvanceToBoardwalk : Card
    {
        public override string Name { get; } = "Advance To Boardwalk";
        public override string Description { get; } = 
            "Take a walk on the Boardwalk. Advance token to Boardwalk.";

        public override void Execute(Player player)
        {
            BoardReference.MoveToSpaceNamed(player, "Boardwalk");
        }
    }
}