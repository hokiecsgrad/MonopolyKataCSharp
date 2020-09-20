using MonopolyKata;
using MonopolyKata.Spaces;
using Xunit;

namespace MonopolyKataTests
{
    public class SpaceTests
    {
        [Fact]
        public void Construct_BasicSpace_ShouldCreateASpace()
        {
            Empty space = new Empty();
            Assert.IsType<Empty>(space);
        }

        [Fact]
        public void Space_PlayerLandsOn_NothingHappens()
        {
            Player horse = new Player("Horse");
            Empty empty = new Empty();

            empty.Enter(horse);
            empty.LandedOnBy(horse);

            Assert.Equal(0, horse.Bank);
        }

        [Fact]
        public void Go_PlayerLandsOn_ShouldReceive200Dollars()
        {
            Player horse = new Player("Horse");
            Go go = new Go();

            go.Enter(horse);
            go.LandedOnBy(horse);

            Assert.Equal(200, horse.Bank);
        }

        [Fact]
        public void Go_PlayerPassesGoWithoutStopping_ShouldReceive200Dollars()
        {
            Player horse = new Player("Horse");
            Go go = new Go();

            go.Enter(horse);
            go.Exit(horse);

            Assert.Equal(200, horse.Bank);
        }
    }
}
