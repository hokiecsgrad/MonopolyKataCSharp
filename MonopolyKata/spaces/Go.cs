namespace MonopolyKata.Spaces
{
    public class Go : Space
    {
        public override string Name { get => "Go"; }

        public override void Enter(Player player)
        {
            player.Bank += 200;
        }
    }
}