namespace MonopolyKata.Cards
{
    public class HospitalFees : Card
    {
        public override string Name { get; } = "Hospital Fees";
        public override string Description { get; } = 
            "Hospital fees. Pay $50.";

        public override void Execute(Player player)
        {
            player.Bank -= 50;
        }
    }
}