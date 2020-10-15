using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata.Cards
{
    public class GeneralRepairs : Card
    {
        public override string Name { get; } = "Make General Repairs";
        public override string Description { get; } = 
            "Make general repairs on all your property: For each house pay $25, for each hotel $100.";

        public override void Execute(Player player)
        {
            int numHouses = 0;
            int numHotels = 0;
            List<PropertyGroup> groupsWithMonopoly = 
                player.Properties
                        .Select(p => p.Group)
                        .Distinct()
                        .Where(g => g.HasMonopoly(player))
                        .ToList();
            
            foreach (PropertyGroup group in groupsWithMonopoly)
            {
                numHouses += group.GetNumHousesPerProperty() * group.GetNumberOfProperties();
                numHotels += group.GetNumHotelsPerProperty() * group.GetNumberOfProperties();
            }

            int total = (numHouses * 20) + (numHotels * 100);
            player.Bank -= total;
        }
    }
}