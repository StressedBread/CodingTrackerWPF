namespace CodingTrackerWPF.Interfaces;

public interface IWeeklyGoalDialogService
{
    Task<int?> GetWeeklyGoal();
    Task<object?> OpenDialogAsync();
}
