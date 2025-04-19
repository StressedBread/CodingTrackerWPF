using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Services;
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

        var queryService = new QueryService();

        IWeeklyGoalService weeklyGoalService = new WeeklyGoalService(queryService);

        DataContext = new HomeViewModel(weeklyGoalService);
    }

    private void WeeklyGoalInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
    }
}
