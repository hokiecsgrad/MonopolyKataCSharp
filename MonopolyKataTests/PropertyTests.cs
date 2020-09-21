using MonopolyKata;
using MonopolyKata.Spaces;
using Xunit;

namespace MonopolyKataTests
{
    public class PropertyTests
    {
        Player horse;
        Player car;
        PropertyGroup blue;
        Space parkPlace;
        Space boardwalk;

        public PropertyTests()
        {
            horse = new Player("Horse");
            horse.Bank = 500;
            car = new Player("Car");
            car.Bank = 500;

            blue = new PropertyGroup("Blue");
            parkPlace = new Property("Park Place", 350, 35, blue);
            boardwalk = new Property("Boardwalk", 400, 50, blue);
        }

        [Fact]
        public void PropertyLandedOn_WhenPropertyIsUnowned_ShouldAllowPlayerToBuy()
        {
            parkPlace.LandedOnBy(horse);

            Assert.Equal(150, horse.Bank);
            Assert.Contains(parkPlace, horse.Properties);
        }

        [Fact]
        public void PropertyLandedOn_WhenPropertyAlreadyOwnedByPlayer_ShouldDoNothing()
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
        public void PropertyPassedOver_WhenPropertyIsUnowned_ShouldDoNothing()
        {
            parkPlace.Enter(horse);
            parkPlace.Exit(horse);

            Assert.Equal(500, horse.Bank);
            Assert.DoesNotContain(parkPlace, horse.Properties);
        }

        [Fact]
        public void PropertyLandedOn_WhenPropertyIsOwnedByAnotherPlayer_ShouldChargePlayerRent()
        {
            parkPlace.LandedOnBy(car);
            Assert.Equal(150, car.Bank);

            parkPlace.LandedOnBy(horse);

            Assert.Equal(465, horse.Bank);
            Assert.Equal(185, car.Bank);
        }

        [Fact]
        public void PropertyGroup_WhenAllPropertiesInGroupAreOwned_ShouldChargeTwiceTheRent()
        {
            horse.Bank = 1000;
            parkPlace.LandedOnBy(horse);
            boardwalk.LandedOnBy(horse);

            boardwalk.LandedOnBy(car);

            Assert.Equal(400, car.Bank);
        }

        [Fact]
        public void RailroadLandedOn_WhenPlayerOwnsOne_ShouldCharge25DollarsForRent()
        {
            PropertyGroup railroads = new PropertyGroup("Railroads");
            Space reading = new Property("Reading Railroad", 200, 25, railroads);
            Space penn = new Property("Pennsylvania Railroad", 200, 25, railroads);
            Space bAndO = new Property("B & O Railroad", 200, 25, railroads);
            Space shortLine = new Property("Short Line", 200, 25, railroads);

            reading.LandedOnBy(horse);
            reading.LandedOnBy(car);

            Assert.Equal(475, car.Bank);
        }

        [Fact]
        public void RailroadLandedOn_WhenPlayerOwnsThree_ShouldCharge100DollarsForRent()
        {
            PropertyGroup railroads = new PropertyGroup("Railroads");
            Space reading = new Railroad("Reading Railroad", 200, 25, railroads);
            Space penn = new Railroad("Pennsylvania Railroad", 200, 25, railroads);
            Space bAndO = new Railroad("B & O Railroad", 200, 25, railroads);
            Space shortLine = new Railroad("Short Line", 200, 25, railroads);

            reading.LandedOnBy(horse);
            penn.LandedOnBy(horse);
            shortLine.LandedOnBy(horse);
            reading.LandedOnBy(car);

            Assert.Equal(400, car.Bank);
        }

        [Fact]
        public void UtilityLandedOn_WhenUnowned_ShouldAllowPlayerToBuy()
        {
            PropertyGroup utilities = new PropertyGroup("Utilities");
            Space electric = new Utility("Electric Company", 150, 0, utilities);
            Space water = new Utility("Water Works", 150, 0, utilities);

            electric.LandedOnBy(horse);

            Assert.Equal(350, horse.Bank);
            Assert.Contains(electric, horse.Properties);
        }

        [Fact]
        public void UtilityLandedOn_WhenOneIsOwnedByAnotherPlayer_ShouldCharge4TimesDiceRoll()
        {
            PropertyGroup utilities = new PropertyGroup("Utilities");
            Space electric = new Utility("Electric Company", 150, 0, utilities);
            Space water = new Utility("Water Works", 150, 0, utilities);

            car.LastRoll = 5;
            electric.LandedOnBy(horse);
            Assert.Equal(350, horse.Bank);
            Assert.Contains(electric, horse.Properties);

            electric.LandedOnBy(car);

            Assert.Equal(480, car.Bank);
        }

        [Fact]
        public void UtilityLandedOn_WhenTwoOwnedByAnotherPlayer_ShouldCharge10TimesDiceRoll()
        {
            PropertyGroup utilities = new PropertyGroup("Utilities");
            Space electric = new Utility("Electric Company", 150, 0, utilities);
            Space water = new Utility("Water Works", 150, 0, utilities);

            car.LastRoll = 5;
            electric.LandedOnBy(horse);
            water.LandedOnBy(horse);
            Assert.Equal(200, horse.Bank);
            Assert.Contains(electric, horse.Properties);
            Assert.Contains(water, horse.Properties);

            electric.LandedOnBy(car);
            
            Assert.Equal(450, car.Bank);
        }
    }
}
