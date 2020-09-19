using System.Collections.Generic;

namespace MonopolyKata.Spaces
{
    public abstract class Base : ISpace
    {
        public ISpace Next { get; set; } = null;

        public virtual void Enter(Player player) { }

        public void Exit(Player player) { }

        public virtual void LandsOn(Player player) { }
    }
}