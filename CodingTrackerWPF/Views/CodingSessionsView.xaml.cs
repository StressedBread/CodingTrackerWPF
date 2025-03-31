using CodingTrackerWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodingTrackerWPF.Views
{
    /// <summary>
    /// Interaction logic for CodingSessionsView.xaml
    /// </summary>
    public partial class CodingSessionsView : UserControl
    {
        private ObservableCollection<CodingSession> CodingSessions { get; set; }

        public CodingSessionsView()
        {
            InitializeComponent();

            CodingSessions = new ObservableCollection<CodingSession>
            {
                new CodingSession(1, DateTime.Now, DateTime.Now.AddHours(1), TimeSpan.FromHours(1)),
                new CodingSession(2, DateTime.Now.AddHours(2), DateTime.Now.AddHours(3), TimeSpan.FromHours(2)),
                new CodingSession(3, DateTime.Now.AddHours(4), DateTime.Now.AddHours(5), TimeSpan.FromHours(3)),
            };

            MainDataGrid.ItemsSource = CodingSessions;
        }
    }
}
