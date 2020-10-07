namespace MonopolyKata.Cards
{
    public class DoctorsFees : Card
    {
        public override string Name { get; } = "Doctors Fees";
        public override string Description { get; } = 
            "Doctor's Fees.  Pay $50.";

        public override void Execute(Player player)
        {
            player.Bank -= 50;
        }
    }
}