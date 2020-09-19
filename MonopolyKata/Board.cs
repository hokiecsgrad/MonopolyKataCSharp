using MonopolyKata.Spaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class Board
    {
        public List<ISpace> Spaces { get; set; }

        public void AddSpace(ISpace space)
        {
            Spaces.Add(space);
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
            Spaces[player.Position].LandsOn(player);
        }
    }
}