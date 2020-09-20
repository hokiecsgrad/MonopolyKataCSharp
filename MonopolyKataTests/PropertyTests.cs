using MonopolyKata;
using MonopolyKata.Spaces;
using Xunit;

namespace MonopolyKataTests
{
    public class PropertyTests
    {
        [Fact]
        public void LandedOn_WhenPropertyIsUnowned_ShouldAllowPlayerToBuy()
        {
            Player horse = new Player("Horse");
            horse.Bank = 500;
            Space parkPlace = new ParkPlace();

            parkPlace.LandedOnBy(horse);

            Assert.Equal(150, horse.Bank);
            Assert.Contains(parkPlace, horse.Properties);
        }

        [Fact]
        public void LandedOn_WhenPropertyAlreadyOwnedByPlayer_ShouldDoNothing()
        {
            Player horse = new Player("Horse");
            horse.Bank = 500;
            Space parkPlace = new ParkPlace();

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
            Player horse = new Player("Horse");
            horse.Bank = 500;
            Space parkPlace = new ParkPlace();

            parkPlace.Enter(horse);
            parkPlace.Exit(horse);

            Assert.Equal(500, horse.Bank);
            Assert.DoesNotContain(parkPlace, horse.Properties);
        }

        [Fact]
        public void LandedOn_WhenPropertyIsOwnedByAnotherPlayer_ShouldChargePlayerRent()
        {
            Player horse = new Player("Horse");
            horse.Bank = 500;
            Player car = new Player("Car");
            car.Bank = 500;
            Space parkPlace = new ParkPlace();

            parkPlace.LandedOnBy(car);
            Assert.Equal(150, car.Bank);

            parkPlace.LandedOnBy(horse);
            
            Assert.Equal(465, horse.Bank);
            Assert.Equal(185, car.Bank);
        }
    }
}
