using System.Collections.Generic;

namespace Monopoly
{
    public class MonopolyBoard : Board
    {
        public MonopolyBoard()
        {
            Spaces = SetupBoard();
        }

        private List<Player>[] SetupBoard()
        {
            List<Player>[] board = new List<Player>[40];

            for (int i = 0; i < board.Length; i++)
                board[i] = new List<Player>();

            return board;
        }
    }
}