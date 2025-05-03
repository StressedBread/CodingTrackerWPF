using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for FiltersDialogView.xaml
    /// </summary>
    public partial class FiltersDialogView : UserControl
    {
        private static readonly Regex _numericRegex = new Regex("^[0-9]+$");

        public FiltersDialogView()
        {
            InitializeComponent();
        }

        private void StartHoursBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_numericRegex.IsMatch(e.Text);
        }

        private void StartMinutesBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_numericRegex.IsMatch(e.Text);
        }

        private void EndHoursBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_numericRegex.IsMatch(e.Text);
        }

        private void EndMinutesBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_numericRegex.IsMatch(e.Text);
        }

        private void StartMinutesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (int.TryParse(textBox.Text, out int minutes))
                {
                    if (minutes > 59)
                    {
                        textBox.Text = "59";
                        textBox.CaretIndex = textBox.Text.Length;
                    }
                }
            }
        }

        private void EndMinutesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (int.TryParse(textBox.Text, out int minutes))
                {
                    if (minutes > 59)
                    {
                        textBox.Text = "59";
                        textBox.CaretIndex = textBox.Text.Length;
                    }
                }
            }
        }
    }
}
