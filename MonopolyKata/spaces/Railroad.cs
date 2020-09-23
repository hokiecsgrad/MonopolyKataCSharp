namespace MonopolyKata.Spaces
{
    public class Railroad : Property
    {
        public Railroad(string name, int purchasePrice, int rentPrice, PropertyGroup group)
            : base(name, purchasePrice, rentPrice, group)
        {

        }

        protected override void RentTo(Player player)
        {
            int rent = RentPrice;
            rent = rent * GetMultiplier();

            player.Bank -= rent;
            Owner.Bank += rent;
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