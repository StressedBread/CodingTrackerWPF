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

    public HomeViewModel(IWeeklyGoalService weeklyGoalService)
    {
        _weeklyGoalService = weeklyGoalService;

        ShowTextBox = new RelayCommand(SetWeeklyGoal);
        SubmitWeeklyGoalCommand = new AsyncRelayCommand(SubmitWeeklyGoalAsync);
    }

    private async Task SubmitWeeklyGoalAsync()
    {
        IsTextBoxVisible = false;

        var id = 1;

        var weeklyGoal = await _weeklyGoalService.GetWeeklyGoal(id);
        var firstDayOfWeek = _weeklyGoalService.GetFirstDayOfWeek(DateTime.Now);
        var thisWeekDurationInDecimal = _weeklyGoalService.GetThisWeekDuration(firstDayOfWeek, DateTime.Now);

        TimeSpan thisWeekDuration = TimeSpan.FromSeconds((double)thisWeekDurationInDecimal);

        if (weeklyGoal != null) id = weeklyGoal.Id;

        var goalTime = TimeSpan.FromHours(Double.Parse(WeeklyGoalText));

        var timeLeft = goalTime - thisWeekDuration;

        var weeklyGoalModel = new WeeklyGoalModel(id, goalTime, timeLeft, thisWeekDuration);
    }

    public Visibility TextBoxVisibility => IsTextBoxVisible ? Visibility.Visible : Visibility.Collapsed;

    public void SetWeeklyGoal()
    {
        IsTextBoxVisible = true;

        OnPropertyChanged(nameof(TextBoxVisibility));
    }
}
