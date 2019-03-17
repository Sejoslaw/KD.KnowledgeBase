using System.Collections.Generic;

namespace KD.KnowledgeBase
{
    class Rule : IRule
    {
        public IEnumerable<string> Inputs { get; } = new List<string>();
        public string Output { get; }

        public Rule(string rule)
        {
            var parts = rule.Split("->");
            (this.Inputs as List<string>).AddRange(parts[0].Split(","));
            this.Output = parts[1];
        }

        public override string ToString()
        {
            return $"{ string.Join(",", this.Inputs) }->{ this.Output }";
        }
    }
}
