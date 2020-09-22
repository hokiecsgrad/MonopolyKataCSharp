using MonopolyKata;
using MonopolyKata.Spaces;
using Xunit;

namespace MonopolyKataTests
{
    class MockJailBoard : Board
    {
        public MockJailBoard()
        {
            AddSpace(new Go());
            for (int i = 1; i < 40; i++)
            {
                switch (i)
                {
                    case 10:
                        AddSpace(new Jail());
                        break;
                    case 30:
                        AddSpace(new GoToJail());
                        break;
                    default:
                        AddSpace(new Empty());
                        break;
                }
            }
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
        public void GoToJail_PlayerLandsOnByRollingNonDoubles_ShouldMoveToJail()
        {
            board.AddPlayerToBoard(horse, 27);

            board.Move(horse, 3);

            Assert.Equal(10, horse.Position);
            Assert.True(horse.IsInJail);
            Assert.Equal(0, horse.Bank);
        }

        [Fact]
        public void GoToJail_PlayerLandsOnByRollingDoubles_ShouldMoveToJail()
        {
            board.AddPlayerToBoard(horse, 28);

            board.Move(horse, 2);

            Assert.Equal(10, horse.Position);
            Assert.True(horse.IsInJail);
            Assert.Equal(0, horse.Bank);
        }

        [Fact]
        public void GoToJail_PlayerPassesGoToJail_ShouldLandWhereDiceIndicate()
        {
            board.AddPlayerToBoard(horse, 29);

            board.Move(horse, 2);

            Assert.Equal(31, horse.Position);
            Assert.Equal(0, horse.Bank);
        }
    }
}
