using Microsoft.Extensions.Logging;

namespace MonopolyKata.Spaces
{
    public class GoToJail : Space
    {
        public override string Name { get => "Go To Jail"; }

        public override void LandedOnBy(Player player)
        {
            base.LandedOnBy(player);

            BoardReference?._logger?.LogInformation("{0} is sent to Jail!", player.Name);

            player.SendToJail();
        }
    }
}