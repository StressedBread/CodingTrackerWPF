using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Services;
using CodingTrackerWPF.ViewModels;
using System.Windows.Controls;

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
        IWeeklyGoalDialogService weeklyGoalDialogService = new WeeklyGoalDialogService();
        IWeeklyGoalBuilder weeklyGoalBuilder = new WeeklyGoalBuilder();
        ICodingSessionService codingSessionService = new CodingSessionService(queryService);
        ICodingStatsService codingStatsService = new CodingStatsService();

        DataContext = new HomeViewModel(weeklyGoalService, weeklyGoalDialogService, weeklyGoalBuilder, codingSessionService, codingStatsService);
    }

    private async void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (DataContext is HomeViewModel viewModel)
        {
            await viewModel.LoadSessionsAsync();
        }
    }
}
