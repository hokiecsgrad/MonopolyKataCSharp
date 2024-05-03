using Microsoft.Extensions.Logging;

namespace MonopolyKata.Spaces
{
    public class LuxuryTax : Space
    {
        public override string Name { get => "Luxury Tax"; }

        public override void LandedOnBy(Player player)
        {
            base.LandedOnBy(player);

            int luxuryTax = 75;

            BoardReference?._logger?.LogInformation("{0} has to pay a ${1} luxury tax.", player.Name, luxuryTax);

            player.Bank -= luxuryTax;
        }
    }
}