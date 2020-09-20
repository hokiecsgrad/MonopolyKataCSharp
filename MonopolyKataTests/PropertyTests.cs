using MonopolyKata;
using MonopolyKata.Spaces;
using Xunit;

namespace MonopolyKataTests
{
    public class PropertyTests
    {
        Player horse;
        Player car;
        Space parkPlace;
        Space boardwalk;

        public PropertyTests()
        {
            horse = new Player("Horse");
            horse.Bank = 500;
            car = new Player("Car");
            car.Bank = 500;
            parkPlace = new Property("Park Place", 350, 35);
            boardwalk = new Property("Boardwalk", 400, 50);
        }

        [Fact]
        public void LandedOn_WhenPropertyIsUnowned_ShouldAllowPlayerToBuy()
        {
            parkPlace.LandedOnBy(horse);

            Assert.Equal(150, horse.Bank);
            Assert.Contains(parkPlace, horse.Properties);
        }

        [Fact]
        public void LandedOn_WhenPropertyAlreadyOwnedByPlayer_ShouldDoNothing()
        {
            Assert.DoesNotContain(parkPlace, horse.Properties);

            parkPlace.LandedOnBy(horse);

            Assert.Equal(150, horse.Bank);
            Assert.Contains(parkPlace, horse.Properties);

            parkPlace.LandedOnBy(horse);

            Assert.Equal(150, horse.Bank);
            Assert.Contains(parkPlace, horse.Properties);
        }

        [Fact]
        public void PassOver_WhenPropertyIsUnowned_ShouldDoNothing()
        {
            parkPlace.Enter(horse);
            parkPlace.Exit(horse);

            Assert.Equal(500, horse.Bank);
            Assert.DoesNotContain(parkPlace, horse.Properties);
        }

        [Fact]
        public void LandedOn_WhenPropertyIsOwnedByAnotherPlayer_ShouldChargePlayerRent()
        {
            parkPlace.LandedOnBy(car);
            Assert.Equal(150, car.Bank);

            parkPlace.LandedOnBy(horse);

            Assert.Equal(465, horse.Bank);
            Assert.Equal(185, car.Bank);
        }

        [Fact]
        public void LandedOn_WhenAllPropertiesInGroupAreOwned_ShouldChargeTwiceTheRent()
        {

        }
    }
}
