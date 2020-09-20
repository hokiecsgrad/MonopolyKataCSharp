namespace MonopolyKata.Spaces
{
    public class ElectricCompany : Property
    {
        public override string Name { get => "Electric Company"; }
        public override int PurchasePrice { get => 150; }
        public override int RentPrice { get => 0; }

        protected override void RentTo(Player player)
        {
            player.Bank -= player.LastRoll * 4;
        }
    }
}