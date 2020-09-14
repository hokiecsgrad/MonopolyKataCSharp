using MonopolyKata;
using System;
using Xunit;

namespace MonopolyKataTests
{
    public class PlayerTests
    {
        [Fact]
        public void Construct_NormalPlayer_ShouldCreateThatPlayer()
        {
            Player horse = new Player("Horse");
        }

        
    }
}
