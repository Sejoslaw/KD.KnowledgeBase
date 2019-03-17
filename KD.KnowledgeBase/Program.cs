using System;
using System.Collections.Generic;

namespace KD.KnowledgeBase
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseFacts = "A,C,H,B,E";
            var rules = new List<string>
            {
                "A->D",
                "F,H->G",
                "B->L",
                "D,J->M",
                "C,D->F",
                "A,E->J",
                "Z->X" // Test KnowledgeBase
            };

            IKnowledgeBase knowledgeBase = KnowledgeBase.New(baseFacts, rules);

            knowledgeBase.Process();
            knowledgeBase.SortFacts();

            Console.WriteLine(knowledgeBase);

            Console.ReadKey();
        }
    }
}
