using System.Collections.Generic;

namespace MonopolyKata.Spaces
{
    public abstract class Space
    {
        public abstract string Name { get; }
        public Board BoardReference {get; set;} = null;

        public virtual void Enter(Player player) { }

        public void Exit(Player player) { }

        public virtual void LandedOnBy(Player player) { }
    }
}