namespace MonopolyKata.Cards
{
    public class IncomeTaxRefund : Card
    {
        public override string Name { get; } = "Income Tax Refund";
        public override string Description { get; } = 
            "Income tax refund. Collect $20.";

        public override void Execute(Player player)
        {
            player.Bank += 20;
        }
    }
}