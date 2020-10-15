using MonopolyKata;
using MonopolyKata.Spaces;
using Xunit;

namespace MonopolyKataTests
{
    public class HousesAndHotelsTests
    {
        PropertyGroup blue;
        Property parkPlace;
        Property boardwalk;
        Player horse;
        Player car;

        public HousesAndHotelsTests()
        {
            blue = new PropertyGroup("Blue", true, 200);
            parkPlace = new Property("Park Place", 350, new int[] {35, 175, 500, 1100, 1300, 1500}, blue);
            boardwalk = new Property("Boardwalk", 400, new int[] {50, 200, 600, 1400, 1700, 2000}, blue);
            horse = new Player("Horse");
            car = new Player("Car");
        }
        
        [Fact]
        public void Build_PlayerDoesNotHaveMonopoly_ShouldNotBuildAnything()
        {
            horse.Bank = 1000;

            parkPlace.LandedOnBy(horse);
            horse.Build();

            Assert.Equal(650, horse.Bank);
            Assert.Equal(0, parkPlace.NumBuildings);
        }

        [Fact]
        public void Build_PlayerHasMonopolyAndEnoughMoneyForOneSetOfHouses_ShouldBuildHouses()
        {
            horse.Bank = 1200;

            parkPlace.LandedOnBy(horse);
            boardwalk.LandedOnBy(horse);
            horse.Build();

            Assert.Equal(50, horse.Bank);
            Assert.Equal(1, parkPlace.NumBuildings);
            Assert.Equal(1, boardwalk.NumBuildings);
        }

        [Fact]
        public void Build_PlayerHasMonopolyAndEnoughMoneyForOneHotel_ShouldBuildOneHotel()
        {
            horse.Bank = 2750;

            parkPlace.LandedOnBy(horse);
            boardwalk.LandedOnBy(horse);
            horse.Build();

            Assert.Equal(0, horse.Bank);
            Assert.Equal(5, parkPlace.NumBuildings);
            Assert.Equal(5, boardwalk.NumBuildings);
        }

        [Fact]
        public void Build_PlayerHasMonopolyAndEnoughMoneyForThreeHotels_ShouldBuildTwoHotels()
        {
            horse.Bank = 6750;

            parkPlace.LandedOnBy(horse);
            boardwalk.LandedOnBy(horse);
            horse.Build();

            Assert.Equal(2000, horse.Bank);
            Assert.Equal(10, parkPlace.NumBuildings);
            Assert.Equal(10, boardwalk.NumBuildings);
        }

        [Fact]
        public void Rent_PropertyHasOneHouse_ShouldPay175Dollars()
        {
            // http://www.falstad.com/monopoly.html
            horse.Bank = 1200;
            car.Bank = 175;

            parkPlace.LandedOnBy(horse);
            boardwalk.LandedOnBy(horse);
            horse.Build();
            parkPlace.LandedOnBy(car);

            Assert.Equal(0, car.Bank);
        }

        [Fact]
        public void Rent_PropertyHasTwoHouses_ShouldPay500Dollars()
        {
            horse.Bank = 1600;
            car.Bank = 500;

            parkPlace.LandedOnBy(horse);
            boardwalk.LandedOnBy(horse);
            horse.Build();
            parkPlace.LandedOnBy(car);

            Assert.Equal(0, car.Bank);
        }

        [Fact]
        public void Rent_PropertyHasOneHotel_ShouldPay1500Dollars()
        {
            horse.Bank = 2750;
            car.Bank = 1500;

            parkPlace.LandedOnBy(horse);
            boardwalk.LandedOnBy(horse);
            horse.Build();
            parkPlace.LandedOnBy(car);

            Assert.Equal(0, car.Bank);
        }
    }
}