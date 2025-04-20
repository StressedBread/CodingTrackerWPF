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
    [ObservableProperty]
    private string sessionTimer = "00:00:00";

    public IRelayCommand? StartSessionCommand { get; }
    public IRelayCommand? StopSessionCommand { get; }

    private TimeSpan _timeElapsed = TimeSpan.Zero;
    private Stopwatch _stopWatch = new();

    private DispatcherTimer _dispatcherTimer = new();

    public LiveCodingSessionViewModel()
    {
        StartSessionCommand = new RelayCommand(StartSession);
        StopSessionCommand = new RelayCommand(StopSession);

        _dispatcherTimer.Tick += DispatcherTimer_Tick;
    }

    private void StartSession()
    {
        _stopWatch.Start();
        _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
        _dispatcherTimer.Start();
    }

    private void StopSession()
    {
        _stopWatch.Stop();
        _dispatcherTimer.Stop();
        _stopWatch.Reset();

        _timeElapsed = TimeSpan.Zero;
        SessionTimer = "00:00:00";
    }

    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        _timeElapsed = _stopWatch.Elapsed;
        SessionTimer = _timeElapsed.ToString(@"hh\:mm\:ss");
    }
}
