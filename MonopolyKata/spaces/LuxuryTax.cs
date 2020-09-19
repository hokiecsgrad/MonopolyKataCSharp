namespace MonopolyKata.Spaces
{
    public class LuxuryTax : Space
    {
        public override string Name { get => "Income Tax"; }

        public override void LandsOn(Player player)
        {
            player.Bank -= 75;
        }
    }
}