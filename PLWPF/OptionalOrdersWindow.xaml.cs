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
using System.Windows.Shapes;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for OptionalOrdersWindow.xaml
    /// </summary>
    public partial class OptionalOrdersWindow : Window
    {
        HostingUnit hostingUnit;
        ObservableCollection<GuestRequest> optionalOrders;
        public IBL myBL;
        public OptionalOrdersWindow(HostingUnit h)
        {
            hostingUnit = h;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            myBL = FactAndSIngBL.GetBL();
            optionalOrders = new ObservableCollection<GuestRequest>(myBL.GuestRequestList(x => myBL.matchHostingUnithToGuestRequest(x, hostingUnit)));
            InitializeComponent();
            if (optionalOrders.Count == 0)
            {
                MessageBoxResult mbResult = MessageBox.Show("No customer requirement fits your hosting unit", "Sorry:(", MessageBoxButton.OK, MessageBoxImage.Information);
                switch (mbResult)
                {
                    case MessageBoxResult.OK:
                        return;
                       // break;
                }
            }
            else
                optionalOrdersCB.ItemsSource = optionalOrders;
            optionalOrdersCB.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myBL.CreateOrder(hostingUnit, (GuestRequest)optionalOrdersCB.SelectedItem);
                MessageBoxResult mbResult = MessageBox.Show("The operation ended successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                optionalOrders.Remove((GuestRequest)optionalOrdersCB.SelectedItem);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); 
            }
        }
    }
}
