using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CodingTrackerWPF.ViewModels;
using CodingTrackerWPF.Views;
using MaterialDesignThemes.Wpf;

namespace CodingTrackerWPF.Services;

public class WeeklyGoalBuilder : IWeeklyGoalBuilder
{
    public async Task<WeeklyGoalModel?> CreateValidatedWeeklyGoalAsync(int id, WeeklyGoalModel? weeklyGoalModel, DateTime? firstDayOfWeek, decimal thisWeekDurationInDecimal, int? numericUpDown)
    {
        TimeSpan thisWeekDuration = TimeSpan.FromSeconds((double)thisWeekDurationInDecimal);

        if (weeklyGoalModel != null) id = weeklyGoalModel.Id;
        if (numericUpDown == null) return null;
        if (numericUpDown < 0 || numericUpDown > 168)
        {
            var messageDialogView = new MessageDialogView
            {
                DataContext = new MessageDialogViewModel("Error", "Invalid weekly goal!")
            };
            await DialogHost.Show(messageDialogView, "HomeRootDialog");
            return null;
        }

        var goalTime = TimeSpan.FromHours((double)numericUpDown);

        var timeLeft = goalTime - thisWeekDuration;
        timeLeft = timeLeft < TimeSpan.Zero ? TimeSpan.Zero : timeLeft;

        return new WeeklyGoalModel(id, goalTime, timeLeft, thisWeekDuration);       
    }
}
