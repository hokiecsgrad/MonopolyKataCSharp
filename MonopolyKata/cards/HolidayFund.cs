namespace MonopolyKata.Cards
{
    public class HolidayFund : Card
    {
        public override string Name { get; } = "Holiday Fund";
        public override string Description { get; } = 
            "Holiday Fund matures. Receive $100.";

        public override void Execute(Player player)
        {
            player.Bank += 100;
        }
    }
}