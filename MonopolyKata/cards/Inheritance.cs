namespace MonopolyKata.Cards
{
    public class Inheritance : Card
    {
        public override string Name { get; } = "Inheritance";
        public override string Description { get; } = 
            "You inherit $100.";

        public override void Execute(Player player)
        {
            player.Bank += 100;
        }
    }
}