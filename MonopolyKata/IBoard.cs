using MonopolyKata.Spaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public interface IBoard
    {
        public int NumSpaces { get; }

        public void AddSpace(Space space);
        public void AddPlayerToBoard(Player player, int boardPosition);
        public void Move(Player player, int numSpaces);
        public int GetBoardPositionOfSpace(string name);
        public List<Property> GetPropertiesInGroup(string groupName);
    }
}