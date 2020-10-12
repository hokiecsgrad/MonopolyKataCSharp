using Microsoft.Extensions.Logging;

namespace MonopolyKata.Spaces
{
    public class Property : Space
    {
        public bool IsOwned { get; private set; }
        public override string Name { get; }
        public int PurchasePrice { get; }
        public int RentPrice { get; }
        public PropertyGroup Group { get; }
        protected Player Owner { get; set; }

        public Property(string name, int purchasePrice, int rentPrice, PropertyGroup group)
        {
            IsOwned = false;
            Name = name;
            PurchasePrice = purchasePrice;
            RentPrice = rentPrice;
            Group = group;
            Group.AddProperty(this);
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
            if (player.Bank >= PurchasePrice)
            {
                player.Properties.Add(this);
                player.Bank -= PurchasePrice;
                Owner = player;
                IsOwned = true;

                BoardReference._logger?.LogInformation("{0} has purchased {1} for ${2}.", player.Name, Name, PurchasePrice);
            }
            else 
                BoardReference._logger?.LogInformation("{0} does not have enough money to buy {1}.", player.Name, Name);
        }

        protected virtual void RentTo(Player player)
        {
            int rent = RentPrice;

            if ( OwnerOwnsAllPropertiesInGroup() )
                rent *= 2;

            player.Bank -= rent;
            Owner.Bank += rent;

            BoardReference._logger?.LogInformation("{0} has to pay ${1} in rent to {2}.", player.Name, rent, Owner.Name);
        }

        private bool OwnerOwnsAllPropertiesInGroup()
        {
            int numberOfPropertiesInGroup = Group.GetNumberOfProperties();
            int numberOfPropertiesInGroupOwnedByOwner = Owner.GetNumberOfPropertiesOwnedInGroup(Group);
            return numberOfPropertiesInGroup == numberOfPropertiesInGroupOwnedByOwner;
        }
    }
}