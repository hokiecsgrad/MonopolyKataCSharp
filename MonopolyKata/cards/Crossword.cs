namespace MonopolyKata.Cards
{
    public class Crossword : Card
    {
        public override string Name { get; } = "Crossword Competition";
        public override string Description { get; } = 
            "You have won a crossword competition. Collect $100.";

        public override void Execute(Player player)
        {
            player.Bank += 100;
        }
    }
}