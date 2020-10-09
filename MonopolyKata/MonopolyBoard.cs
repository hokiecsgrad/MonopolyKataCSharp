using MonopolyKata.Spaces;
using System.Collections.Generic;

namespace MonopolyKata
{
    public class MonopolyBoard : Board
    {
        public MonopolyBoard()
        {
            SetupBoard();
        }

        private void SetupBoard()
        {
            List<Space> board = new List<Space>();

            PropertyGroup brown = new PropertyGroup("Brown");
            Property medAve = new Property("Mediterranean Avenue", 60, 2, brown);
            Property baltic = new Property("Baltic Avenue", 60, 4, brown);

            PropertyGroup railroads = new PropertyGroup("Railroads");
            Property reading = new Railroad("Reading Railroad", 200, 0, railroads);
            Property penn = new Railroad("Pennsylvania Railroad", 200, 0, railroads);
            Property bAndO = new Railroad("B&O Railroad", 200, 0, railroads);
            Property shortLine = new Railroad("Short Line", 200, 0, railroads);

            PropertyGroup lightBlue = new PropertyGroup("Light Blue");
            Property oriental = new Property("Oriental Avenue", 100, 6, lightBlue);
            Property vermont = new Property("Vermont Avenue", 100, 6, lightBlue);
            Property conn = new Property("Connecticut Avenue", 120, 8, lightBlue);

            PropertyGroup pink = new PropertyGroup("Pink");
            Property stChuck = new Property("St. Charles Place", 140, 10, pink);
            Property states = new Property("States Avenue", 140, 10, pink);
            Property virginia = new Property("Virginia Avenue", 160, 12, pink);

            PropertyGroup utilities = new PropertyGroup("Utilities");
            Property electric = new Utility("Electric Company", 150, 0, utilities);
            Property water = new Utility("Water Works", 150, 0, utilities);

            PropertyGroup orange = new PropertyGroup("Orange");
            Property stJames = new Property("St. James Place", 180, 14, orange);
            Property tenn = new Property("Tennessee Avenue", 180, 14, orange);
            Property newYork = new Property("New York Avenue", 200, 16, orange);

            PropertyGroup red = new PropertyGroup("Red");
            Property ky = new Property("Kentucky Avenue", 220, 18, red);
            Property indiana = new Property("Indiana Avenue", 220, 18, red);
            Property illinois = new Property("Illinois Avenue", 240, 20, red);

            PropertyGroup yellow = new PropertyGroup("Yellow");
            Property atlantic = new Property("Atlantic Avenue", 260, 22, yellow);
            Property ventnor = new Property("Ventnor Avenue", 260, 22, yellow);
            Property marv = new Property("Marvin Gardens", 280, 24, yellow);

            PropertyGroup green = new PropertyGroup("Green");
            Property pacific = new Property("Pacific Avenue", 300, 26, green);
            Property carolina = new Property("North Carolina Avenue", 300, 26, green);
            Property pennAve = new Property("Pennsylvania Avenue", 320, 28, green);

            PropertyGroup blue = new PropertyGroup("Blue");
            Property parkPlace = new Property("Park Place", 350, 35, blue);
            Property boardwalk = new Property("Boardwalk", 400, 50, blue);

            CardSpace communityChest = new CardSpace("Community Chest", DeckFactory.CommunityChest());
            CardSpace chance = new CardSpace("Chance", DeckFactory.Chance());

            board = new List<Space> {
                new Go(), medAve, communityChest, baltic, new IncomeTax(), reading, oriental, chance, vermont, conn,
                new Jail(), stChuck, electric, states, virginia, penn, stJames, communityChest, tenn, newYork,
                new Empty(), ky, chance, indiana, illinois, bAndO, atlantic, ventnor, water, marv, new GoToJail(),
                pacific, carolina, communityChest, pennAve, shortLine, chance, parkPlace, new LuxuryTax(), boardwalk
            };

            foreach (Space space in board)
                AddSpace(space);
        }
    }
}