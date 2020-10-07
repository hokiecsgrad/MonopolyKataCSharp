namespace MonopolyKata.Cards
{
    public class BuildingAndLoan : Card
    {
        public override string Name { get; } = "Building And Loan";
        public override string Description { get; } = 
            "Your building loan matures. Receive $150.";

        public override void Execute(Player player)
        {
            player.Bank += 150;
        }
    }
}