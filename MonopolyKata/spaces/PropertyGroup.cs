using Microsoft.Extensions.Logging;
using MonopolyKata.Spaces;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class PropertyGroup
    {
        public string Name { get; set; }
        public List<Property> Properties { get; set; }
        public bool CanBuildOnProperties { get; set; }
        public int CostOfHouse { get; set; }

        public PropertyGroup(string name, bool canBuildHouses = false, int costOfHouse = 0)
        {
            Name = name;
            CanBuildOnProperties = canBuildHouses;
            CostOfHouse = costOfHouse;
            Properties = new List<Property>();
        }

        public void AddProperty(Property property)
        {
            Properties.Add(property);
        }

        public int GetNumberOfProperties()
            => Properties.Count;

        public int GetNumHousesPerProperty()
            => Properties[0].NumBuildings % 5;

        public int GetNumHotelsPerProperty()
            => Properties[0].NumBuildings / 5;


        public void BuyProperties(Player player)
        {
            if (!CanBuildHouses(player)) return;

            int newHouses = 0;
            int maxHousesPerProp = 10;
            int currNumHouses = GetNumHotelsPerProperty() * 5 + GetNumHousesPerProperty();
            int costOfOneSetOfHouses = GetNumberOfProperties() * CostOfHouse;
            while (currNumHouses < maxHousesPerProp && player.Bank >= costOfOneSetOfHouses)
            {
                AddHouse(player);
                player.Bank -= costOfOneSetOfHouses;
                currNumHouses++;
                newHouses++;
            }

            if (newHouses > 0)
                Properties[0].BoardReference?._logger?.LogInformation("{0} decided to build on the {1} properties.  There are now {2} houses and {3} hotels on each property.", player.Name, Name, GetNumHousesPerProperty(), GetNumHotelsPerProperty());
        }

        private bool CanBuildHouses(Player player)
        {
            if (CanBuildOnProperties
                && HasMonopoly(player)
                && (GetNumHotelsPerProperty() * 5 + GetNumHousesPerProperty() < 10))
                return true;
            else
                return false;
        }

        public bool HasMonopoly(Player player)
        {
            int numPropsInGroup = Properties.Count;
            int numPropsOwnedInGroup = Properties.Count(prop => prop.Owner == player);
            return numPropsInGroup == numPropsOwnedInGroup;
        }

        public void AddHouse(Player player)
        {
            player.BoardRef?._logger?.LogInformation("{0} added a house on each of the {1} properties.", player.Name, Name);
            Properties.ForEach(prop => prop.NumBuildings++);
        }
    }
}