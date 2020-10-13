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
            Property medAve = new Property("Mediterranean Avenue", 60, 2, brown);
            Property baltic = new Property("Baltic Avenue", 60, 4, brown);

            PropertyGroup railroads = new PropertyGroup("Railroads");
            Property reading = new Railroad("Reading Railroad", 200, 25, railroads);
            Property penn = new Railroad("Pennsylvania Railroad", 200, 25, railroads);
            Property bAndO = new Railroad("B&O Railroad", 200, 25, railroads);
            Property shortLine = new Railroad("Short Line", 200, 25, railroads);

            PropertyGroup lightBlue = new PropertyGroup("Light Blue", true, 50);
            Property oriental = new Property("Oriental Avenue", 100, 6, lightBlue);
            Property vermont = new Property("Vermont Avenue", 100, 6, lightBlue);
            Property conn = new Property("Connecticut Avenue", 120, 8, lightBlue);

            PropertyGroup pink = new PropertyGroup("Pink", true, 100);
            Property stChuck = new Property("St. Charles Place", 140, 10, pink);
            Property states = new Property("States Avenue", 140, 10, pink);
            Property virginia = new Property("Virginia Avenue", 160, 12, pink);

            PropertyGroup utilities = new PropertyGroup("Utilities");
            Property electric = new Utility("Electric Company", 150, 0, utilities);
            Property water = new Utility("Water Works", 150, 0, utilities);

            PropertyGroup orange = new PropertyGroup("Orange", true, 100);
            Property stJames = new Property("St. James Place", 180, 14, orange);
            Property tenn = new Property("Tennessee Avenue", 180, 14, orange);
            Property newYork = new Property("New York Avenue", 200, 16, orange);

            PropertyGroup red = new PropertyGroup("Red", true, 150);
            Property ky = new Property("Kentucky Avenue", 220, 18, red);
            Property indiana = new Property("Indiana Avenue", 220, 18, red);
            Property illinois = new Property("Illinois Avenue", 240, 20, red);

            PropertyGroup yellow = new PropertyGroup("Yellow", true, 150);
            Property atlantic = new Property("Atlantic Avenue", 260, 22, yellow);
            Property ventnor = new Property("Ventnor Avenue", 260, 22, yellow);
            Property marv = new Property("Marvin Gardens", 280, 24, yellow);

            PropertyGroup green = new PropertyGroup("Green", true, 200);
            Property pacific = new Property("Pacific Avenue", 300, 26, green);
            Property carolina = new Property("North Carolina Avenue", 300, 26, green);
            Property pennAve = new Property("Pennsylvania Avenue", 320, 28, green);

            PropertyGroup blue = new PropertyGroup("Blue", true, 200);
            Property parkPlace = new Property("Park Place", 350, 35, blue);
            Property boardwalk = new Property("Boardwalk", 400, 50, blue);

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