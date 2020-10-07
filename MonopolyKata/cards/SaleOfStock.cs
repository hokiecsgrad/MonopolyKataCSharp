namespace MonopolyKata.Cards
{
    public class SaleOfStock : Card
    {
        public override string Name { get; } = "Sale of Stock";
        public override string Description { get; } = 
            "From sale of stock you get $50.";

        public override void Execute(Player player)
        {
            player.Bank += 50;
        }
    }
}