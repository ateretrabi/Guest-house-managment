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
    /// Interaction logic for LogInUnitWindow.xaml
    /// </summary>
    public partial class LogInUnitWindow : Window
    {
        public static IBL myBL;
        List<HostingUnit> hostingUnits;
        
        public LogInUnitWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            myBL = FactAndSIngBL.GetBL();
           

            InitializeComponent();
            idGrid.Visibility = Visibility.Visible;
            hostingUnitGrid.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)idEXEP.Content != "") 
            {
                MessageBox.Show("Please fill in the fields correctly", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Id.Password=="")
            {
                MessageBox.Show("Please fill in the fields correctly", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

           var h= from item in myBL.HostingUnitList()
                  where item.Owner.HostKey==long.Parse(Id.Password)
                  select item;
            hostingUnits = h.ToList();

            if (hostingUnits == null)
            {
                idEXEP.Content = "Id not found.";
            }
            else
            {
                idGrid.Visibility = Visibility.Hidden;
                hostingUnitsCB.ItemsSource = hostingUnits;
                hostingUnitsCB.DisplayMemberPath = "HostingUnitName";
                hostingUnitsCB.SelectedIndex = 0;
                hostingUnitGrid.Visibility = Visibility.Visible;
            }
        }

        private void Id_PasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                idEXEP.Content = "";
                Configuration.isValidId(Id.Password);
            }
            catch (Exception ex)
            {

                idEXEP.Content = ex.Message;
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateHostingUnitWindow updateHostingUnitWindow = new UpdateHostingUnitWindow((HostingUnit)hostingUnitsCB.SelectedItem);
            
            updateHostingUnitWindow.ShowDialog();
            var h = from item in myBL.HostingUnitList()
                    where item.Owner.HostKey == long.Parse(Id.Password)
                    select item;
            hostingUnits = h.ToList();
            hostingUnitsCB.ItemsSource = hostingUnits;
            hostingUnitsCB.DisplayMemberPath = "HostingUnitName";
            hostingUnitsCB.SelectedIndex = 0;

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult mbResult = MessageBox.Show("Are you sure you want to delete this hosting unit?", "sure?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (mbResult)
                {
                    case MessageBoxResult.Yes:
                        myBL.DeleteHostingUnit((HostingUnit)hostingUnitsCB.SelectedItem);
                        MessageBox.Show("The operation ended successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
                var h = from item in myBL.HostingUnitList()
                        where item.Owner.HostKey == long.Parse(Id.Password)
                        select item;
                hostingUnits = h.ToList();
                if (hostingUnits.Count()==0)
                {
                    MessageBox.Show("You no longer have units", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                hostingUnitsCB.ItemsSource = hostingUnits;
                hostingUnitsCB.DisplayMemberPath = "HostingUnitName";
                hostingUnitsCB.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void orderButton_Click(object sender, RoutedEventArgs e)
        {
            new OrderOptionsWindow((HostingUnit)hostingUnitsCB.SelectedItem).ShowDialog();
        }
    }
}
