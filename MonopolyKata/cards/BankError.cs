namespace MonopolyKata.Cards
{
    public class BankError : Card
    {
        public override string Name { get; } = "Bank Error";
        public override string Description { get; } = 
            "Bank error in your favor.  Collect $200.";

        public override void Execute(Player player)
        {
            player.Bank += 200;
        }
    }
}