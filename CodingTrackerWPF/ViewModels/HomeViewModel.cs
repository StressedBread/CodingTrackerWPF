using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CodingTrackerWPF.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace CodingTrackerWPF.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    public IRelayCommand? ShowTextBox { get; }

    private readonly IWeeklyGoalService _weeklyGoalService;
    private readonly IWeeklyGoalDialogService _weeklyGoalDialogService;
    private readonly IWeeklyGoalBuilder _weeklyGoalBuilder;

    [ObservableProperty]
    private bool isTextBoxVisible;
    [ObservableProperty]
    private string weeklyGoalText = String.Empty;
    [ObservableProperty]
    private string weeklyGoalStats = "";
    [ObservableProperty]
    private int numericUpDownValue = 0;

    private readonly string[] weeklyGoalStatsArray = new string[3];
    

    public HomeViewModel(IWeeklyGoalService weeklyGoalService, IWeeklyGoalDialogService weeklyGoalDialogService, IWeeklyGoalBuilder weeklyGoalBuilder)
    {
        _weeklyGoalService = weeklyGoalService;
        _weeklyGoalDialogService = weeklyGoalDialogService;
        _weeklyGoalBuilder = weeklyGoalBuilder;

        ShowTextBox = new AsyncRelayCommand(SetWeeklyGoal);
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

    public async Task SetWeeklyGoal()
    {
        var id = 1;

        var weeklyGoal = await _weeklyGoalService.GetWeeklyGoal(id);
        var firstDayOfWeek = _weeklyGoalService.GetFirstDayOfWeek(DateTime.Now);
        var thisWeekDurationInDecimal = _weeklyGoalService.GetThisWeekDuration(firstDayOfWeek, DateTime.Now);

        var newWeeklyGoal = await _weeklyGoalDialogService.GetWeeklyGoal();

        var weeklyGoalModel = await _weeklyGoalBuilder.CreateValidatedWeeklyGoalAsync(id, weeklyGoal, firstDayOfWeek, thisWeekDurationInDecimal, newWeeklyGoal);

        if (weeklyGoalModel == null) return;

        _weeklyGoalService.SetWeeklyGoal(weeklyGoalModel);
        await GetWeeklyGoalStats();
    }
}
