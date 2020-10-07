namespace MonopolyKata.Cards
{
    public class BankDividend : Card
    {
        public override string Name { get; } = "Bank Dividend";
        public override string Description { get; } = 
            "Bank pays you dividend of $50.";

        public override void Execute(Player player)
        {
            player.Bank += 50;
        }
    }
}