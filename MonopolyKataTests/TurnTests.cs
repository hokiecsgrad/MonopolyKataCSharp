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

        private class DoublesGenerator : IRandomGenerator
        {
            Random Generator = new Random();
            int LastRoll = 0;

            public int Generate(int numMax)
            {
                if (LastRoll == 0)
                {
                    LastRoll = Generator.Next(numMax) + 1;
                    return LastRoll;
                }
                else
                {
                    int currentRoll = LastRoll;
                    LastRoll = 0;
                    return currentRoll;
                }
            }
        }

        [Fact]
        public void RollsDoubles_ThreeTimesInSingleTurn_ShouldGoToJail()
        {
            Board board = new MockJailBoard();
            Dice dice = new Dice(new DoublesGenerator(), 6);
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
            Dice dice = new Dice(new DoublesGenerator(), 6);
            Player horse = new Player("Horse");
            horse.Bank = 200;
            Turn turn = new Turn(board, dice);

            board.AddPlayerToBoard(horse, 37);
            turn.Take(horse);

            Assert.Equal(400, horse.Bank);
            Assert.True(horse.IsInJail);
            Assert.Equal(10, horse.Position);
        }

        [Fact(Skip="I don't know how to test this.")]
        public void RollsDoubles_TwoTimesInARow_ShouldDoNothingWithJail()
        {
        }
    }
}