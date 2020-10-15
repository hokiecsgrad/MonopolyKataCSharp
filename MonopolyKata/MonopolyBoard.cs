using Microsoft.Extensions.Logging;
using MonopolyKata.Cards;
using MonopolyKata.Spaces;
using System.Collections.Generic;

namespace MonopolyKata
{
    public class MonopolyBoard : Board
    {
        public MonopolyBoard(ILoggerFactory loggerFactory = null)
            : base(loggerFactory)
        {
            _logger?.LogInformation("Creating default Monopoly board.");
            SetupBoard();
            _logger?.LogInformation("Monopoly board created.");
        }

        private void SetupBoard()
        {
            List<Space> spaces = new List<Space>();

            PropertyGroup brown = new PropertyGroup("Brown", true, 50);
            Property medAve = new Property("Mediterranean Avenue", 60, new int[] {2, 10, 30, 90, 160, 250}, brown);
            Property baltic = new Property("Baltic Avenue", 60, new int[] {4, 20, 60, 180, 320, 450}, brown);

            PropertyGroup railroads = new PropertyGroup("Railroads");
            Property reading = new Railroad("Reading Railroad", 200, 25, railroads);
            Property penn = new Railroad("Pennsylvania Railroad", 200, 25, railroads);
            Property bAndO = new Railroad("B&O Railroad", 200, 25, railroads);
            Property shortLine = new Railroad("Short Line", 200, 25, railroads);

            PropertyGroup lightBlue = new PropertyGroup("Light Blue", true, 50);
            Property oriental = new Property("Oriental Avenue", 100, new int[] {6, 30, 90, 270, 400, 550}, lightBlue);
            Property vermont = new Property("Vermont Avenue", 100, new int[] {6, 30, 90, 270, 400, 550}, lightBlue);
            Property conn = new Property("Connecticut Avenue", 120, new int[] {8, 40, 100, 300, 450, 600}, lightBlue);

            PropertyGroup pink = new PropertyGroup("Pink", true, 100);
            Property stChuck = new Property("St. Charles Place", 140, new int[] {10, 50, 150, 450, 625, 750}, pink);
            Property states = new Property("States Avenue", 140, new int[] {10, 50, 150, 450, 625, 750}, pink);
            Property virginia = new Property("Virginia Avenue", 160, new int[] {12, 60, 180, 500, 700, 900}, pink);

            PropertyGroup utilities = new PropertyGroup("Utilities");
            Property electric = new Utility("Electric Company", 150, 0, utilities);
            Property water = new Utility("Water Works", 150, 0, utilities);

            PropertyGroup orange = new PropertyGroup("Orange", true, 100);
            Property stJames = new Property("St. James Place", 180, new int[] {14, 70, 200, 550, 750, 950}, orange);
            Property tenn = new Property("Tennessee Avenue", 180, new int[] {14, 70, 200, 550, 750, 950}, orange);
            Property newYork = new Property("New York Avenue", 200, new int[] {16, 80, 220, 600, 800, 1000}, orange);

            PropertyGroup red = new PropertyGroup("Red", true, 150);
            Property ky = new Property("Kentucky Avenue", 220, new int[] {18, 90, 250, 700, 875, 1050}, red);
            Property indiana = new Property("Indiana Avenue", 220, new int[] {18, 90, 250, 700, 875, 1050}, red);
            Property illinois = new Property("Illinois Avenue", 240, new int[] {20, 100, 300, 750, 925, 1100}, red);

            PropertyGroup yellow = new PropertyGroup("Yellow", true, 150);
            Property atlantic = new Property("Atlantic Avenue", 260, new int[] {22, 110, 330, 800, 975, 1150}, yellow);
            Property ventnor = new Property("Ventnor Avenue", 260, new int[] {22, 110, 330, 800, 975, 1150}, yellow);
            Property marv = new Property("Marvin Gardens", 280, new int[] {24, 120, 360, 850, 1025, 1200}, yellow);

            PropertyGroup green = new PropertyGroup("Green", true, 200);
            Property pacific = new Property("Pacific Avenue", 300, new int[] {26, 130, 390, 900, 1100, 1275}, green);
            Property carolina = new Property("North Carolina Avenue", 300, new int[] {26, 130, 390, 900, 1100, 1275}, green);
            Property pennAve = new Property("Pennsylvania Avenue", 320, new int[] {28, 150, 450, 1000, 1200, 1400}, green);

            PropertyGroup blue = new PropertyGroup("Blue", true, 200);
            Property parkPlace = new Property("Park Place", 350, new int[] {35, 175, 500, 1100, 1300, 1500}, blue);
            Property boardwalk = new Property("Boardwalk", 400, new int[] {50, 200, 600, 1400, 1700, 2000}, blue);

            Space chance = new CardSpace("Chance", DeckFactory.Chance());
            Space commChest = new CardSpace("Community Chest", DeckFactory.CommunityChest());

            spaces = new List<Space> {
                new Go(), medAve, commChest, baltic, new IncomeTax(), reading, oriental, chance, vermont, conn,
                new Jail(), stChuck, electric, states, virginia, penn, stJames, commChest, tenn, newYork,
                new FreeParking(), ky, chance, indiana, illinois, bAndO, atlantic, ventnor, water, marv, new Spaces.GoToJail(),
                pacific, carolina, commChest, pennAve, shortLine, chance, parkPlace, new LuxuryTax(), boardwalk
            };

            foreach (Space space in spaces)
                AddSpace(space);
        }
    }
}