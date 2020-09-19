using MonopolyKata;
using MonopolyKata.Spaces;
using Xunit;

namespace MonopolyKataTests
{
    class MockTaxBoard : Board
    {
        public MockTaxBoard()
        {
            AddSpace(new Go());
            for (int i = 1; i < 40; i++)
            {
                switch (i)
                {
                    case 4:
                        AddSpace(new IncomeTax());
                        break;
                    case 38:
                        AddSpace(new LuxuryTax());
                        break;
                    default:
                        AddSpace(new Empty());
                        break;
                }
            }
        }
    }

    public class TaxTests
    {
        MockTaxBoard board;
        Player horse;

        public TaxTests()
        {
            board = new MockTaxBoard();
            horse = new Player("Horse");
        }

        [Fact]
        public void IncomeTax_PlayerWith1800_ShouldLose180Dollars()
        {
            horse.Bank = 1800;
            board.AddPlayerToBoard(horse, 0);

            board.Move(horse, 4);

            Assert.Equal(4, horse.Position);
            Assert.Equal(1620, horse.Bank);
        }

        [Fact]
        public void IncomeTax_PlayerWith2200_ShouldLose200Dollars()
        {
            horse.Bank = 2200;
            board.AddPlayerToBoard(horse, 0);

            board.Move(horse, 4);

            Assert.Equal(4, horse.Position);
            Assert.Equal(2000, horse.Bank);
        }

        [Fact]
        public void IncomeTax_PlayerWith0_ShouldLose0Dollars()
        {
            board.AddPlayerToBoard(horse, 0);

            board.Move(horse, 4);

            Assert.Equal(4, horse.Position);
            Assert.Equal(0, horse.Bank);
        }

        [Fact]
        public void IncomeTax_PlayerWith2000_ShouldLose200Dollars()
        {
            horse.Bank = 2000;
            board.AddPlayerToBoard(horse, 0);

            board.Move(horse, 4);

            Assert.Equal(4, horse.Position);
            Assert.Equal(1800, horse.Bank);
        }

        [Fact]
        public void IncomeTax_PlayerWith2000PassesSpace_ShouldLose0Dollars()
        {
            horse.Bank = 2000;
            board.AddPlayerToBoard(horse, 0);

            board.Move(horse, 5);

            Assert.Equal(5, horse.Position);
            Assert.Equal(2000, horse.Bank);
        }

        [Fact]
        public void LuxuryTax_PlayerLandsOn_ShouldLose75Dollars()
        {
            horse.Bank = 2000;
            board.AddPlayerToBoard(horse, 35);

            board.Move(horse, 3);

            Assert.Equal(1925, horse.Bank);
        }

        [Fact]
        public void LuxuryTax_PlayerPassesOver_ShouldLose0Dollars()
        {
            horse.Bank = 2000;
            board.AddPlayerToBoard(horse, 35);

            board.Move(horse, 4);

            Assert.Equal(2000, horse.Bank);
        }
    }
}
