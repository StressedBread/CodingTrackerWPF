using CodingTrackerWPF.Models;

namespace CodingTrackerWPF.Interfaces;

public interface IWeeklyGoalService
{
    void CreateWeeklyGoalTable();
    void InsertFirstRow();
    Task<int> GetRowCountWeeklyGoal();
    void SetWeeklyGoal(WeeklyGoalModel weeklyGoalModel);
}
