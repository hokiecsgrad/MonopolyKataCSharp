using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

using MonopolyKata.Spaces;

namespace MonopolyKata
{
    public abstract class Board : IBoard
    {
        internal readonly ILogger<Board>? _logger = null;

        public List<Space> Spaces { get; set; }

        public int NumSpaces { get { return Spaces.Count; } }


        public Board(ILoggerFactory? loggerFactory = null)
        {
            _logger = loggerFactory?.CreateLogger<Board>();
            Spaces = [];
        }

        public void AddSpace(Space space)
        {
            Spaces.Add(space);
            space.BoardReference = this;
        }

        public Space GetSpace(int position)
        {
            return Spaces[position];
        }

        public void AddPlayerToBoard(Player player, int boardPosition)
        {
            if (boardPosition > Spaces.Count)
                throw new ArgumentException("Starting position must be less than total spaces on the board.");

            player.Position = boardPosition;
            player.BoardRef = this;

            _logger?.LogInformation($"{player.Name} added to the board at {Spaces[boardPosition].Name}.");
        }

        public void Move(Player player, int numSpaces)
        {
            int modifier = 1;
            if (numSpaces < 0) modifier *= -1;

            for (int i = 0; i < Math.Abs(numSpaces); i++)
            {
                Spaces[player.Position].Exit(player);
                if (!player.IsInJail)
                {
                    player.Position = (player.Position + modifier) % Spaces.Count;
                    if (player.Position < 0) player.Position = Spaces.Count - 1;
                    Spaces[player.Position].Enter(player);
                }
            }

            Spaces[player.Position].LandedOnBy(player);
        }

        public void MoveToFirstPropertyOfType(Player player, string group)
        {
            string currGroup = Spaces[player.Position] switch
            {
                Property p => p.Group.Name,
                _ => ""
            };

            while (currGroup != group)
            {
                Spaces[player.Position].Exit(player);
                player.Position++;
                if (player.Position >= Spaces.Count) player.Position = 0;
                Spaces[player.Position].Enter(player);
                currGroup = Spaces[player.Position] switch
                {
                    Property p => p.Group.Name,
                    _ => ""
                };
            }
        }

        public void MoveToSpaceNamed(Player player, string name)
        {
            int positionOfSpace = GetBoardPositionOfSpace(name);
            int distance = GetNumberOfMovesToPosition(player.Position, positionOfSpace);
            Move(player, distance);
        }

        public int FindDistanceToNearestProperty(int currentPosition, List<Property> properties)
        {
            int minDistance = NumSpaces;

            foreach (Space space in properties)
            {
                int targetPosition = GetBoardPositionOfSpace(space.Name);
                int distance = GetNumberOfMovesToPosition(currentPosition, targetPosition);
                if (distance < minDistance) minDistance = distance;
            }

            return minDistance;
        }

        public int GetNumberOfMovesToPosition(int currentPosition, int target)
        {
            int distance;

            if (target < currentPosition)
                distance = NumSpaces - currentPosition + target;
            else
                distance = target - currentPosition;

            return distance;
        }

        public int GetBoardPositionOfSpace(string name)
        {
            for (int i = 0; i < Spaces.Count; i++)
                if (Spaces[i].Name == name)
                    return i;
            return -1;
        }

        public List<Property> GetPropertiesInGroup(string groupName)
        {
            List<Property> props = new List<Property>();
            props = [.. Spaces
                .OfType<Property>()
                .Where( prop => prop.Group.Name == groupName )];
            return props;
        }
    }
}