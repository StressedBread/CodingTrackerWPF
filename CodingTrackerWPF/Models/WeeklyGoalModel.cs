namespace CodingTrackerWPF.Models;

public class WeeklyGoalModel
{
    public Int32 Id { get; set; }
    public TimeSpan Goal { get; set; }
    public TimeSpan LeftTime { get; set; }
    public TimeSpan CodedThisWeek { get; set; }

    public WeeklyGoalModel(Int32 id) 
    {
        Id = id;
    }

    public WeeklyGoalModel(TimeSpan goalTime)
    {
        Goal = goalTime;
    }

    public WeeklyGoalModel(Int32 id, TimeSpan goalTime, TimeSpan leftCodingTime, TimeSpan codedThisWeek)
    {
        Id = id;
        Goal = goalTime;
        LeftTime = leftCodingTime;
        CodedThisWeek = codedThisWeek;
    }
}
