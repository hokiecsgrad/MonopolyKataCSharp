using MonopolyKata.Spaces;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class Player
    {
        public string Name { get; set; }
        public int Bank { get; set; }
        public int Position { get; set; }
        public Board BoardRef { get; set; }
        public int Rounds { get; set; }
        public (int, int) LastRoll { get; set; }
        public bool IsInJail { get; set; }
        public int NumTurnsInJail { get; set; }
        public bool WantsToPayToGetOutOfJail { get; set; }
        public List<Property> Properties { get; set; }

        public Player(string name)
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
        }

        public int GetNumberOfPropertiesOwnedInGroup(PropertyGroup group)
        {
            int numProperties = Properties.Count(property => property.Group == group);
            return numProperties;
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
    }
}