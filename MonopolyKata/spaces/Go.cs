using Microsoft.Extensions.Logging;

namespace MonopolyKata.Spaces
{
    public class Go : Space
    {
        public override string Name { get => "Go"; }

        public override void Enter(Player player)
        {
            BoardReference?._logger?.LogInformation("{0} passed Go, collected $200!", player.Name);

            player.Bank += 200;
        }
    }
}