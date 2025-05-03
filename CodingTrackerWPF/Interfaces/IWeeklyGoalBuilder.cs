using CodingTrackerWPF.Models;

namespace CodingTrackerWPF.Interfaces;

public interface IWeeklyGoalBuilder
{
    Task<WeeklyGoalModel?> CreateValidatedWeeklyGoalAsync(int id, WeeklyGoalModel? weeklyGoalModel, DateTime? firstDayOfWeek, decimal thisWeekDurationInDecimal, int? numericUpDown);
}
