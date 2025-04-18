namespace CodingTrackerWPF.Services;

public static class QueryStore
{
    public const string CreateCodingSessionsTable = @"
            CREATE TABLE IF NOT EXISTS CodingSessions (
            ID INTEGER PRIMARY KEY AUTO_INCREMENT,
            StartDateTime DATETIME NOT NULL,
            EndDateTime DATETIME NOT NULL,
            Duration TIME NOT NULL
        )";

    public const string CreateWeeklyGoalTable = @"
            CREATE TABLE IF NOT EXISTS WeeklyGoal (
            ID INTEGER PRIMARY KEY AUTO_INCREMENT,
            GoalTime TIME NOT NULL,
            LeftCodingTime TIME NOT NULL,
            CodedThisWeek TIME NOT NULL
        )";

    public const string GetRowCountWeeklyGoal = @"
            SELECT COUNT(*) FROM WeeklyGoal";

    public const string InsertFirstRow = @"
            INSERT INTO WeeklyGoal (GoalTime, LeftCodingTime, CodedThisWeek)
            SELECT 0, 0, 0 
            FROM DUAL
            WHERE NOT EXISTS (SELECT 1 FROM WeeklyGoal)";

    public const string SetWeeklyGoal = @"
            UPDATE WeeklyGoal
            SET GoalTime = @GoalTime, LeftCodingTime = @LeftCodingTime, CodedThisWeek = @CodedThisWeek
            WHERE ID = @id";

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

    public const string DeleteSession = @"
            DELETE FROM CodingSessions
            WHERE ID = @id";

    public const string UpdateWeeklyGoal = @"
            UPDATE WeeklyGoal
            SET GoalTime = @GoalTime
            WHERE ID = @id";
}
