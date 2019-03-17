using System.Collections.Generic;

namespace KD.KnowledgeBase
{
    interface IRule
    {
        IEnumerable<string> Inputs { get; }
        string Output { get; }
    }
}
