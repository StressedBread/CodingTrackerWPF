using CodingTrackerWPF.ViewModels;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodingTrackerWPF.Views;

/// <summary>
/// Interaction logic for HomeView.xaml
/// </summary>
public partial class HomeView : UserControl
{    
    public HomeView()
    {
        InitializeComponent();

        DataContext = new HomeViewModel();
    }

    private void WeeklyGoalInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
    }
}
