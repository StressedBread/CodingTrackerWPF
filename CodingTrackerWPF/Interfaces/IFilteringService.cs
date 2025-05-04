using CodingTrackerWPF.Models;
using System.Collections.ObjectModel;

namespace CodingTrackerWPF.Interfaces;

public interface IFilteringService
{
    List<CodingSession> Filtering(ObservableCollection<CodingSession> sessions, FiltersModel filters);
    DateTime? GetDateRangeFromPeriod(string period);
}
