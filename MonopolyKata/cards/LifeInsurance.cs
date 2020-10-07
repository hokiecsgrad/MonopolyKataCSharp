namespace MonopolyKata.Cards
{
    public class LifeInsurance : Card
    {
        public override string Name { get; } = "Life Insurance";
        public override string Description { get; } = 
            "Life insurance matures – Collect $100.";

        public override void Execute(Player player)
        {
            player.Bank += 100;
        }
    }
}