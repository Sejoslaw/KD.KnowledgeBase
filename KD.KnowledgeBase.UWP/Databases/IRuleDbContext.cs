using KD.KnowledgeBase.UWP.Models;

namespace KD.KnowledgeBase.UWP.Databases
{
    public interface IRuleDbContext
    {
        bool RuleExists(SingleServiceModel model);
    }
}
