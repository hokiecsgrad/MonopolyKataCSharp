using MonopolyKata;
using MonopolyKata.Spaces;
using System.Collections.Generic;
using Xunit;

namespace MonopolyKataTests
{
    class MockJailBoard : Board
    {
        public MockJailBoard()
        {
            Spaces = new List<ISpace>();

            Spaces.Add(new Go());
            for (int i = 0; i < 39; i++)
                Spaces.Add(new Empty());
        }
    }

    public class JailBoardTests
    {
        MockJailBoard board;
        Player horse;

        public JailBoardTests()
        {
            board = new MockJailBoard();
            horse = new Player("Horse");
        }

        [Fact]
        public void Jail_PlayerLandsOnGoToJail_ShouldMoveToJailSpaceButBeVisiting()
        {
            board.AddPlayerToBoard(horse, 29);
            
            board.Move(horse, 1);

            Assert.Equal(10, horse.Position);
        }
    }
}
