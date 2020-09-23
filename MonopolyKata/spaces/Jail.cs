namespace MonopolyKata.Spaces
{
    public class Jail : Space
    {
        public override string Name { get => "Jail"; }

        public override void Exit(Player player)
        {
            if ( player.CanExitJail() )
                player.IsInJail = false;
        }
    }
}