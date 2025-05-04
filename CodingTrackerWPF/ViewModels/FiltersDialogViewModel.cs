using CodingTrackerWPF.Enums;
using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace CodingTrackerWPF.ViewModels;

public partial class FiltersDialogViewModel : ObservableObject
{
    private readonly IDateTimeDialogService _dateTimeDialogService;

    public IAsyncRelayCommand? GetDateRangeCommand { get; }
    public IRelayCommand? SubmitCommand { get; }

    [ObservableProperty]
    private FilterPeriod selectedPeriod;
    [ObservableProperty]
    private ObservableCollection<string> periodComboBoxItems = new();
    [ObservableProperty]
    private ObservableCollection<string> dayOfWeekComboBoxItems = new();
    [ObservableProperty]
    private string selectedPeriodComboBoxItem = "None";
    [ObservableProperty]
    private string selectedDayOfWeek = "None";
    [ObservableProperty]
    private int startHours, startMinutes, endHours, endMinutes;

    private readonly string _filtersDialogID = "FiltersDialog";

    private DateTime? startDateTime;
    private DateTime? endDateTime;    
    private TimeSpan? startDuration, endDuration;

    public FiltersDialogViewModel(IDateTimeDialogService dateTimeDialogService)
    {
        _dateTimeDialogService = dateTimeDialogService;

        GetDateRangeCommand = new AsyncRelayCommand(GetDateRange);
        SubmitCommand = new RelayCommand(Submit);

        SelectedPeriod = FilterPeriod.Month;
        UpdatePeriodComboBoxItems();
        UpdateDaysOfWeekComboBoxItems();
    }

    partial void OnSelectedPeriodChanged(FilterPeriod value)
    {
        UpdatePeriodComboBoxItems();
    }

    public ICommand SetPeriodCommand => new RelayCommand<string>(periodStr =>
    {
        if (Enum.TryParse<FilterPeriod>(periodStr, out var period))
        {
            SelectedPeriod = period;
        }
    });

    private void UpdatePeriodComboBoxItems()
    {
        PeriodComboBoxItems.Clear();
        PeriodComboBoxItems.Add("None");

        switch (SelectedPeriod)
        {
            case FilterPeriod.Week:
                var startOfYear = new DateTime(DateTime.Now.Year, 1, 1);
                var weeksInYear = (int)Math.Ceiling((DateTime.Now - startOfYear).TotalDays / 7);
                for (int i = 1; i <= weeksInYear; i++)
                    PeriodComboBoxItems.Add($"Week {i}");
                break;

            case FilterPeriod.Month:
                for (int i = 1; i <= 12; i++)
                    PeriodComboBoxItems.Add(CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(i));
                break;

            case FilterPeriod.Year:
                int currentYear = DateTime.Now.Year;
                for (int i = 2000; i <= currentYear; i++)
                    PeriodComboBoxItems.Add(i.ToString());
                break;
        }
    }

    private void UpdateDaysOfWeekComboBoxItems()
    {
        DayOfWeekComboBoxItems.Clear();
        DayOfWeekComboBoxItems.Add("None");

        for (int i = 0; i < 7; i++)
        {
            var day = (DayOfWeek)(((int)DayOfWeek.Monday + i) % 7);
            DayOfWeekComboBoxItems.Add(day.ToString());
        }
    }

    private async Task GetDateRange()
    {
        startDateTime = await _dateTimeDialogService.GetSessionDateTimeStartAsync(_filtersDialogID, null, null);
        if (startDateTime == null) return;

        await Task.Delay(500);

        endDateTime = await _dateTimeDialogService.GetSessionDateTimeEndAsync(_filtersDialogID, null, null);
        if (endDateTime == null) return;
    }

    private void Submit()
    {
        DayOfWeek? selectedDay = Enum.TryParse<DayOfWeek>(SelectedDayOfWeek, out var day) ? day : null;

        if ((StartHours > 0 || StartMinutes > 0) && (EndHours > 0 || EndMinutes > 0))
        {
            startDuration = new TimeSpan(StartHours, StartMinutes, 0);
            endDuration = new TimeSpan(EndHours, EndMinutes, 0);
        }
        else
        {
            startDuration = null;
            endDuration = null;
        }

        if (startDuration > endDuration)
        {
            var temp = startDuration;
            startDuration = endDuration;
            endDuration = temp;
        }

        var filtersModel = new FiltersModel(SelectedPeriodComboBoxItem, selectedDay, startDateTime, endDateTime, startDuration, endDuration);

        DialogHost.CloseDialogCommand.Execute(filtersModel, null);
    }
}
