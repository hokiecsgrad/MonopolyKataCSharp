using Microsoft.Extensions.Logging;
using System;

namespace MonopolyKata.Spaces
{
    public class IncomeTax : Space
    {
        public override string Name { get => "Income Tax"; }

        public override void LandedOnBy(Player player)
        {
            int incomeTaxPayment = player.Bank > 0 ? Math.Min( (int)Math.Floor(player.Bank * 0.10), 200) : 0;
            player.Bank -= incomeTaxPayment;

            BoardReference?._logger?.LogInformation("{0} has to pay ${1} in income taxes.", player.Name, incomeTaxPayment);
        }
    }
}