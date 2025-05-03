using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Models;
using CodingTrackerWPF.ViewModels;
using CodingTrackerWPF.Views;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;

namespace CodingTrackerWPF.Services;

public class FiltersDialogService : IFiltersDialogService
{
    public async Task<FiltersModel?> GetFiltersAsync()
    {
        // Simulate getting filters from a dialog
        var result = await OpenDialogAsync();
        if (result is not FiltersModel filtersModel) return null;
        return filtersModel;
    }

    public async Task<object?> OpenDialogAsync()
    {
        IDateTimeDialogService dateTimeDialogService = new DateTimeDialogService();

        var viewModel = new FiltersDialogViewModel(dateTimeDialogService);

        var dialog = new FiltersDialogView()
        {
            DataContext = viewModel
        };

        var wrapper = new ContentControl { Content = dialog };

        return await DialogHost.Show(wrapper, "RootDialog");
    }
}
