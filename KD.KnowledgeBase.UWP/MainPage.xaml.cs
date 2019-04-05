using KD.KnowledgeBase.UWP.Models;
using KD.KnowledgeBase.UWP.Providers;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KD.KnowledgeBase.UWP
{
    public sealed partial class MainPage : Page
    {
        private IServiceProvider ServiceProvider { get; }

        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1000, 440);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            this.ServiceProvider = new ServiceProvider("services.json", ApplicationData.Current.LocalFolder);

            App.Current.Suspending += App_Suspending;
        }

        private void ButtonSaveClick(object sender, RoutedEventArgs e)
        {
            // Add new record.
        }

        private void ButtonDeleteClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = this.tableListView.SelectedItem;
            if (selectedItem != null)
            {
                this.tableListView.Items.Remove(selectedItem);
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var services = await this.ServiceProvider.ReadServicesAsync();
                services.ToList().ForEach(model => this.tableListView.Items.Add(model));
            }
            catch (FileNotFoundException ex)
            {
                // TODO: Handle exception
            }
        }

        private async void App_Suspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            var services = this.tableListView.Items.OfType<SingleServiceModel>();
            await this.ServiceProvider.WriteServicesAsync(services);

            deferral.Complete();
        }
    }
}
