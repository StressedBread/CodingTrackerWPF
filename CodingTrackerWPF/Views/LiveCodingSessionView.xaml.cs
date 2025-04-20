using CodingTrackerWPF.Interfaces;
using CodingTrackerWPF.Services;
using CodingTrackerWPF.ViewModels;
using System.Windows.Controls;

namespace CodingTrackerWPF.Views
{
    /// <summary>
    /// Interaction logic for LiveCodingSessionView.xaml
    /// </summary>
    public partial class LiveCodingSessionView : UserControl
    {
        public LiveCodingSessionView()
        {
            InitializeComponent();

            var queryService = new QueryService();

            ICodingSessionService codingSessionService = new CodingSessionService(queryService);

            DataContext = new LiveCodingSessionViewModel(codingSessionService);
        }
    }
}
