using System.Collections.Generic;

namespace ScrumPocker.Core.Models
{
    public class VotingDefinition
    {
        public VotingDefinition()
        {
            Values = new List<string>();
        }
        public string Name { get; set; }
        public List<string> Values { get; set; }
    }
}
