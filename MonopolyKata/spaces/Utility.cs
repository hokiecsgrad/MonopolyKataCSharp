using Microsoft.Extensions.Logging;

namespace MonopolyKata.Spaces
{
    public class Utility : Property
    {
        public Utility(string name, int purchasePrice, int rentPrice, PropertyGroup group)
            : base(name, purchasePrice, new int[] { rentPrice }, group)
        {

        }

        protected override void RentTo(Player player)
        {
            if (Owner is null) return;

            int numberOfUtilitiesOwned = GetNumberOfUtilitiesOwnedByOwner();
            int rentModifier = 0;

            if (numberOfUtilitiesOwned == 1)
                rentModifier = 4;
            else if (numberOfUtilitiesOwned == 2)
                rentModifier = 10;

            int rent = (player.LastRoll.Item1 + player.LastRoll.Item2) * rentModifier;

            BoardReference?._logger?.LogInformation("{0} has to pay ${1} in rent to {2}.", player.Name, rent, Owner.Name);

            player.Bank -= rent;
            Owner.Bank += rent;
        }

        private int GetNumberOfUtilitiesOwnedByOwner()
        {
            return Owner?.GetNumberOfPropertiesOwnedInGroup(Group) ?? 0;
        }
    }
}