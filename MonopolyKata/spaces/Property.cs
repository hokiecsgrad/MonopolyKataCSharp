using Microsoft.Extensions.Logging;

namespace MonopolyKata.Spaces
{
    public class Property : Space
    {
        public override string Name { get; }
        public bool IsOwned { get; private set; }
        public int NumBuildings { get; set; }
        public int PurchasePrice { get; }
        public int[] RentPrices { get; }
        public PropertyGroup Group { get; }
        public Player? Owner { get; private set; } = null;

        public Property(string name, int purchasePrice, int[] rentPrice, PropertyGroup group)
        {
            Name = name;
            IsOwned = false;
            NumBuildings = 0;
            PurchasePrice = purchasePrice;
            RentPrices = rentPrice;
            Group = group;
            Group.AddProperty(this);
        }

        public void Reset()
        {
            Owner = null;
            IsOwned = false;
            NumBuildings = 0;
        }

        public override void LandedOnBy(Player player)
        {
            base.LandedOnBy(player);

            if (IsOwned && Owner == player)
                return;

            if (IsOwned)
                RentTo(player);
            else
                SellTo(player);
        }

        public virtual void SellTo(Player player)
        {
            if (player.Bank >= PurchasePrice)
            {
                BoardReference?._logger?.LogInformation("{0} has purchased {1} for ${2}.", player.Name, Name, PurchasePrice);

                player.Properties.Add(this);
                player.Bank -= PurchasePrice;
                Owner = player;
                IsOwned = true;

                if (player.HasMonopoly(Group))
                    BoardReference?._logger?.LogInformation("{0} has a MONOPOLY on {1} properties!", player.Name, Group.Name);
            }
            else
                BoardReference?._logger?.LogInformation("{0} does not have enough money to buy {1}.", player.Name, Name);
        }

        protected virtual void RentTo(Player player)
        {
            if (Owner is null) return;

            int rent = CalculateRent();

            BoardReference?._logger?.LogInformation("{0} has to pay ${1} in rent to {2}.", player.Name, rent, Owner.Name);

            player.Bank -= rent;
            Owner.Bank += rent;
        }

        private int CalculateRent()
        {
            int rent = 0;

            if (HasHouses())
                rent = GetHouseAndHotelRent();
            else
                rent = GetNormalRent();

            return rent;
        }

        private bool HasHouses()
        {
            return (NumBuildings > 0);
        }

        private int GetNormalRent()
        {
            int rent = RentPrices[0];
            if (OwnerOwnsAllPropertiesInGroup())
                rent *= 2;
            return rent;
        }

        private int GetHouseAndHotelRent()
        {
            int houseRent = Group.GetNumHousesPerProperty() > 0 ?
                                    RentPrices[Group.GetNumHousesPerProperty()]
                                    :
                                    0;
            int hotelRent = Group.GetNumHotelsPerProperty() * RentPrices[5];
            return houseRent + hotelRent;
        }

        private bool OwnerOwnsAllPropertiesInGroup()
        {
            int numberOfPropertiesInGroup = Group.GetNumberOfProperties();
            int numberOfPropertiesInGroupOwnedByOwner = Owner?.GetNumberOfPropertiesOwnedInGroup(Group) ?? 0;
            return numberOfPropertiesInGroup == numberOfPropertiesInGroupOwnedByOwner;
        }
    }
}