using CodingTrackerWPF.Models;

namespace CodingTrackerWPF.Interfaces;

public interface ICodingSessionService
{
    void CreateCodingSessionsTable();
    void AddSession(CodingSession session);
    Task<List<CodingSession>> ViewSessionsAsync();
    void UpdateStartTime(CodingSession session);
    void UpdateEndTime(CodingSession session);
}
