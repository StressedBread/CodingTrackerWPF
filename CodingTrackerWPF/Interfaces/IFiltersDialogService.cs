using CodingTrackerWPF.Models;

namespace CodingTrackerWPF.Interfaces;

public interface IFiltersDialogService
{
    Task<object?> OpenDialogAsync();
    Task<FiltersModel?> GetFiltersAsync();
}
