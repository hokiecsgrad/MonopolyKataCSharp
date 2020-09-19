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
                    default:
                        AddSpace(new Empty());
                        break;
                }
            }
        }
    }

    public class IncomeTaxTests
    {
        MockTaxBoard board;
        Player horse;

        public IncomeTaxTests()
        {
            board = new MockTaxBoard();
            horse = new Player("Horse");
        }

        [Fact]
        public void IncomeTax_PlayerWith1800_ShouldLose180()
        {
            horse.Bank = 1800;
            board.AddPlayerToBoard(horse, 0);

            board.Move(horse, 4);

            Assert.Equal(4, horse.Position);
            Assert.Equal(1620, horse.Bank);
        }

        [Fact]
        public void IncomeTax_PlayerWith2200_ShouldLose200()
        {
            horse.Bank = 2200;
            board.AddPlayerToBoard(horse, 0);

            board.Move(horse, 4);

            Assert.Equal(4, horse.Position);
            Assert.Equal(2000, horse.Bank);
        }

        [Fact]
        public void IncomeTax_PlayerWith0_ShouldLose0()
        {
            board.AddPlayerToBoard(horse, 0);

            board.Move(horse, 4);

            Assert.Equal(4, horse.Position);
            Assert.Equal(0, horse.Bank);
        }

        [Fact]
        public void IncomeTax_PlayerWith2000_ShouldLose200()
        {
            horse.Bank = 2000;
            board.AddPlayerToBoard(horse, 0);

            board.Move(horse, 4);

            Assert.Equal(4, horse.Position);
            Assert.Equal(1800, horse.Bank);
        }

        [Fact]
        public void IncomeTax_PlayerWith2000PassesSpace_ShouldLose0()
        {
            horse.Bank = 2000;
            board.AddPlayerToBoard(horse, 0);

            board.Move(horse, 5);

            Assert.Equal(5, horse.Position);
            Assert.Equal(2000, horse.Bank);
        }
    }
}
