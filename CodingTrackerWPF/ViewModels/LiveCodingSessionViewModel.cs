using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace CodingTrackerWPF.ViewModels;

public partial class LiveCodingSessionViewModel : ObservableObject
{
    private ICodingSessionService _codingSessionService;

    [ObservableProperty]
    private string sessionTimer = "00:00:00";

    public IRelayCommand? StartSessionCommand { get; }
    public IRelayCommand? StopSessionCommand { get; }

    private TimeSpan _timeElapsed = TimeSpan.Zero;
    private Stopwatch _stopWatch = new();

    private DateTime _startTime;
    private DateTime _endTime;

    private DispatcherTimer _dispatcherTimer = new();

    public LiveCodingSessionViewModel(ICodingSessionService codingSessionService)
    {
        _codingSessionService = codingSessionService;

        StartSessionCommand = new RelayCommand(StartSession);
        StopSessionCommand = new RelayCommand(StopSession);

        _dispatcherTimer.Tick += DispatcherTimer_Tick;
    }

    private void StartSession()
    {
        if (!_stopWatch.IsRunning)
        {
            _stopWatch.Start();

            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Start();

            _startTime = DateTime.Now;
        }
    }

    private void StopSession()
    {
        if (_stopWatch.IsRunning)
        {
            _stopWatch.Stop();
            _dispatcherTimer.Stop();

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
}
