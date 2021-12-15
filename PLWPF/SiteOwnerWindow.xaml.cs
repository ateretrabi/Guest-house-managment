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
    /// Interaction logic for SiteOwnerWindow.xaml
    /// </summary>
    public partial class SiteOwnerWindow : Window
    {
        public IBL myBL = FactAndSIngBL.GetBL();
        SiteOwner siteOwner = new SiteOwner()
        {
            password = 00000000
        };
        public SiteOwnerWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

       
        

        private void showSiteProfitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The profits from the website are: "+myBL.siteProfit()+"$", "your profits", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (long.Parse(siteOwnerPassword.Password) == siteOwner.password)
            {
                showSiteProfitButton.IsEnabled = true;
                moreOptionsButton.IsEnabled = true;
            }
            else
            {
                siteOwnerPassword.BorderBrush = Brushes.Red;
                WrongPassword.Text = "the password is incorrect.";
            }
                
        }

        private void siteOwnerPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            siteOwnerPassword.BorderBrush = Brushes.Transparent;
            WrongPassword.Text = "";
        }

        private void moreOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            new MoreOptionsWindow().ShowDialog();
        }
    }
}
