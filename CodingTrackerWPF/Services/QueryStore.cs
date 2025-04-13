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
}
