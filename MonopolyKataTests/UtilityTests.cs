using MonopolyKata;
using MonopolyKata.Spaces;
using Xunit;

namespace MonopolyKataTests
{
    public class UtilityTests
    {
        [Fact]
        public void LandedOn_WhenUtilityIsUnowned_ShouldAllowPlayerToBuy()
        {
            Player horse = new Player("Horse");
            horse.Bank = 500;
            Space electric = new ElectricCompany();

            electric.LandedOnBy(horse);

            Assert.Equal(350, horse.Bank);
            Assert.Contains(electric, horse.Properties);
        }

        [Fact]
        public void Rent_WhenOneUtilityIsOwnedByAnotherPlayer_ShouldCharge4TimesDiceRoll()
        {
            Player horse = new Player("Horse");
            horse.Bank = 500;
            Player car = new Player("Car");
            car.Bank = 500;
            car.LastRoll = 5;
            Space electric = new ElectricCompany();

            electric.LandedOnBy(horse);
            Assert.Equal(350, horse.Bank);
            Assert.Contains(electric, horse.Properties);

            electric.LandedOnBy(car);
            Assert.Equal(480, car.Bank);
        }

        [Fact]
        public void Rent_WhenTwoUtilitiesOwnedByAnotherPlayer_ShouldCharge10TimesDiceRoll()
        {
            Player horse = new Player("Horse");
            horse.Bank = 500;
            Player car = new Player("Car");
            car.Bank = 500;
            car.LastRoll = 5;
            Space electric = new ElectricCompany();
            Space water = new WaterWorks();

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
