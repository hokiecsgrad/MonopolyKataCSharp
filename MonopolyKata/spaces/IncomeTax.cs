using System;

namespace MonopolyKata.Spaces
{
    public class IncomeTax : Space
    {
        public override string Name { get => "Income Tax"; }

        public override void LandsOn(Player player)
        {
            player.Bank -= Math.Min( (int)(player.Bank * 0.10), 200);
        }
    }
}