using CodingTrackerWPF.Models;

namespace CodingTrackerWPF.Interfaces;

public interface ICodingSessionBuilder
{
    Task<CodingSession?> CreateValidatedSessionAsync(DateTime? startDateTime, DateTime? endDateTime);
}
