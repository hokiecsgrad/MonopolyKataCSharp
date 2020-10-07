namespace MonopolyKata.Cards
{
    public class ConsultancyFees : Card
    {
        public override string Name { get; } = "Consultancy Fees";
        public override string Description { get; } = 
            "Receive $25 consultancy fee.";

        public override void Execute(Player player)
        {
            player.Bank += 25;
        }
    }
}