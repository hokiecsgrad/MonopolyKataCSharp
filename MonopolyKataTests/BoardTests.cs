using Monopoly;
using System.Collections.Generic;
using Xunit;

namespace MonopolyKataTests
{
    public class BoardTests
    {
        [Fact]
        public void Construct_BasicBoard_ShouldCreateThatBoard()
        {
            Board board = new Board();
        }

        [Fact]
        public void Construct_MonopolyBoard_ShouldCreateBoardWith40Spaces()
        {
            MonopolyBoard board = new MonopolyBoard();
            Assert.Equal(40, board.Spaces.Length);
        }

        [Fact]
        public void Movement_PlayerStartsOnZeroAndRolls7_ShouldBeOn7thSpace()
        {
            MonopolyBoard board = new MonopolyBoard();
            Player horse = new Player("Horse");

            board.Spaces[0].Add(horse);
            Assert.Contains(horse, board.Spaces[0]);

            board.Move(horse, 7);
            Assert.Contains(horse, board.Spaces[7]);
        }

        [Fact]
        public void Movement_PlayerStartsOn39AndRolls6_ShouldBeOn5thSpace()
        {
            MonopolyBoard board = new MonopolyBoard();
            Player horse = new Player("Horse");

            board.Spaces[39].Add(horse);
            Assert.Contains(horse, board.Spaces[39]);

            board.Move(horse, 6);
            Assert.Contains(horse, board.Spaces[5]);
        }
    }
}
