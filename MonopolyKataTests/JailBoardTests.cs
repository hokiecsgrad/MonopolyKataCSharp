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

        [Fact]
        public void LeaveJail_PlayerPays50Dollars_ShouldTakeNormalTurn()
        {
            board.AddPlayerToBoard(horse, 10);
            horse.Bank = 200;
            horse.IsInJail = true;

            board.Spaces[horse.Position].Exit(horse);

            Assert.Equal(150, horse.Bank);
            Assert.False(horse.IsInJail);
        }

        [Fact]
        public void LeaveJail_PlayerRollsDoubles_ShouldLeaveJailAndMoveOnce()
        {
            board.AddPlayerToBoard(horse, 10);
            horse.Bank = 10;
            horse.IsInJail = true;
            horse.LastRoll = (5, 5);

            board.Spaces[horse.Position].Exit(horse);
            board.Move(horse, 10);

            Assert.Equal(10, horse.Bank);
            Assert.Equal(20, horse.Position);
            Assert.False(horse.IsInJail);
        }
    }
}
