using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace CodingTrackerWPF.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    public IRelayCommand? ShowTextBox { get; }
    public IAsyncRelayCommand? SubmitWeeklyGoalCommand { get; }

    private IWeeklyGoalService _weeklyGoalService;

    [ObservableProperty]
    private bool isTextBoxVisible;
    [ObservableProperty]
    private string weeklyGoalText = String.Empty;
    [ObservableProperty]
    private string weeklyGoalStats = "";
    [ObservableProperty]
    private int numericUpDownValue = 0;

    private string[] weeklyGoalStatsArray = new string[3];
    

    public HomeViewModel(IWeeklyGoalService weeklyGoalService)
    {
        _weeklyGoalService = weeklyGoalService;

        ShowTextBox = new RelayCommand(SetWeeklyGoal);
        SubmitWeeklyGoalCommand = new AsyncRelayCommand(SubmitWeeklyGoalAsync);
    }

    private async Task SubmitWeeklyGoalAsync()
    {
        var id = 1;

        var weeklyGoal = await _weeklyGoalService.GetWeeklyGoal(id);
        var firstDayOfWeek = _weeklyGoalService.GetFirstDayOfWeek(DateTime.Now);
        var thisWeekDurationInDecimal = _weeklyGoalService.GetThisWeekDuration(firstDayOfWeek, DateTime.Now);

        TimeSpan thisWeekDuration = TimeSpan.FromSeconds((double)thisWeekDurationInDecimal);

        if (weeklyGoal != null) id = weeklyGoal.Id;

        var goalTime = TimeSpan.FromHours((double)NumericUpDownValue);

        var timeLeft = goalTime - thisWeekDuration;
        timeLeft = timeLeft < TimeSpan.Zero ? TimeSpan.Zero : timeLeft;

        var weeklyGoalModel = new WeeklyGoalModel(id, goalTime, timeLeft, thisWeekDuration);

        _weeklyGoalService.SetWeeklyGoal(weeklyGoalModel);

        IsTextBoxVisible = false;
        OnPropertyChanged(nameof(TextBoxVisibility));

        await GetWeeklyGoalStats();
    }

    private async Task GetWeeklyGoalStats()
    {
        int id = 1;

        var weeklyGoal = await _weeklyGoalService.GetWeeklyGoal(id);

        weeklyGoalStatsArray[0] = weeklyGoal?.Goal.ToString() ?? "0";
        weeklyGoalStatsArray[1] = weeklyGoal?.LeftTime.ToString() ?? "0";
        weeklyGoalStatsArray[2] = weeklyGoal?.CodedThisWeek.ToString() ?? "0";

        WeeklyGoalStats = $"Weekly Goal: {weeklyGoalStatsArray[0]} Left Time: {weeklyGoalStatsArray[1]} Coded This Week: {weeklyGoalStatsArray[2]}";
    }

    public Visibility TextBoxVisibility => IsTextBoxVisible ? Visibility.Visible : Visibility.Collapsed;

    public void SetWeeklyGoal()
    {
        IsTextBoxVisible = true;

        OnPropertyChanged(nameof(TextBoxVisibility));
    }
}
