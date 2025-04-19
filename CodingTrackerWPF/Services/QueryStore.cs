namespace CodingTrackerWPF.Services;

public static class QueryStore
{
    #region CodingSession
    public const string CreateCodingSessionsTable = @"
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

    public const string DeleteSession = @"
            DELETE FROM CodingSessions
            WHERE ID = @id";
    #endregion

    #region WeeklyGoal
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

    public const string GetWeeklyGoal = @"
            SELECT * FROM WeeklyGoal
            WHERE ID = @id";

    public const string SetWeeklyGoal = @"
            UPDATE WeeklyGoal
            SET GoalTime = @GoalTime, LeftCodingTime = @LeftCodingTime, CodedThisWeek = @CodedThisWeek
            WHERE ID = @id";

    public const string UpdateWeeklyGoal = @"
            UPDATE WeeklyGoal
            SET GoalTime = @GoalTime, LeftCodingTime = @LeftCodingTime, CodedThisWeek = @CodedThisWeek
            WHERE ID = @id";

    public const string GetFirstDayOfWeek = @"
            SELECT DATE(DATE_SUB(@currentDay, INTERVAL WEEKDAY(@currentDay) DAY))
            AS FirstDay";

    public const string GetLastDayOfWeek = @"
            SELECT DATE_ADD(@lastDay, INTERVAL (6 - WEEKDAY(@lastDay)) DAY)
            AS LastDay";

    public const string GetThisWeekDuration = @"
            SELECT SUM(TIME_TO_SEC(Duration)) AS TotalDuration
            FROM CodingSessions
            WHERE StartDateTime BETWEEN @startDay AND @endDay";
    #endregion
}
