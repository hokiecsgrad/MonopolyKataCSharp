using MonopolyKata.Spaces;
using System.Collections.Generic;

namespace MonopolyKata
{
    public class PropertyGroup
    {
        public string Name { get; set; }
        public List<Property> Properties { get; set; }
        public bool CanBuildHouses { get; set; }
        public int CostOfHouse { get; set; }

        public PropertyGroup(string name, bool canBuildHouses = false, int costOfHouse = 0)
        {
            Name = name;
            CanBuildHouses = canBuildHouses;
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
            => Properties[0].NumHouses;

        public void AddHouse()
        {
            foreach (Property prop in Properties)
                prop.NumHouses++;
        }

        public void AddHotel()
        {
            foreach (Property prop in Properties)
            {
                prop.NumHouses = 0;
                prop.NumHotels += 1;
            }
        }
    }
}