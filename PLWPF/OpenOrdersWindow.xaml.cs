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
using System.ComponentModel;
using BE;
using BL;
using System.Net.Mail;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for OpenOrdersWindow.xaml
    /// </summary>
    public partial class OpenOrdersWindow : Window
    {
        HostingUnit hostingUnit;
        ObservableCollection<Order> openOrders;
        public IBL myBL;
        BackgroundWorker worker;
        public OpenOrdersWindow(HostingUnit h)
        {
            hostingUnit = h;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            myBL = FactAndSIngBL.GetBL();
            openOrders =new ObservableCollection<Order>( myBL.OrderList(x => (x.HostingUnitKey == hostingUnit.HostingUnitKey)&&((x.Status==OrderStatus.NotYetAddressed)||(x.Status == OrderStatus.SentAnEmail))));
            InitializeComponent();
            if (openOrders.Count==0)
            {
                MessageBoxResult mbResult = MessageBox.Show("There are no open orders who requires handling.", "no open orders", MessageBoxButton.OK, MessageBoxImage.Information);
                switch (mbResult)
                {
                    case MessageBoxResult.OK:
                        //this.Close();
                        return;
                        
                }
            }
            ordersCB.ItemsSource = openOrders;
            ordersCB.SelectedIndex = 0;
            inviteButton.IsEnabled = false;
            closeDealButton.IsEnabled = false;
            if (((Order)ordersCB.SelectedItem).Status==OrderStatus.SentAnEmail)
            {
                closeDealButton.IsEnabled = true;

            }
            else
                inviteButton.IsEnabled = true;

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Order order = (Order)e.Argument;
            EmailSender(order);
        }

        public void EmailSender(Order order)
        {
            GuestRequest guest = myBL.getGuestRequest(order.GuestRequestKey);
            HostingUnit hosting = (myBL.HostingUnitList(x => x.HostingUnitKey == order.HostingUnitKey))[0];
            MailMessage mail = new MailMessage();
           
            mail.To.Add(guest.MailAddress);
     
            mail.From = new MailAddress("hostingsystem100@gmail.com");
            
            mail.Subject = "invitation to visit our hosting unit";
          
            mail.Body = "We now invite you to stay in our beautiful hosting unit: "+hosting.HostingUnitName+"\n";
            mail.Body += "for further information please contact us through this Email: " + hosting.Owner.MailAddress + "\n";
            mail.Body += "or though phone number: " + hosting.Owner.PhoneNumber + "\n";
            mail.Body += "thank you for your interest " + hosting.Owner.fullName + "\n"; 


            mail.IsBodyHtml = true;

            
            SmtpClient smtp = new SmtpClient();
            
            smtp.Host = "smtp.gmail.com";
            

            smtp.Credentials = new System.Net.NetworkCredential("hostingsystem100@gmail.com", "08642975");
           
            smtp.EnableSsl = true;
            bool sent = false;
            while (sent==false)
            {
                try
                {
                    smtp.Send(mail);
                    sent = true;
                    MessageBox.Show("Invitation sent successfully", "GREAT:)");
                }
                catch (Exception  )
                {
                    System.Threading.Thread.Sleep(2);

                    //MessageBox.Show(ex.Message, ":(", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            

        }


        private void inviteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                
               worker.RunWorkerAsync((Order)ordersCB.SelectedItem);
                myBL.SendInvite((Order)ordersCB.SelectedItem);
                inviteButton.IsEnabled = false;
                closeDealButton.IsEnabled = true;

                MessageBoxResult mbResult = MessageBox.Show("The operation ended successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void closeDealButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myBL.CloseDeal((Order)ordersCB.SelectedItem);
                MessageBoxResult mbResult = MessageBox.Show("The operation ended successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                openOrders.Remove((Order)ordersCB.SelectedItem);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ordersCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Order)ordersCB.SelectedItem).Status == OrderStatus.SentAnEmail)
            {
                closeDealButton.IsEnabled = true;
                inviteButton.IsEnabled = false;
            }
            else
            {
                inviteButton.IsEnabled = true;
                closeDealButton.IsEnabled = false;
            }
                
        }
    }
}
