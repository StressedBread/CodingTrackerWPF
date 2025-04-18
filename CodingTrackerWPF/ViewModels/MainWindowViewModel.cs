using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Views;

namespace CodingTrackerWPF.ViewModels;

public class MainWindowViewModel
{
    private ICodingSessionService _codingSessionService;
    private IWeeklyGoalService _weeklyGoalService;

    public MainWindowViewModel(ICodingSessionService codingSessionService, IWeeklyGoalService weeklyGoalService)
    {
        _codingSessionService = codingSessionService;
        _weeklyGoalService = weeklyGoalService;

        _codingSessionService.CreateCodingSessionsTable();
        _weeklyGoalService.CreateWeeklyGoalTable();
    }     

    public async Task InsertFirstRowAsync()
    {
        int count = await _weeklyGoalService.GetRowCountWeeklyGoal();

        if (count == 0)
        {
            _weeklyGoalService.InsertFirstRow();
        }

        else
        {
            var messageDialogView = new MessageDialogView();
            messageDialogView.DataContext = new MessageDialogViewModel("Error", "Weekly Goal table cannot have more than 1 row!");
        }
    }
}