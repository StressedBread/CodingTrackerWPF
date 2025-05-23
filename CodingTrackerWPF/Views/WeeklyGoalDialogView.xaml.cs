﻿using System;
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
    /// Interaction logic for WeeklyGoalDialogView.xaml
    /// </summary>
    public partial class WeeklyGoalDialogView : UserControl
    {
        public WeeklyGoalDialogView()
        {
            InitializeComponent();
        }

        private void WeeklyGoalInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
    }
}
