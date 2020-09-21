using MonopolyKata.Spaces;
using System.Collections.Generic;

namespace MonopolyKata
{
    public class PropertyGroup
    {
        public string Name { get; set; }
        public List<Property> Properties { get; set; }

        public PropertyGroup(string name)
        {
            Name = name;
            Properties = new List<Property>();
        }

        public void AddProperty(Property property)
        {
            Properties.Add(property);
        }

        public int GetNumberOfProperties()
            => Properties.Count;
    }
}