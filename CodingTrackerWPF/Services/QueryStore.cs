namespace CodingTrackerWPF.Services;

internal class QueryStore
{
    public const string CreateTable = @"
        CREATE TABLE IF NOT EXISTS CodingSessions (
            ID INTEGER PRIMARY KEY AUTOINCREMENT,
            StartDateTime DATETIME NOT NULL,
            EndDateTime DATETIME NOT NULL,
            Duration TIME NOT NULL
        )";
}
