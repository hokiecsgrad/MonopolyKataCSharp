using MonopolyKata;
using MonopolyKata.Spaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace MonopolyKataTests
{
    public class TurnTests
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

        [Fact]
        public void RollsDoubles_ThreeTimesInSingleTurn_ShouldGoToJail()
        {
            Board board = new MockJailBoard();
            Dice dice = new Dice(6, new DoublesGenerator());
            Player horse = new Player("Horse");
            horse.Bank = 200;
            Turn turn = new Turn(board, dice);

            board.AddPlayerToBoard(horse, 0);
            turn.Take(horse);

            Assert.Equal(200, horse.Bank);
            Assert.True(horse.IsInJail);
            Assert.Equal(10, horse.Position);
        }

        [Fact]
        public void RollsDoubles_ThreeTimesButPassesGoBeforeThirdTurn_ShouldCollect200Dollars()
        {
            Board board = new MockJailBoard();
            Dice dice = new Dice(6, new DoublesGenerator());
            Player horse = new Player("Horse");
            horse.Bank = 200;
            Turn turn = new Turn(board, dice);

            board.AddPlayerToBoard(horse, 37);
            turn.Take(horse);

            Assert.Equal(400, horse.Bank);
            Assert.True(horse.IsInJail);
            Assert.Equal(10, horse.Position);
        }
    }
}