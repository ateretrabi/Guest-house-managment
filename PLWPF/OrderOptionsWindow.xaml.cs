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
    /// Interaction logic for OrderOptionsWindow.xaml
    /// </summary>
    public partial class OrderOptionsWindow : Window
    {
        HostingUnit hostingUnit;
        public IBL myBL;
        public OrderOptionsWindow(HostingUnit h)
        {
            hostingUnit = h;
            myBL = FactAndSIngBL.GetBL();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void OpenOrders_Click(object sender, RoutedEventArgs e)
        {
            var openorders = myBL.OrderList(x => (x.HostingUnitKey == hostingUnit.HostingUnitKey) && ((x.Status == OrderStatus.NotYetAddressed) || (x.Status == OrderStatus.SentAnEmail)));
            if (openorders.Count!=0)
            {
                new OpenOrdersWindow(hostingUnit).ShowDialog();
            }
            else
                MessageBox.Show("There are no open orders who requires handling.", "no open orders", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void PossibleOrders_Click(object sender, RoutedEventArgs e)
        {
            var possible = myBL.GuestRequestList(x => myBL.matchHostingUnithToGuestRequest(x, hostingUnit));
            if (possible.Count!=0)
            {
                new OptionalOrdersWindow(hostingUnit).ShowDialog();
            }
            else
                MessageBox.Show("No customer requirement fits your hosting unit", "Sorry:(", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
