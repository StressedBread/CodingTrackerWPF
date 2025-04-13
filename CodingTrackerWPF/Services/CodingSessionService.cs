using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using Dapper;

namespace CodingTrackerWPF.Services;

public class CodingSessionService : ICodingSessionService
{
    private QueryService _queryService = new();
    private DynamicParameters _parameters = new();
    public void CreateCodingSessionsTable()
    {
        var query = QueryStore.CreateTable;
        _queryService.ExecuteQuery(query);
    }

    public async Task<List<CodingSession>> ViewSessionsAsync()
    {
        var query = QueryStore.GetAllSessions;

        return await _queryService.ReaderAsync(query);
    }
    public void AddSession(CodingSession session)
    {
        var query = QueryStore.AddSession;

        _parameters.Add("@StartTime", session.StartDateTime);
        _parameters.Add("@EndTime", session.EndDateTime);
        _parameters.Add("@Duration", session.Duration);

        _queryService.ExecuteQuery(query, _parameters);
    }    
}
