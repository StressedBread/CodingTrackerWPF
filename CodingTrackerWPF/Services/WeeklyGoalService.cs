using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using Dapper;

namespace CodingTrackerWPF.Services;

public class WeeklyGoalService : IWeeklyGoalService
{
    private QueryService _queryService;
    private DynamicParameters _parameters = new();

    public WeeklyGoalService(QueryService queryService)
    {
        _queryService = queryService;
    }

    public void CreateWeeklyGoalTable()
    {
        var query = QueryStore.CreateWeeklyGoalTable;
        _queryService.ExecuteQuery(query);
    }

    public void InsertFirstRow()
    {
        var query = QueryStore.InsertFirstRow;
        _queryService.ExecuteQuery(query);
    }

    public async Task<int> GetRowCountWeeklyGoal()
    {
        var query = QueryStore.GetRowCountWeeklyGoal;
        return await _queryService.QueryAsync<int>(query);
    }

    public void SetWeeklyGoal(WeeklyGoalModel weeklyGoal)
    {
        var query = QueryStore.UpdateWeeklyGoal;

        _parameters.Add("@id", weeklyGoal.Id);
        _parameters.Add("@GoalTime", weeklyGoal.Goal);
        _parameters.Add("@LeftCodingTime", weeklyGoal.LeftTime);
        _parameters.Add("@CodedThisWeek", weeklyGoal.CodedThisWeek);

        _queryService.ExecuteQuery(query, _parameters);
    }
}
