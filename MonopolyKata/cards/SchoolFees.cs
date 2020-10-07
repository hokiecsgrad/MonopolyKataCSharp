namespace MonopolyKata.Cards
{
    public class SchoolFees : Card
    {
        public override string Name { get; } = "School Fees";
        public override string Description { get; } = 
            "School fees. Pay $50.";

        public override void Execute(Player player)
        {
            player.Bank -= 50;
        }
    }
}