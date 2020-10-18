using Microsoft.Extensions.Logging;
using MonopolyKata.Spaces;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class Player
    {
        private readonly ILogger<Player> _logger = null;
        public string Name { get; set; }
        private int _bank = 0;
        public int Bank { 
            get {
                return _bank;
            }
            set {
                _bank = value;
                _logger?.LogDebug("{0}'s bank balance is now ${1}.", Name, Bank);
            }
        }
        public int Position { get; set; }
        public Board BoardRef { get; set; }
        public Monopoly GameRef { get; set; }
        public int Rounds { get; set; }
        public (int, int) LastRoll { get; set; }
        public bool IsInJail { get; set; }
        public int NumTurnsInJail { get; set; }
        public bool WantsToPayToGetOutOfJail { get; set; }
        public List<Property> Properties { get; set; }

        public Player(string name, ILoggerFactory loggerFactory = null)
        {
            Name = name;
            Bank = 0;
            Position = 0;
            Rounds = 0;
            LastRoll = (0, 0);
            IsInJail = false;
            NumTurnsInJail = 0;
            WantsToPayToGetOutOfJail = false;
            Properties = new List<Property>();
            _logger = loggerFactory?.CreateLogger<Player>();
        }

        public int GetNumberOfPropertiesOwnedInGroup(PropertyGroup group)
        {
            int numProperties = Properties.Count(property => property.Group == group);
            return numProperties;
        }

        public bool HasMonopoly(PropertyGroup group)
        {
            int numPropsInGroup = group.Properties.Count;
            int numPropsOwnedInGroup = GetNumberOfPropertiesOwnedInGroup(group);
            return numPropsInGroup == numPropsOwnedInGroup;
        }

        public void Build()
        {
            List<PropertyGroup> candidates = 
                Properties.Select(g => g.Group)
                            .Distinct()
                            .OrderBy(g => g.CostOfHouse)
                            .ToList();

            foreach( PropertyGroup group in candidates )
                group.BuyProperties(this);
        }

        public void MoveToSpaceNamed(string space)
        {
            int newPosition = BoardRef.GetBoardPositionOfSpace(space);
            int numberOfMoves = GetNumberOfMovesToPosition(newPosition);
            BoardRef.Move(this, numberOfMoves);
        }

        public void MoveToNearestSpaceInGroup(string groupName)
        {
            List<Property> properties = BoardRef.GetPropertiesInGroup(groupName);
            int distance = FindDistanceToNearestProperty(properties);
            BoardRef.Move(this, distance);
        }

        private int FindDistanceToNearestProperty(List<Property> properties)
        {
            int minDistance = BoardRef.NumSpaces;

            foreach (Space space in properties)
            {
                int targetPosition = BoardRef.GetBoardPositionOfSpace(space.Name);
                int distance = GetNumberOfMovesToPosition(targetPosition);
                if (distance < minDistance) minDistance = distance;
            }

            return minDistance;
        }

        private int GetNumberOfMovesToPosition(int target)
        {
            int distance;

            if (target < Position)
                distance = BoardRef.NumSpaces - Position + target;
            else 
                distance = target - Position;
            
            return distance;
        }

        public void SendToJail()
        {
            IsInJail = true;
            NumTurnsInJail = 0;
            WantsToPayToGetOutOfJail = false;
            Position = BoardRef.GetBoardPositionOfSpace("Jail");
        }

        public bool CanExitJail()
        {
            if ( ! IsInJail )
                return true;
            
            if ( WantsToPayToGetOutOfJail )
            {
                Bank -= 50;
                return true;
            }
 
            if ( LastRoll.Item1 == LastRoll.Item2 )
                return true;

            if ( NumTurnsInJail == 3 )
            {
                Bank -= 50;
                return true;
            }

            return false;
        }

        public void Bankrupt()
        {
            foreach ( Property prop in Properties )
            {
                prop.Reset();
            }
        }
    }
}