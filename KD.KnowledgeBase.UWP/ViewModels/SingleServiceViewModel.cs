using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KD.KnowledgeBase.UWP.Databases;
using KD.KnowledgeBase.UWP.Models;
using KD.KnowledgeBase.UWP.Providers;

namespace KD.KnowledgeBase.UWP.ViewModels
{
    public class SingleServiceViewModel
    {
        private IServiceProvider ServiceProvider { get; }
        private RulesDbContext Context { get; }

        public ICollection<SavedServiceModel> Items { get; }

        public SingleServiceViewModel(IServiceProvider serviceProvider, RulesDbContext context)
        {
            this.ServiceProvider = serviceProvider;
            this.Context = context;
            this.Items = new ObservableCollection<SavedServiceModel>();
        }

        public async Task LoadAsync()
        {
            try
            {
                this.Items.Clear();

                var services = await this.ServiceProvider.ReadServicesAsync();
                services.ToList().ForEach(model => this.Items.Add(model));
            }
            catch (FileNotFoundException ex)
            {
                // TODO: Handle exception
            }
        }

        public async Task SaveAsync()
        {
            await this.ServiceProvider.WriteServicesAsync(this.Items);
        }

        public void Add(SavedServiceModel model)
        {
            this.Items.Add(model);
        }

        public void Delete(object selectedItem)
        {
            if (selectedItem != null && selectedItem is SavedServiceModel)
            {
                this.Items.Remove(selectedItem as SavedServiceModel);
            }
        }
    }
}
