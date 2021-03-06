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
    /// Interaction logic for guestGroupedByArea.xaml
    /// </summary>
    public partial class guestGroupedByArea : Window
    {
        public IBL myBL;
        public guestGroupedByArea()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            myBL = FactAndSIngBL.GetBL();
            InitializeComponent();
            var groupedGuests = myBL.GetAllGuestsGroupedByArea();
            List<GuestRequest> guestRequests = new List<GuestRequest>();
            foreach (var list in groupedGuests)
            {
                foreach (var item in list)
                {
                    guestRequests.Add(item);
                }
            }
            LVgroup.ItemsSource = guestRequests;
        }
    }
}
