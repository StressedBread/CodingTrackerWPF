namespace CodingTrackerWPF.Services;

public static class QueryStore
{
    public const string CreateTable = @"
            CREATE TABLE IF NOT EXISTS CodingSessions (
            ID INTEGER PRIMARY KEY AUTO_INCREMENT,
            StartDateTime DATETIME NOT NULL,
            EndDateTime DATETIME NOT NULL,
            Duration TIME NOT NULL
        )";

    public const string GetAllSessions = @"
        SELECT * FROM CodingSessions";

    public const string AddSession = @"
            INSERT INTO CodingSessions (StartDateTime, EndDateTime, Duration)
            VALUES (@StartTime, @EndTime, @Duration)";

    public const string UpdateStartTime = @"
            UPDATE CodingSessions 
            SET StartDateTime = @StartTime, Duration = @Duration
            WHERE ID = @id";

    public const string UpdateEndTime = @"
            UPDATE CodingSessions
            SET EndDateTime = @EndTime, Duration = @Duration
            WHERE ID = @id";
}
