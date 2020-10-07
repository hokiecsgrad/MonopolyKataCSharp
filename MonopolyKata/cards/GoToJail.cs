namespace MonopolyKata.Cards
{
    public class GoToJail : Card
    {
        public override string Name { get; } = "Go To Jail";
        public override string Description { get; } = 
            "Go directly to jail. Do not pass Go, Do not collect $200.";

        public override void Execute(Player player)
        {
            player.SendToJail();
        }
    }
}