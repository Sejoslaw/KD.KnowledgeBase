using KD.KnowledgeBase.UWP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KD.KnowledgeBase.UWP.Providers
{
    public interface IServiceProvider
    {
        Task<IEnumerable<SavedServiceModel>> ReadServicesAsync();
        Task WriteServicesAsync(IEnumerable<SavedServiceModel> services);
    }
}
