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
            this.CB_Location.Items.Add("Wola");
            this.CB_Location.Items.Add("Centrum");
        }

        private void ButtonSaveClick(object sender, RoutedEventArgs e)
        {
            var model = new SavedServiceModel
            {
                Location = this.CB_Location?.SelectedItem?.ToString(),
                Salon = this.CB_Salon?.SelectedItem?.ToString(),
                Hairdresser = this.CB_Hairdresser?.SelectedItem?.ToString(),
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

        private void CB_Location_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.CB_Salon.SelectedItem = null;
            this.CB_Salon.Items.Clear();

            this.Context.Database
                .Where(model => model.Location.Equals(this.CB_Location.SelectedItem?.ToString()))
                .Select(model => model.Salon)
                .Distinct()
                .ToList()
                .ForEach(salon => this.CB_Salon.Items.Add(salon));

            this.CB_Salon_SelectionChanged(sender, e);
        }

        private void CB_Salon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.CB_Hairdresser.SelectedItem = null;
            this.CB_Hairdresser.Items.Clear();

            this.Context.Database
                .Where(model => model.Location.Equals(this.CB_Location.SelectedItem?.ToString()) && model.Salon.Equals(this.CB_Salon.SelectedItem?.ToString()))
                .Select(model => model.Hairdressers)
                .SelectMany(model => model.ToList())
                .Select(model => model.Name)
                .ToList()
                .ForEach(model => this.CB_Hairdresser.Items.Add(model));

            this.CB_Hairdresser_SelectionChanged(sender, e);
        }

        private void CB_Hairdresser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.CB_Service.SelectedItem = null;
            this.CB_Service.Items.Clear();

            this.Context.Database
                .Where(model => model.Location.Equals(this.CB_Location.SelectedItem?.ToString()) && model.Salon.Equals(this.CB_Salon.SelectedItem?.ToString()))
                .Select(model => model.Hairdressers)
                .SelectMany(model => model.ToList())
                .Where(model => model.Name.Equals(this.CB_Hairdresser.SelectedItem?.ToString()))
                .SelectMany(model => model.Services)
                .ToList()
                .ForEach(model => this.CB_Service.Items.Add(model));
        }
    }
}
