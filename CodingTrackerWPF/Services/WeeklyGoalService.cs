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

    public async Task<WeeklyGoalModel?> GetWeeklyGoal(Int32 id)
    {
        var query = QueryStore.GetWeeklyGoal;

        _parameters.Add("@id", id);

        return await _queryService.QueryAsync<WeeklyGoalModel>(query, _parameters);
    }

    public DateTime GetFirstDayOfWeek(DateTime currentDay)
    {
        var query = QueryStore.GetFirstDayOfWeek;

        _parameters.Add("@currentDay", currentDay);

        return _queryService.Query<DateTime>(query, _parameters);
    }

    public DateTime GetLastDayOfWeek()
    {
        var query = QueryStore.GetLastDayOfWeek;
        return _queryService.Query<DateTime>(query);
    }

    public Decimal GetThisWeekDuration(DateTime firstDay, DateTime endDay)
    {
        var query = QueryStore.GetThisWeekDuration;
        
        _parameters.Add("@startDay", firstDay);
        _parameters.Add("@endDay", endDay);

        return _queryService.Query<Decimal>(query, _parameters);
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
