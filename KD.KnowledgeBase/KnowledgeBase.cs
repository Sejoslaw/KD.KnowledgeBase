using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KD.KnowledgeBase
{
    class KnowledgeBase : IKnowledgeBase
    {
        private List<string> Facts { get; } = new List<string>();
        private List<IRule> Rules { get; } = new List<IRule>();

        private KnowledgeBase(string baseFacts, IEnumerable<string> rules)
        {
            this.Facts.AddRange(baseFacts.Split(','));

            rules.ToList().ForEach(rule => this.Rules.Add(new Rule(rule)));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Knowledge Base [Facts: [{ string.Join(",", this.Facts) }]; Unprocessed Rules: [{ string.Join(";", this.Rules) }]];");
            return sb.ToString();
        }

        public void SortFacts()
        {
            this.Facts.Sort();
        }

        public void Process()
        {
            int loopCount = -1;
            do
            {
                foreach (IRule rule in this.Rules)
                {
                    if (this.ProcessRule(rule))
                    {
                        loopCount = 0;
                        this.Facts.Add(rule.Output);
                        this.Rules.Remove(rule);
                        break;
                    }
                    loopCount++;
                }

                if (loopCount > 0)
                {
                    return;
                }
            } while (this.Rules.Count > 0);
        }

        private bool ProcessRule(IRule rule)
        {
            foreach (var input in rule.Inputs)
            {
                if (!this.Facts.Contains(input))
                {
                    return false;
                }
            }
            return true;
        }

        internal static IKnowledgeBase New(string baseFacts, IEnumerable<string> rules)
        {
            return new KnowledgeBase(baseFacts, rules);
        }
    }
}
