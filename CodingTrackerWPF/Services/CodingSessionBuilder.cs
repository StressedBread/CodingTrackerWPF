using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CodingTrackerWPF.ViewModels;
using CodingTrackerWPF.Views;
using MaterialDesignThemes.Wpf;

namespace CodingTrackerWPF.Services;

public class CodingSessionBuilder : ICodingSessionBuilder
{
    public async Task<CodingSession?> CreateValidatedSessionAsync(DateTime? startDateTime, DateTime? endDateTime)
    {
        if (startDateTime == null || endDateTime == null) return null;

        DateTime startDateTimeValue = startDateTime.Value;
        DateTime endDateTimeValue = endDateTime.Value;

        TimeSpan duration = endDateTimeValue - startDateTimeValue;

        if (duration < TimeSpan.Zero)
        {
            var messageDialogView = new MessageDialogView
            {
                DataContext = new MessageDialogViewModel("Error", "End cannot be earlier than start!")
            };
            await DialogHost.Show(messageDialogView, "RootDialog");
            return null;
        }

        return new CodingSession(startDateTimeValue, endDateTimeValue, duration);
    }
}
