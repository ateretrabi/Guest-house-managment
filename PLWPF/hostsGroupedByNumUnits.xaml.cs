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
using BL;
using BE;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for hostsGroupedByNumUnits.xaml
    /// </summary>
    public partial class hostsGroupedByNumUnits : Window
    {
        public IBL myBL;
        public hostsGroupedByNumUnits()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            myBL = FactAndSIngBL.GetBL();
            InitializeComponent();
            var groupedhosts = myBL.GetAllHostsByNumOfUnitsTheyOwn();
            List<Host> hosts = new List<Host>();
            foreach (var list in groupedhosts)
            {
                foreach (var item in list)
                {
                    hosts.Add(item);
                }
            }
            LVgroup.ItemsSource = hosts;
        }
    }
}
