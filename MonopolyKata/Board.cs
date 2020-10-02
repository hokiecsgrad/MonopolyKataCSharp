using MonopolyKata.Spaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public abstract class Board
    {
        private List<Space> Spaces { get; set; }
        public int NumSpaces { get { return Spaces.Count(); } }

        public Board()
        {
            Spaces = new List<Space>();
        }

        public void AddSpace(Space space)
        {
            Spaces.Add(space);
            space.BoardReference = this;
        }

        public void AddPlayerToBoard(Player player, int boardPosition)
        {
            if (boardPosition > Spaces.Count())
                throw new ArgumentException("Starting position must be less than total spaces on the board.");

            player.Position = boardPosition;
        }

        public void Move(Player player, int numSpaces)
        {
            for (int i = 0; i < numSpaces; i++)
            {
                Spaces[player.Position].Exit(player);
                player.Position = (player.Position + 1) % Spaces.Count();
                Spaces[player.Position].Enter(player);
            }
            Spaces[player.Position].LandedOnBy(player);
        }

        public int GetBoardPositionOf(string name)
        {
            for (int i = 0; i < Spaces.Count(); i++)
            {
                if (Spaces[i].Name == name)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}