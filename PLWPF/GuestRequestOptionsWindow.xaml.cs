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
    /// Interaction logic for GuestRequestOptionsWindow.xaml
    /// </summary>
    public partial class GuestRequestOptionsWindow : Window
    {
        public static IBL myBL;
        GuestRequest GuestRequest;
        public GuestRequestOptionsWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            myBL = FactAndSIngBL.GetBL();
            GuestRequest = new GuestRequest()
            {
                Status = GuestRequestStatus.Active,
                RegistrationDate=DateTime.Today,

            };

            InitializeComponent();
            this.DPentryDate.DisplayDateStart = DateTime.Today;
            this.DPentryDate.SelectedDate = DateTime.Today;
            this.DPreleaseDate.DisplayDateStart = DateTime.Today.AddDays(1);
            DPreleaseDate.SelectedDate = DateTime.Today.AddDays(1);
            this.DPreleaseDate.DisplayDateEnd = DateTime.Today.AddMonths(11);
            this.DPentryDate.DisplayDateEnd = DateTime.Today.AddMonths(11);
            this.AreaCB.ItemsSource = Enum.GetValues(typeof(Area));
            this.AreaCB.SelectedIndex = 0;
            this.ComboBoxType.ItemsSource= Enum.GetValues(typeof(BE.Type));
            this.ComboBoxType.SelectedIndex = 0;
            for (int i = 0; i < 10; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = i + 1;
                adultsCB.Items.Add(newItem);
            }
            this.adultsCB.SelectedIndex = 0;
            for (int i = 0; i < 21; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = i;
                kidsCB.Items.Add(newItem);
            }
            this.kidsCB.SelectedIndex = 0;


            this.isPoolCB.ItemsSource = Enum.GetValues(typeof(Additions));
            isPoolCB.SelectedIndex = 0;
            this.isJacuzziCB.ItemsSource = Enum.GetValues(typeof(Additions));
            isJacuzziCB.SelectedIndex = 0;
            this.isgardenCB.ItemsSource = Enum.GetValues(typeof(Additions));
            isgardenCB.SelectedIndex = 0;
            this.isaatractionsCB.ItemsSource = Enum.GetValues(typeof(Additions));
            isaatractionsCB.SelectedIndex = 0;
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            
            {
                if ((string)FirstNameExep.Text!=""|| (string)LastNAmeExeptions.Text != ""|| (string)emailExep.Text != "")
                {
                    MessageBox.Show("Please fill in the fields correctly", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (adultsCB.SelectedItem==null||kidsCB.SelectedItem==null||AreaCB.SelectedItem==null||ComboBoxType.SelectedItem==null||isPoolCB.SelectedItem==null||isJacuzziCB.SelectedItem==null||isgardenCB.SelectedItem==null||isaatractionsCB.SelectedItem==null)
                {
                    MessageBox.Show("Please choose an option.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if ((TBfirstName.Text == "")||(TBlastName.Text == "")||(TBemailAddress.Text == ""))
                {
                    if (TBfirstName.Text == "")
                    {
                        TBfirstName.BorderBrush = Brushes.Red;
                        FirstNameExep.Text = "Please fill in the field.";

                    }
                    

                    if (TBlastName.Text == "")
                    {
                        TBlastName.BorderBrush = Brushes.Red;
                        LastNAmeExeptions.Text = "Please fill in the field.";
                    }
                   
                    if (TBemailAddress.Text == "")
                    {
                        TBemailAddress.BorderBrush = Brushes.Red;
                        emailExep.Text = "Please fill in the field.";
                    }
                    
                    return;
                }
                GuestRequest.PrivateName = TBfirstName.Text;
                GuestRequest.FamilyName = TBlastName.Text;
                GuestRequest.MailAddress = TBemailAddress.Text;
                GuestRequest.EntryDate = ((DateTime)this.DPentryDate.SelectedDate);
                GuestRequest.ReleaseDate = (DateTime)this.DPreleaseDate.SelectedDate;
                GuestRequest.Area = (Area)AreaCB.SelectedItem;
                GuestRequest.Type = (BE.Type)ComboBoxType.SelectedItem;
                GuestRequest.Adults = int.Parse((string)this.adultsCB.Text);
                GuestRequest.Children = int.Parse((string)this.kidsCB.Text);
                GuestRequest.IsPool = (Additions)this.isPoolCB.SelectedItem;
                GuestRequest.IsJacuzzi = (Additions)this.isJacuzziCB.SelectedItem;
                GuestRequest.IsGarden = (Additions)this.isgardenCB.SelectedItem;
                GuestRequest.IsChildrenAttractions = (Additions)this.isaatractionsCB.SelectedItem;

                myBL.AddGuestRequest(GuestRequest);
                MessageBoxResult mbResult = MessageBox.Show("The operation ended successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                if (mbResult== MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

        
        }

       

        private void TBlastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                LastNAmeExeptions.Text = "";
                TBlastName.BorderBrush = Brushes.Transparent;
                Configuration.isValidName(TBlastName.Text);
            }
            catch (Exception ex)
            {

                LastNAmeExeptions.Text = ex.Message;
                TBlastName.BorderBrush = Brushes.Red;
            }
        }

        private void TBfirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                FirstNameExep.Text = "";
                TBfirstName.BorderBrush = Brushes.Transparent;
                Configuration.isValidName(TBfirstName.Text);
            }
            catch (Exception ex)
            {

                FirstNameExep.Text = ex.Message;
                TBfirstName.BorderBrush = Brushes.Red;
            }
        }

        private void TBemailAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                emailExep.Text = "";
                TBemailAddress.BorderBrush = Brushes.Transparent;
                Configuration.isValidEmail(TBemailAddress.Text);
            }
            catch (Exception ex)
            {

                emailExep.Text = ex.Message;
                TBemailAddress.BorderBrush = Brushes.Red;
            }
        }

        

        private void DPentryDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DPreleaseDate.DisplayDateStart = (((DateTime)(DPentryDate.SelectedDate)).AddDays(1));
            DPreleaseDate.SelectedDate = (((DateTime)(DPentryDate.SelectedDate)).AddDays(1));
        }
    }
}
