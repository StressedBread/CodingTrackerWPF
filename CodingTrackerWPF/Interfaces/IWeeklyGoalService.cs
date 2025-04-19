using CodingTrackerWPF.Models;

namespace CodingTrackerWPF.Interfaces;

public interface IWeeklyGoalService
{
    void CreateWeeklyGoalTable();
    void InsertFirstRow();
    Task<int> GetRowCountWeeklyGoal();
    void SetWeeklyGoal(WeeklyGoalModel weeklyGoalModel);
    Task<WeeklyGoalModel?> GetWeeklyGoal(Int32 id);
    DateTime GetFirstDayOfWeek(DateTime currentDay);
    DateTime GetLastDayOfWeek();
    Decimal GetThisWeekDuration(DateTime firstDay, DateTime endDay);
}
