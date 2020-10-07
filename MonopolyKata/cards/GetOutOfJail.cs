using System;

namespace MonopolyKata.Cards
{
    public class GetOutOfJail : Card
    {
        public override string Name { get; } = "Get Out of Jail Free";
        public override string Description { get; } = 
            "Get out of jail free.  This card may be kept until needed or sold/traded.";

        public override void Execute(Player player)
        {
            throw new NotImplementedException();
        }
    }
}