using KD.KnowledgeBase.UWP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KD.KnowledgeBase.UWP.Providers
{
    public interface IServiceProvider
    {
        Task<IEnumerable<SingleServiceModel>> ReadServicesAsync();
        Task WriteServicesAsync(IEnumerable<SingleServiceModel> services);
    }
}
