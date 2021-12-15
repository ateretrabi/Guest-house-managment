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
    /// Interaction logic for UpdateHostingUnitWindow.xaml
    /// </summary>
    public partial class UpdateHostingUnitWindow : Window
    {
        public IBL myBL;
        public HostingUnit hostingUnit;
        public UpdateHostingUnitWindow(HostingUnit h)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            myBL = FactAndSIngBL.GetBL();
            hostingUnit = h;
            InitializeComponent();
            hostingGrid.DataContext = hostingUnit;
            hostGrid.DataContext = hostingUnit.Owner;
            branchGrid.DataContext = hostingUnit.Owner.BankBranchDetails;
            branchAddressEXEP.Content = "";
        }


        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource hostingUnitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostingUnitViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hostingUnitViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource bankBranchViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("bankBranchViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // bankBranchViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource hostViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hostViewSource.Source = [generic data source]
        }

        private void hostingUnitNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                hostingUnitNameEXEP.Content = "";
                Configuration.isValidName(hostingUnitNameTextBox.Text);
            }
            catch (Exception ex)
            {
                hostingUnitNameEXEP.Content = ex.Message;

            }
        }

        private void bankNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bankNameEXEP.Content = "";
                Configuration.isValidName(bankNameTextBox.Text);
            }
            catch (Exception ex)
            {

                bankNameEXEP.Content = ex.Message;
            }
        }

        private void bankNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bankNumberEXEP.Content = "";
                int.Parse(bankNumberTextBox.Text);
            }
            catch (Exception)
            {

                bankNumberEXEP.Content = "Bank number must contain only numbers.";
            }
        }

        private void branchCityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                branchCityEXEP.Content = "";
                Configuration.isValidName(branchCityTextBox.Text);
            }
            catch (Exception ex)
            {

                branchCityEXEP.Content = ex.Message;
            }
        }

        private void branchNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                branchNumberEXEP.Content = "";
                int.Parse(branchNumberTextBox.Text);
            }
            catch (Exception)
            {

                branchNumberEXEP.Content = "Bank branch number must contain only numbers.";
            }
        }

        private void bankAccountNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bankAcountNumberEXEP.Content = "";
                long.Parse(bankAccountNumberTextBox.Text);
            }
            catch (Exception)
            {

                bankAcountNumberEXEP.Content = "Bank account number must contain only numbers.";
            }
        }

        private void mailAddressTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                emailEXEP.Content = "";
                Configuration.isValidEmail(mailAddressTextBox.Text);
            }
            catch (Exception ec)
            {

                emailEXEP.Content = ec.Message;
            }
        }

        private void phoneNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                phoneEXEP.Content = "";
                Configuration.isValidCell(phoneNumberTextBox.Text);
            }
            catch (Exception ex)
            {

                phoneEXEP.Content = ex.Message;
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            HostingUnit newHostingUnit;
            try
            {
                if ((string)bankNameEXEP.Content != "" || (string)bankAcountNumberEXEP.Content != "" || (string)hostingUnitNameEXEP.Content != "" || (string)bankNumberEXEP.Content != "" || (string)emailEXEP.Content != "" || (string)phoneEXEP.Content != "" || (string)branchAddressEXEP.Content != "" || (string)branchCityEXEP.Content != "" || (string)branchNumberEXEP.Content != "")
                {
                    MessageBox.Show("Please fill in the fields correctly", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                if (bankAccountNumberTextBox.Text == "" || branchNumberTextBox.Text == "" || branchCityTextBox.Text == "" || bankNumberTextBox.Text == "" || bankNameTextBox.Text == "" || phoneNumberTextBox.Text == "" || mailAddressTextBox.Text == "" || hostingUnitNameTextBox.Text == "" || branchAddressTextBox.Text == "")
                {
                    if (bankAccountNumberTextBox.Text == "")
                    {
                        bankAcountNumberEXEP.Content = "Please fill in the field.";
                    }
                    if (branchNumberTextBox.Text == "")
                    {
                        branchNumberEXEP.Content = "Please fill in the field.";
                    }
                    if (branchCityTextBox.Text == "")
                    {
                        branchCityEXEP.Content = "Please fill in the field.";
                    }
                    if (bankNumberTextBox.Text == "")
                    {
                        bankNumberEXEP.Content = "Please fill in the field.";
                    }
                    if (bankNameTextBox.Text == "")
                    {
                        bankNameEXEP.Content = "Please fill in the field.";
                    }
                    if (phoneNumberTextBox.Text == "")
                    {
                        phoneEXEP.Content = "Please fill in the field.";
                    }
                    if (mailAddressTextBox.Text == "")
                    {
                        emailEXEP.Content = "Please fill in the field.";
                    }
                    
                    if (hostingUnitNameTextBox.Text == "")
                    {
                        hostingUnitNameEXEP.Content = "Please fill in the field.";
                    }
                    if (branchAddressTextBox.Text == "")
                    {
                        branchAddressEXEP.Content = "Please fill in the field.";
                    }
                    return;
                }
                else
                {
                    newHostingUnit = new HostingUnit()
                    {
                        Owner = new Host()
                        {
                            BankAccountNumber = int.Parse(bankAccountNumberTextBox.Text),
                            BankBranchDetails = new BankBranch()
                            {
                                BankName = bankNameTextBox.Text,
                                BankNumber = int.Parse(bankNumberTextBox.Text),
                                BranchAddress = branchAddressTextBox.Text,
                                BranchCity = branchCityTextBox.Text


                            },
                            HostKey = hostingUnit.Owner.HostKey,
                            PrivateName = hostingUnit.Owner.PrivateName,
                            FamilyName = hostingUnit.Owner.FamilyName,
                            PhoneNumber = phoneNumberTextBox.Text,
                            MailAddress = mailAddressTextBox.Text,
                            CollectionClearance = (bool)collectionClearanceCheckBox.IsChecked,

                        },
                        HostingUnitName = hostingUnitNameTextBox.Text,
                        IsChildrenAttractions = (bool)isChildrenAttractionsCheckBox.IsChecked,
                        IsGarden = (bool)isGardenCheckBox.IsChecked,
                        IsJacuzzi = (bool)isJacuzziCheckBox.IsChecked,
                        IsPool = (bool)isPoolCheckBox.IsChecked,
                        Area = hostingUnit.Area,
                        Type = hostingUnit.Type,
                        Diary = hostingUnit.Diary
                    };
                    myBL.AddHostingUnit(newHostingUnit);
                    MessageBoxResult mbResult = MessageBox.Show("The operation ended successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (mbResult == MessageBoxResult.OK)
                    {
                        this.Close();
                    }
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
    