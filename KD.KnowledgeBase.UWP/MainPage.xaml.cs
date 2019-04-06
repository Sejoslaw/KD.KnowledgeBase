using KD.KnowledgeBase.UWP.Databases;
using KD.KnowledgeBase.UWP.Models;
using KD.KnowledgeBase.UWP.Providers;
using KD.KnowledgeBase.UWP.ViewModels;
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
        private RulesDbContext Context { get; }
        private SingleServiceViewModel ViewModel { get; }

        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1000, 440);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            App.Current.Suspending += App_Suspending;

            this.Context = new RulesDbContext();
            this.ViewModel = new SingleServiceViewModel(new ServiceProvider("services.json", ApplicationData.Current.LocalFolder), this.Context);
            this.DataContext = this.ViewModel;

            this.FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            this.Context.Prices.ToList().ForEach(x => this.CB_Price.Items.Add(x));
            this.Context.Hairdressers.ToList().ForEach(x => this.CB_Hairdresser.Items.Add(x));
            this.Context.Salons.ToList().ForEach(x => this.CB_Salon.Items.Add(x));
            this.Context.Services.ToList().ForEach(x => this.CB_Service.Items.Add(x));
        }

        private void ButtonSaveClick(object sender, RoutedEventArgs e)
        {
            var model = new SingleServiceModel
            {
                Cost = this.CB_Price?.SelectedItem?.ToString(),
                Date = this.DP_Date.Date.Date,
                Hairdresser = this.CB_Hairdresser?.SelectedItem?.ToString(),
                Salon = this.CB_Salon?.SelectedItem?.ToString(),
                Service = this.CB_Service?.SelectedItem?.ToString()
            };

            this.ViewModel.Add(model);
        }

        private void ButtonDeleteClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Delete(this.tableListView.SelectedItem);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await this.ViewModel.LoadAsync();
        }

        private async void App_Suspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await this.ViewModel.SaveAsync();
            deferral.Complete();
        }
    }
}
