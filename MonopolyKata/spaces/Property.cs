namespace MonopolyKata.Spaces
{
    public class Property : Space
    {
        public bool IsOwned { get; set; }
        public override string Name { get; }
        public int PurchasePrice { get; }
        public int RentPrice { get; }
        protected Player Owner { get; set; }

        public Property(string name, int purchasePrice, int rentPrice)
        {
            Name = name;
            PurchasePrice = purchasePrice;
            RentPrice = rentPrice;
        }

        public override void LandedOnBy(Player player)
        {
            if ( IsOwned && player.Properties.Contains(this) )
                return;

            if ( IsOwned )
                RentTo(player);
            else 
                SellTo(player);
        }

        protected virtual void SellTo(Player player)
        {
            player.Properties.Add(this);
            player.Bank -= PurchasePrice;
            Owner = player;
            IsOwned = true;
        }

        protected virtual void RentTo(Player player)
        {
            player.Bank -= RentPrice;
            Owner.Bank += RentPrice;
        }
    }
}