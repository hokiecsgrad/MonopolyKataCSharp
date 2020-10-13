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

        public HousesAndHotelsTests()
        {
            blue = new PropertyGroup("Blue", true, 200);
            parkPlace = new Property("Park Place", 350, 35, blue);
            boardwalk = new Property("Boardwalk", 400, 50, blue);
            horse = new Player("Horse");
        }
        
        [Fact]
        public void Build_PlayerDoesNotHaveMonopoly_ShouldNotBuildAnything()
        {
            horse.Bank = 1000;

            parkPlace.LandedOnBy(horse);
            horse.BuildOn(blue);

            Assert.Equal(650, horse.Bank);
            Assert.Equal(0, parkPlace.NumHouses);
        }

        [Fact]
        public void Build_PlayerHasMonopolyAndEnoughMoneyForOneSetOfHouses_ShouldBuildHouses()
        {
            horse.Bank = 1200;

            parkPlace.LandedOnBy(horse);
            boardwalk.LandedOnBy(horse);
            horse.BuildOn(blue);

            Assert.Equal(50, horse.Bank);
            Assert.Equal(1, parkPlace.NumHouses);
            Assert.Equal(1, boardwalk.NumHouses);
        }

        [Fact]
        public void Build_PlayerHasMonopolyAndEnoughMoneyForOneHotel_ShouldBuildOneHotel()
        {
            horse.Bank = 2750;

            parkPlace.LandedOnBy(horse);
            boardwalk.LandedOnBy(horse);
            horse.BuildOn(blue);

            Assert.Equal(0, horse.Bank);
            Assert.Equal(0, parkPlace.NumHouses);
            Assert.Equal(0, boardwalk.NumHouses);
            Assert.Equal(1, parkPlace.NumHotels);
            Assert.Equal(1, boardwalk.NumHotels);
        }

        [Fact]
        public void Build_PlayerHasMonopolyAndEnoughMoneyForThreeHotels_ShouldBuildOneHotel()
        {
            horse.Bank = 6750;

            parkPlace.LandedOnBy(horse);
            boardwalk.LandedOnBy(horse);
            horse.BuildOn(blue);

            Assert.Equal(4000, horse.Bank);
            Assert.Equal(0, parkPlace.NumHouses);
            Assert.Equal(0, boardwalk.NumHouses);
            Assert.Equal(1, parkPlace.NumHotels);
            Assert.Equal(1, boardwalk.NumHotels);
        }

        [Fact]
        public void Rent_PropertyHasOneHouse_ShouldBeMultipliedBy5()
        {
            // http://www.falstad.com/monopoly.html
            Assert.True(false);
        }

        [Fact]
        public void Rent_PropertyHasTwoHouses_ShouldBeMultipliedBy15()
        {
            Assert.True(false);
        }

        [Fact]
        public void Rent_PropertyHasThreeHouses_ShouldBeMultipliedBy45()
        {
            Assert.True(false);
        }

        [Fact]
        public void Rent_PropertyHasFourHouses_ShouldBeMultipliedBy80()
        {
            Assert.True(false);
        }

        [Fact]
        public void Rent_PropertyHasOneHotel_ShouldBeMultipliedBy125()
        {
            Assert.True(false);
        }
    }
}