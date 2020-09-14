using System.Collections.Generic;

namespace Monopoly
{
    public class Board
    {
        public List<Player>[] Spaces { get; set; }

        public void Move(Player player, int numSpaces)
        {
            int playerPosition = FindPlayerPosition(player);
            if (playerPosition < 0)
                return;

            this.Spaces[playerPosition].Remove(player);
            this.Spaces[CalculateNewSpace(playerPosition, numSpaces)].Add(player);
        }

        public int FindPlayerPosition(Player player)
        {
            int playerPosition = -1;

            for (int i = 0; i < this.Spaces.Length; i++)
            {
                if (this.Spaces[i].Contains(player))
                {
                    playerPosition = i;
                    break;
                }
            }

            return playerPosition;
        }

        private int CalculateNewSpace(int oldSpace, int spacesToMove)
        {
            return (oldSpace + spacesToMove) % this.Spaces.Length;
        }
    }
}