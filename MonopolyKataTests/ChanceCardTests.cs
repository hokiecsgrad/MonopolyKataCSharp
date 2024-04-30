using MonopolyKata;
using MonopolyKata.Cards;
using MonopolyKata.Spaces;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MonopolyKataTests
{
    public class CardBoard : Board
    {
        public CardBoard()
        {
            AddSpace(new Go());
            AddSpace(new Empty());
            AddSpace(new IncomeTax());
            AddSpace(new Empty());
            PropertyGroup railroads = new PropertyGroup("Railroads");
            Property reading = new Railroad("Reading Railroad", 200, 25, railroads);
            Property penn = new Railroad("Pennsylvania Railroad", 200, 25, railroads);
            Property bAndO = new Railroad("B&O Railroad", 200, 25, railroads);
            Property shortLine = new Railroad("Short Line", 200, 25, railroads);
            AddSpace(reading);
            AddSpace(penn);
            AddSpace(bAndO);
            AddSpace(shortLine);
            PropertyGroup utilities = new PropertyGroup("Utilities");
            Property electric = new Utility("Electric Company", 150, 0, utilities);
            Property water = new Utility("Water Works", 150, 0, utilities);
            AddSpace(water);
            AddSpace(electric);
            PropertyGroup brown = new PropertyGroup("Brown", true, 50);
            Property medAve = new Property("Mediterranean Avenue", 60, new int[] { 2, 10, 30, 90, 160, 250 }, brown);
            Property baltic = new Property("Baltic Avenue", 60, new int[] { 4, 20, 60, 180, 320, 450 }, brown);
            AddSpace(medAve);
            AddSpace(baltic);
        }
    }

    public class ChanceCardTests
    {
        [Fact]
        public void GoBack3Spaces_WhenOnSomeRandomSpace_ShouldMove3SpacesBack()
        {
            Board board = new CardBoard();
            GoBack3Spaces back = new GoBack3Spaces();
            Player horse = new Player("Horse");
            horse.Bank = 175;
            board.AddPlayerToBoard(horse, 5);

            back.Execute(horse);

            Assert.Equal(158, horse.Bank);
            Assert.Equal(2, horse.Position);
        }

        [Fact]
        public void AdvanceToRailroad_AllRailroadsOwned_ShouldPayDoubleRentToOwner()
        {
            Board board = new CardBoard();
            AdvanceToRailroad advToRr = new AdvanceToRailroad();
            Player horse = new Player("Horse");
            Player car = new Player("Car");
            car.Bank = 2000;
            board.AddPlayerToBoard(car, 0);
            board.AddPlayerToBoard(horse, 0);

            board.Move(car, 4);
            board.Move(car, 1);
            board.Move(car, 1);
            board.Move(car, 1);

            advToRr.Execute(horse);

            Assert.Equal(-400, horse.Bank);
            Assert.Equal(4, horse.Position);
        }

        [Fact]
        public void AdvanceToReading_ReadingUnowned_ShouldMovePlayerToReadingRailroadAndPurchase()
        {
            Board board = new CardBoard();
            AdvanceToReading advToRead = new AdvanceToReading();
            Player horse = new Player("Horse");
            horse.Bank = 500;
            board.AddPlayerToBoard(horse, 0);

            advToRead.Execute(horse);

            Assert.Equal(300, horse.Bank);
            Assert.Equal(4, horse.Position);
        }

        [Fact]
        public void AdvanceToUtility_UtilityUnowned_ShouldMovePlayerToWaterWorksAndPurchase()
        {
            Board board = new CardBoard();
            AdvanceToUtility advToUtil = new AdvanceToUtility();
            Player horse = new Player("Horse");
            horse.Bank = 500;
            board.AddPlayerToBoard(horse, 0);

            advToUtil.Execute(horse);

            Assert.Equal(350, horse.Bank);
            Assert.Equal(8, horse.Position);
        }

        [Fact]
        public void AdvanceToUtility_UtilityOwned_ShouldMovePlayerToWaterWorksAndPayRent()
        {
            Board board = new CardBoard();
            AdvanceToUtility advToUtil = new AdvanceToUtility();
            Player horse = new Player("Horse");
            Player car = new Player("Car");
            car.Bank = 2000;
            board.AddPlayerToBoard(car, 0);
            board.AddPlayerToBoard(horse, 0);

            board.Move(car, 8);

            horse.LastRoll = (6, 6);
            advToUtil.Execute(horse);

            Assert.True((horse.Bank > -120) && (horse.Bank <= -20));
            Assert.Equal(8, horse.Position);
        }

        [Fact]
        public void Birthday_WithTwoOtherPlayers_ShouldCollect20Dollars()
        {
            Monopoly game = new Monopoly(null, null, null);
            Board board = new CardBoard();
            Birthday birthday = new Birthday();
            Player horse = new Player("Horse");
            Player car = new Player("Car");
            Player shoe = new Player("Shoe");
            game.AddPlayer(horse);
            game.AddPlayer(car);
            game.AddPlayer(shoe);
            board.AddPlayerToBoard(car, 0);
            board.AddPlayerToBoard(horse, 0);
            board.AddPlayerToBoard(shoe, 0);

            birthday.Execute(horse);

            Assert.Equal(20, horse.Bank);
            Assert.Equal(-10, car.Bank);
            Assert.Equal(-10, shoe.Bank);
        }

        [Fact]
        public void StreetRepairs_WithTwoHouses_ShouldPay80Dollars()
        {
            Board board = new CardBoard();
            StreetRepairs repairs = new StreetRepairs();
            Player horse = new Player("Horse");
            horse.Bank = 220;
            board.AddPlayerToBoard(horse, 0);

            board.Move(horse, 10);
            board.Move(horse, 1);
            horse.Build();

            repairs.Execute(horse);

            Assert.Equal(-80, horse.Bank);
        }
    }
}
