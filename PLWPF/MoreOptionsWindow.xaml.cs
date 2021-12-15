using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MoreOptionsWindow.xaml
    /// </summary>
    public partial class MoreOptionsWindow : Window
    {
        public MoreOptionsWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void groupGuestsByArea_Click(object sender, RoutedEventArgs e)
        {
            new guestGroupedByArea().ShowDialog();
        }

        private void getGuestsbyAmountPeople_Click(object sender, RoutedEventArgs e)
        {
            new guestsGroupedByVacationer().Show();
        }

        private void getHostsByNumberOfUnits_Click(object sender, RoutedEventArgs e)
        {
            new hostsGroupedByNumUnits().Show();
        }

        private void getUnitsByArea_Click(object sender, RoutedEventArgs e)
        {
            new hostingUnitGroupedByArea().ShowDialog();
        }
    }
}
