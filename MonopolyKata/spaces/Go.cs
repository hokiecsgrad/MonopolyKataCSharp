namespace MonopolyKata.Spaces
{
    public class Go : Base
    {
        public override void Enter(Player player)
        {
            player.Bank += 200;
        }
    }
}