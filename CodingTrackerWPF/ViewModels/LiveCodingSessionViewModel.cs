using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows.Threading;
using System.Timers;

namespace CodingTrackerWPF.ViewModels;

public partial class LiveCodingSessionViewModel : ObservableObject
{
    private readonly ICodingSessionService _codingSessionService;

    [ObservableProperty]
    private string sessionTimer = "00:00:00";

    public IRelayCommand? StartSessionCommand { get; }
    public IRelayCommand? StopSessionCommand { get; }

    private TimeSpan _timeElapsed = TimeSpan.Zero;
    private readonly Stopwatch _stopWatch = new();

    private DateTime _startTime;
    private DateTime _endTime;

    private readonly DispatcherTimer _dispatcherTimer = new();

    private readonly System.Timers.Timer _timer = new();

    public LiveCodingSessionViewModel(ICodingSessionService codingSessionService)
    {
        _codingSessionService = codingSessionService;

        StartSessionCommand = new RelayCommand(StartSession);
        StopSessionCommand = new RelayCommand(StopSession);

        //_dispatcherTimer.Tick += DispatcherTimer_Tick;
        _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);

        _timer.Elapsed += Timer_Elapsed;
        _timer.Interval = 100;
        _timer.AutoReset = true;
    }

    

    private void StartSession()
    {
        if (!_stopWatch.IsRunning)
        {
            _stopWatch.Start();
            
            _timer.Start();

            _startTime = DateTime.Now;
            _timeElapsed = _stopWatch.Elapsed;
        }
    }

    private void StopSession()
    {
        if (_stopWatch.IsRunning)
        {
            _stopWatch.Stop();
            _timer.Stop();

            _timeElapsed = _stopWatch.Elapsed;
            _endTime = DateTime.Now;

            var codingSesion = new CodingSession(_startTime, _endTime, _timeElapsed);

            _codingSessionService.AddSession(codingSesion);

            _stopWatch.Reset();

            _timeElapsed = TimeSpan.Zero;
            SessionTimer = "00:00:00";
        }
    }

    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        _timeElapsed = _stopWatch.Elapsed;
        SessionTimer = _timeElapsed.ToString(@"hh\:mm\:ss");
    }

    private int _lastDisplayedSeconds = -1;

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        var elapsed = _stopWatch.Elapsed;

        if (elapsed.Seconds != _lastDisplayedSeconds)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                SessionTimer = elapsed.ToString(@"hh\:mm\:ss");
            });

            _lastDisplayedSeconds = elapsed.Seconds;
        }
    }
}
