namespace MonopolyKata.Cards
{
    public class BeautyContest : Card
    {
        public override string Name { get; } = "Beauty Contest";
        public override string Description { get; } = 
            "You have won second prize in a beauty contest. Collect $10.";

        public override void Execute(Player player)
        {
            player.Bank += 10;
        }
    }
}