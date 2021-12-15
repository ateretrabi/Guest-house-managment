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
    /// Interaction logic for hostingUnitGroupedByArea.xaml
    /// </summary>
    public partial class hostingUnitGroupedByArea : Window
    {
        public IBL myBL;
        public hostingUnitGroupedByArea()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            myBL = FactAndSIngBL.GetBL();
            InitializeComponent();
            var groupedunits = myBL.GetAllUnitsGroupedByArea();
            List<HostingUnit> units = new List<HostingUnit>();
            foreach (var list in groupedunits)
            {
                foreach (var item in list)
                {
                    units.Add(item);
                }
            }
            LVgroup.ItemsSource = units;
        }
    }
}
