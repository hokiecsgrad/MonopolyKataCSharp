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
            player.BoardRef = this;
        }

        public void Move(Player player, int numSpaces)
        {
            int modifier = 1;
            if (numSpaces < 0) modifier = modifier * -1;

            for (int i = 0; i < Math.Abs(numSpaces); i++)
            {
                Spaces[player.Position].Exit(player);
                if (!player.IsInJail)
                {
                    player.Position = (player.Position + modifier) % Spaces.Count();
                    if (player.Position < 0) player.Position = Spaces.Count() - 1;
                    Spaces[player.Position].Enter(player);
                }
            }
            Spaces[player.Position].LandedOnBy(player);
        }

        public int GetBoardPositionOfSpace(string name)
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

        public List<Property> GetPropertiesInGroup(string groupName)
        {
            List<Property> props = new List<Property>();
            props = Spaces
                .OfType<Property>()
                .Where( prop => prop.Group.Name == groupName )
                .ToList<Property>();
            return props;
        }
    }
}