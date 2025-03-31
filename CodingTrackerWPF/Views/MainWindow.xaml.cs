using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodingTrackerWPF.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainContent.Content = new HomeView();
    }    
    private void OnDrawerItemClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            switch (button.Name)
            {
                case "Home":
                    MainContent.Content = new HomeView();
                    break;
                case "CodingSessionsButton":
                    MainContent.Content = new CodingSessionsView();
                    break;
                /*case "SettingsButton":
                    MainContent.Content = new SettingsView();
                    break;*/
                default:
                    break;
            }
        }
    }
}