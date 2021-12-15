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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostingUnitOptionsWindow.xaml
    /// </summary>
    public partial class HostingUnitOptionsWindow : Window
    {
        
        

        public HostingUnitOptionsWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void AddUnitButton_Click(object sender, RoutedEventArgs e)
        {
            new AddHostingUnit().ShowDialog();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            new LogInUnitWindow().ShowDialog();
        }
    }
}
