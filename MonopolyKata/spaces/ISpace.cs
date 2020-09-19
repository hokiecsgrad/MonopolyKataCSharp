using System.Collections.Generic;

namespace MonopolyKata.Spaces
{
    public interface ISpace
    {
        public ISpace Next { get; set; }
        
        public void Enter(Player player);
        public void Exit(Player player);
        public void LandsOn(Player player);
    }
}