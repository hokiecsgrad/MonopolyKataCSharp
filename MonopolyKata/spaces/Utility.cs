namespace MonopolyKata.Spaces
{
    public class Utility : Property
    {
        public Utility(string name, int purchasePrice, int rentPrice, PropertyGroup group)
            : base(name, purchasePrice, rentPrice, group)
        {

        }

        protected override void RentTo(Player player)
        {
            int numberOfUtilitiesOwned = GetNumberOfUtilitiesOwnedByOwner();
            int rentModifier = 0;
            
            if ( numberOfUtilitiesOwned == 1 )
                rentModifier = 4;
            else if ( numberOfUtilitiesOwned == 2 )
                rentModifier = 10;

            int rent = player.LastRoll * rentModifier;

            player.Bank -= rent;
            Owner.Bank += rent;
        }

        private int GetNumberOfUtilitiesOwnedByOwner()
        {
            return Owner.GetNumberOfPropertiesOwnedInGroup(Group);
        }
    }
}