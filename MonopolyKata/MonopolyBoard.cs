using MonopolyKata.Spaces;
using System.Collections.Generic;

namespace MonopolyKata
{
    public class MonopolyBoard : Board
    {
        public MonopolyBoard()
        {
            Spaces = SetupBoard();
        }

        private List<Space> SetupBoard()
        {
            List<Space> board = new List<Space>();

            board.Add(new Go());
            for (int i = 0; i < 39; i++)
                board.Add(new Empty());

            return board;
        }
    }
}