using MonopolyKata;
using MonopolyKata.Spaces;
using Xunit;

namespace MonopolyKataTests
{
    class MockGoBoard : Board
    {
        public MockGoBoard()
        {
            AddSpace(new Go());
            for (int i = 0; i < 39; i++)
                AddSpace(new Empty());
        }
    }

    public class GoBoardTests
    {
        MockGoBoard board;
        Player horse;

        public GoBoardTests()
        {
            board = new MockGoBoard();
            horse = new Player("Horse");
        }

        [Fact]
        public void Construct_BasicBoard_ShouldCreateThatBoard()
        {
            MockGoBoard board = new MockGoBoard();
            Assert.IsType<MockGoBoard>(board);
        }

        [Fact]
        public void Construct_MonopolyBoard_ShouldCreateBoardWith40Spaces()
        {
            Assert.Equal(40, board.NumSpaces);
        }

        [Fact]
        public void Movement_PlayerStartsOnZeroAndRolls7_ShouldBeOn7thSpace()
        {
            board.AddPlayerToBoard(horse, 0);
            Assert.Equal(0, horse.Position);

            board.Move(horse, 7);
            Assert.Equal(7, horse.Position);
        }

        [Fact]
        public void Movement_PlayerStartsOn39AndRolls6_ShouldBeOn5thSpace()
        {
            board.AddPlayerToBoard(horse, 39);
            Assert.Equal(39, horse.Position);

            board.Move(horse, 6);
            Assert.Equal(5, horse.Position);
        }

        [Fact]
        public void Go_PlayerStartsBeforeGoRollsEnoughToPassGo_ShouldReceive200Dollars()
        {
            Assert.Equal(0, horse.Bank);

            board.AddPlayerToBoard(horse, 35);

            board.Move(horse, 11);

            Assert.Equal(200, horse.Bank);
        }

        [Fact]
        public void Go_PlayerPassesGoTwice_ShouldReceive400Dollars()
        {
            Assert.Equal(0, horse.Bank);

            board.AddPlayerToBoard(horse, 39);

            board.Move(horse, 45);

            Assert.Equal(400, horse.Bank);
        }
    }
}
