using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Kernel.Sketches;
using SkiaSharp;

namespace CodingTrackerWPF.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    public List<CodingSession> CodingSessions = [];

    public ISeries[] Series { get; set; } = Array.Empty<ISeries>();
    public ICartesianAxis[] XAxes { get; set; } = Array.Empty<ICartesianAxis>();
    public ICartesianAxis[] YAxes { get; set; } = Array.Empty<ICartesianAxis>();

    public IRelayCommand? ShowTextBox { get; }

    private readonly IWeeklyGoalService _weeklyGoalService;
    private readonly IWeeklyGoalDialogService _weeklyGoalDialogService;
    private readonly IWeeklyGoalBuilder _weeklyGoalBuilder;
    private readonly ICodingSessionService _codingSessionService;
    private readonly ICodingStatsService _codingStatsService;

    [ObservableProperty]
    private bool isTextBoxVisible;
    [ObservableProperty]
    private string weeklyGoalText = String.Empty;
    [ObservableProperty]
    private string weeklyGoalStats = "";
    [ObservableProperty]
    private int numericUpDownValue = 0;

    [ObservableProperty]
    private TimeSpan totalTimeCoded;
    [ObservableProperty]
    private int numberOfTotalSessions;
    [ObservableProperty]
    private TimeSpan averageSessionDuration;
    [ObservableProperty]
    private TimeSpan longestSessionDuration;
    [ObservableProperty]
    private TimeSpan shortestSessionDuration;
    [ObservableProperty]
    private TimeSpan totalTimeCodedToday;
    [ObservableProperty]
    private TimeSpan totalTimeCodedThisWeek;
    [ObservableProperty]
    private TimeSpan totalTimeCodedThisMonth;
    [ObservableProperty]
    private TimeSpan averageDailyTimeThisWeek;

    [ObservableProperty]
    private int progressBarValue = 0;

    private readonly string[] weeklyGoalStatsArray = new string[3];


    public HomeViewModel(IWeeklyGoalService weeklyGoalService, IWeeklyGoalDialogService weeklyGoalDialogService,
        IWeeklyGoalBuilder weeklyGoalBuilder, ICodingSessionService codingSessionService, ICodingStatsService codingStatsService)
    {
        _weeklyGoalService = weeklyGoalService;
        _weeklyGoalDialogService = weeklyGoalDialogService;
        _weeklyGoalBuilder = weeklyGoalBuilder;
        _codingSessionService = codingSessionService;
        _codingStatsService = codingStatsService;

        ShowTextBox = new AsyncRelayCommand(SetWeeklyGoal);
    }

    public async Task LoadSessionsAsync()
    {
        var sessions = await _codingSessionService.ViewSessionsAsync();
        if (sessions != null)
        {
            CodingSessions = new List<CodingSession>(sessions);

            TotalTimeCoded = _codingStatsService.GetTotalTimeCoded(CodingSessions);
            NumberOfTotalSessions = _codingStatsService.GetNumberOfTotalSessions(CodingSessions);
            AverageSessionDuration = _codingStatsService.GetAverageSessionDuration(CodingSessions);
            LongestSessionDuration = _codingStatsService.GetLongestSessionDuration(CodingSessions);
            ShortestSessionDuration = _codingStatsService.GetShortestSessionDuration(CodingSessions);
            TotalTimeCodedToday = _codingStatsService.GetTotalTimeCodedToday(CodingSessions);
            TotalTimeCodedThisWeek = _codingStatsService.GetTotalTimeCodedThisWeek(CodingSessions);
            TotalTimeCodedThisMonth = _codingStatsService.GetTotalTimeCodedThisMonth(CodingSessions);
            AverageDailyTimeThisWeek = _codingStatsService.GetAverageDailyTimeThisWeek(CodingSessions);

            await GetWeeklyGoalStats();

            AddSeries(_codingStatsService.GetTimeCodedPerDayThisWeek(CodingSessions));
        }
    }

    private async Task GetWeeklyGoalStats()
    {
        int id = 1;

        var weeklyGoal = await _weeklyGoalService.GetWeeklyGoal(id);

        var progressBarTimeSpan = (weeklyGoal?.CodedThisWeek / weeklyGoal?.Goal * 100);

        ProgressBarValue = (int)(progressBarTimeSpan ?? 0);

        weeklyGoalStatsArray[0] = weeklyGoal?.Goal.ToString() ?? "0";
        weeklyGoalStatsArray[1] = weeklyGoal?.LeftTime.ToString() ?? "0";
        weeklyGoalStatsArray[2] = weeklyGoal?.CodedThisWeek.ToString() ?? "0";

        WeeklyGoalStats = $"Weekly Goal: {weeklyGoalStatsArray[0]} Time Left: {weeklyGoalStatsArray[1]}";
    }

    public async Task SetWeeklyGoal()
    {
        var id = 1;

        var weeklyGoal = await _weeklyGoalService.GetWeeklyGoal(id);
        var firstDayOfWeek = _weeklyGoalService.GetFirstDayOfWeek(DateTime.Now);
        var thisWeekDurationInDecimal = _weeklyGoalService.GetThisWeekDuration(firstDayOfWeek, DateTime.Now);

        var newWeeklyGoal = await _weeklyGoalDialogService.GetWeeklyGoal();

        var weeklyGoalModel = await _weeklyGoalBuilder.CreateValidatedWeeklyGoalAsync(id, weeklyGoal, firstDayOfWeek, thisWeekDurationInDecimal, newWeeklyGoal);

        if (weeklyGoalModel == null) return;

        _weeklyGoalService.SetWeeklyGoal(weeklyGoalModel);
        await GetWeeklyGoalStats();
    }

    public void AddSeries(List<TimeSpan> timeCodedPerDayThisWeek)
    {
        var values = timeCodedPerDayThisWeek.Select(x => (double)x.TotalHours).ToArray();
        var labels = timeCodedPerDayThisWeek
                        .Select(ts => $"{(int)ts.TotalHours}h {ts.Minutes}m")
                        .ToArray();

        Series =
        [
            new ColumnSeries<double>
            {
                Values = values,
                Name = $"Hours: "
            }
        ];

        XAxes = 
        [
            new Axis
            {
                Name = "Days in week",
                Labels = new[]
                {
                    "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"
                },
                TextSize = 12,
                Position = LiveChartsCore.Measure.AxisPosition.Start,
                LabelsPaint = new SolidColorPaint(SKColors.White),
                NamePaint = new SolidColorPaint(SKColors.White)
            }
        ];

        YAxes =
        [
            new Axis
            {
                Labeler = value =>
                {
                    var ts = TimeSpan.FromHours(value);
                    return $"{(int)ts.TotalHours}h {ts.Minutes}m";
                },

                TextSize = 12,
                Position = LiveChartsCore.Measure.AxisPosition.Start,
                LabelsPaint = new SolidColorPaint(SKColors.White),
                NamePaint = new SolidColorPaint(SKColors.White)
            }
        ];

        OnPropertyChanged(nameof(Series));
        OnPropertyChanged(nameof(XAxes));
        OnPropertyChanged(nameof(YAxes));
    }
}
