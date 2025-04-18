namespace CodingTrackerWPF.Models;

public class WeeklyGoalModel
{
    public Int32 Id { get; set; }
    public TimeSpan Goal { get; set; }
    public TimeSpan LeftTime { get; set; }
    public TimeSpan CodedThisWeek { get; set; }

    public WeeklyGoalModel(Int32 id, TimeSpan goal, TimeSpan leftTime, TimeSpan codedThisWeek)
    {
        Id = id;
        Goal = goal;
        LeftTime = leftTime;
        CodedThisWeek = codedThisWeek;
    }
}
