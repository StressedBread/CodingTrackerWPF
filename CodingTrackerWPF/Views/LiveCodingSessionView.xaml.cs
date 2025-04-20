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

            DataContext = new LiveCodingSessionViewModel();
        }
    }
}
