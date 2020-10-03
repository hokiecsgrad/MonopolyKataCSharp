using System;
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
        Dice always3Dice;
        Dice alwaysDubTwoDice;
        Dice neverDubDice;

        public JailBoardTests()
        {
            board = new MockJailBoard();
            horse = new Player("Horse");

            always3Dice = new Dice(new AlwaysGenerateThree(), 6);
            alwaysDubTwoDice = new Dice(new AlwaysGenerateDubTwo(), 6);
            neverDubDice = new Dice(new NonDoublesGenerator(), 6);
        }

        [Fact]
        public void GoToJail_PlayerLandsOnByRollingNonDoubles_ShouldMoveToJail()
        {
            board.AddPlayerToBoard(horse, 27);
            horse.Bank = 333;
            horse.IsInJail = false;
            Turn turn = new Turn(board, always3Dice);

            turn.Take(horse);

            Assert.Equal(10, horse.Position);
            Assert.True(horse.IsInJail);
            Assert.Equal(333, horse.Bank);
        }

        [Fact]
        public void GoToJail_PlayerLandsOnGoToJailByRollingDoubles_ShouldMoveToJail()
        {
            board.AddPlayerToBoard(horse, 26);
            horse.Bank = 222;
            horse.IsInJail = false;
            Turn turn = new Turn(board, alwaysDubTwoDice);

            turn.Take(horse);

            Assert.Equal(10, horse.Position);
            Assert.True(horse.IsInJail);
            Assert.Equal(222, horse.Bank);
        }

        [Fact]
        public void GoToJail_PlayerPassesGoToJail_ShouldLandWhereDiceIndicate()
        {
            board.AddPlayerToBoard(horse, 29);
            horse.Bank = 111;
            horse.IsInJail = false;
            Turn turn = new Turn(board, always3Dice);

            turn.Take(horse);

            Assert.Equal(32, horse.Position);
            Assert.Equal(111, horse.Bank);
        }

        [Fact]
        public void LeaveJail_PlayerPays50Dollars_ShouldTakeNormalTurn()
        {
            board.AddPlayerToBoard(horse, 10);
            horse.Bank = 200;
            horse.IsInJail = true;
            horse.WantsToPayToGetOutOfJail = true;
            Turn turn = new Turn(board, neverDubDice);

            turn.Take(horse);

            Assert.Equal(150, horse.Bank);
            Assert.False(horse.IsInJail);
        }

        [Fact]
        public void LeaveJail_PlayerRollsDoubles_ShouldLeaveJailAndMoveOnce()
        {
            board.AddPlayerToBoard(horse, 10);
            horse.Bank = 10;
            horse.IsInJail = true;
            Turn turn = new Turn(board, alwaysDubTwoDice);

            turn.Take(horse);

            Assert.Equal(10, horse.Bank);
            Assert.Equal(14, horse.Position);
            Assert.False(horse.IsInJail);
        }

        [Fact]
        public void LeaveJail_PlayerRollsNonDoublesFirstTurn_ShouldStayInJail()
        {
            board.AddPlayerToBoard(horse, 10);
            horse.Bank = 10;
            horse.NumTurnsInJail = 0;
            horse.IsInJail = true;
            Turn turn = new Turn(board, neverDubDice);

            turn.Take(horse);

            Assert.Equal(10, horse.Position);
            Assert.Equal(10, horse.Bank);
            Assert.Equal(1, horse.NumTurnsInJail);
        }

        [Fact]
        public void LeaveJail_PlayerRollsNonDoublesTwoTurns_ShouldStayInJail()
        {
            board.AddPlayerToBoard(horse, 10);
            horse.Bank = 10;
            horse.NumTurnsInJail = 0;
            horse.IsInJail = true;
            Turn turn = new Turn(board, neverDubDice);

            turn.Take(horse);
            turn.Take(horse);

            Assert.Equal(10, horse.Position);
            Assert.Equal(10, horse.Bank);
            Assert.Equal(2, horse.NumTurnsInJail);
        }

        [Fact]
        public void LeaveJail_PlayerRollsDoublesOnThirdTry_ShouldMoveAndNotRollAgain()
        {
            board.AddPlayerToBoard(horse, 10);
            horse.Bank = 10;
            horse.NumTurnsInJail = 0;
            horse.IsInJail = true;
            Turn turn = new Turn(board, neverDubDice);

            turn.Take(horse);
            turn.Take(horse);
            turn.SetDice(alwaysDubTwoDice);
            turn.Take(horse);

            Assert.Equal(14, horse.Position);
            Assert.Equal(10, horse.Bank);
            Assert.Equal(0, horse.NumTurnsInJail);
        }

        [Fact]
        public void LeaveJail_PlayerRollsNonDoublesThreeTimes_ShouldMoveCharge50DollarsAndMoveThirdRoll()
        {
            board.AddPlayerToBoard(horse, 10);
            horse.Bank = 10;
            horse.IsInJail = true;
            Turn turn = new Turn(board, neverDubDice);

            turn.Take(horse);
            turn.Take(horse);
            turn.Take(horse);

            Assert.Equal(10 + (horse.LastRoll.Item1 + horse.LastRoll.Item2), horse.Position);
            Assert.Equal(-40, horse.Bank);
            Assert.Equal(0, horse.NumTurnsInJail);
        }
    }
}
