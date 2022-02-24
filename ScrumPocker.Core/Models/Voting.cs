using System.Collections.Generic;

namespace ScrumPocker.Core.Models
{
    public class Voting
    {
        public Voting()
        {
            Values = new List<string>();
        }
        public string Name { get; set; }
        public List<string> Values { get; set; }
    }
}
