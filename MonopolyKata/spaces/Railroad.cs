using Microsoft.Extensions.Logging;

namespace MonopolyKata.Spaces
{
    public class Railroad : Property
    {
        public Railroad(string name, int purchasePrice, int rentPrice, PropertyGroup group)
            : base(name, purchasePrice, new int[] {rentPrice}, group)
        {

        }

        protected override void RentTo(Player player)
        {
            int rent = CalculateRent();

            player.Bank -= rent;
            Owner.Bank += rent;

            BoardReference?._logger?.LogInformation("{0} has to pay ${1} in rent to {2}.", player.Name, rent, Owner.Name);
        }

        public int CalculateRent()
        {
            int rent = RentPrices[0];
            return rent * GetMultiplier();
        }

        private int GetMultiplier()
        {
            return (int)System.Math.Pow(2, GetNumberOfRailroadsOwnedByOwner() - 1);
        }

        private int GetNumberOfRailroadsOwnedByOwner()
        {
            return Owner.GetNumberOfPropertiesOwnedInGroup(Group);
        }
    }
}