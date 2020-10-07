namespace MonopolyKata.Cards
{
    public class PoorTax : Card
    {
        public override string Name { get; } = "Pay Poor Tax";
        public override string Description { get; } = 
            "Pay poor tax of $15.";

        public override void Execute(Player player)
        {
            player.Bank -= 15;
        }
    }
}