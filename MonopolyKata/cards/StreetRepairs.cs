using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata.Cards
{
    public class StreetRepairs : Card
    {
        public override string Name { get; } = "Street Repairs";
        public override string Description { get; } = 
            "You are assessed for street repairs: Pay $40 per house and $115 per hotel you own.";

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

            int total = (numHouses * 40) + (numHotels * 115);
            player.Bank -= total;
        }
    }
}