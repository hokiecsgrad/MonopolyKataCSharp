using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace MonopolyKata.Spaces
{
    public abstract class Space
    {
        public abstract string Name { get; }
        public virtual Board BoardReference { get; set; } = null;

        public virtual void Enter(Player player) { }

        public virtual void Exit(Player player) { }

        public virtual void LandedOnBy(Player player) 
        { 
            BoardReference?._logger?.LogInformation("{0} has landed on {1}.", player.Name, Name);
        }
    }
}