using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CodingTrackerWPF.ViewModels;
using CodingTrackerWPF.Views;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;

namespace CodingTrackerWPF.Services;

class WeeklyGoalDialogService : IWeeklyGoalDialogService
{
    public async Task<int?> GetWeeklyGoal()
    {
        var result = await OpenDialogAsync();
        if (result is not WeeklyGoalModel weeklyGoalModel) return null;
        return (int)weeklyGoalModel.Goal.TotalHours;
    }

    public async Task<object?> OpenDialogAsync()
    {
        var viewModel = new WeeklyGoalDialogViewModel();

        var dialog = new WeeklyGoalDialogView()
        {
            DataContext = viewModel
        };

        var wrapper = new ContentControl { Content = dialog };

        return await DialogHost.Show(wrapper, "HomeRootDialog");
    }
}
