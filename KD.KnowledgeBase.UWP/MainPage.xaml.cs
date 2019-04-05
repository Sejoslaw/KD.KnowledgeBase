using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KD.KnowledgeBase.UWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1000, 440);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
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
    }
}
