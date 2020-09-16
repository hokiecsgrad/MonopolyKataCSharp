using MonopolyKata;
using System.Collections.Generic;
using Xunit;

namespace MonopolyKataTests
{
    public class BoardTests
    {
        MonopolyBoard board;
        Player horse;

        public BoardTests()
        {
            board = new MonopolyBoard();
            horse = new Player("Horse");
        }

        [Fact]
        public void Construct_BasicBoard_ShouldCreateThatBoard()
        {
            Board board = new Board();
            Assert.IsType<Board>(board);
        }

        [Fact]
        public void Construct_MonopolyBoard_ShouldCreateBoardWith40Spaces()
        {
            Assert.Equal(40, board.Spaces.Length);
        }

        [Fact]
        public void Movement_PlayerStartsOnZeroAndRolls7_ShouldBeOn7thSpace()
        {
            board.Spaces[0].Add(horse);
            Assert.Contains(horse, board.Spaces[0]);

            board.Move(horse, 7);
            Assert.Contains(horse, board.Spaces[7]);
        }

        [Fact]
        public void Movement_PlayerStartsOn39AndRolls6_ShouldBeOn5thSpace()
        {
            board.Spaces[39].Add(horse);
            Assert.Contains(horse, board.Spaces[39]);

            board.Move(horse, 6);
            Assert.Contains(horse, board.Spaces[5]);
        }
    }
}
