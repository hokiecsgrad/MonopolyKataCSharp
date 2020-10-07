using System;

namespace MonopolyKata.Cards
{
    public class GeneralRepairs : Card
    {
        public override string Name { get; } = "Make General Repairs";
        public override string Description { get; } = 
            "Make general repairs on all your property: For each house pay $25, for each hotel $100.";

        public override void Execute(Player player)
        {
            throw new NotImplementedException();
        }
    }
}