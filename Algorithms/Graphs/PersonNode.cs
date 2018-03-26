using System.Collections.Generic;

namespace Algorithms.Graphs
{
    public class PersonNode
    {
        public PersonNode(string name, List<string> friends)
        {
            Name = name;
            Friends = friends;
            Visited = false;
        }
        public string Name { get; set; }
        public List<string> Friends { get; set; }
        public bool Visited { get; set; }
    }
}