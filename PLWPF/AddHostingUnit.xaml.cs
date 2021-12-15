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
    /// Interaction logic for AddHostingUnit.xaml
    /// </summary>
    public partial class AddHostingUnit : Window
    {
        public IBL myBL;
        HostingUnit h;
        HostingUnit hosting;

        public AddHostingUnit()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            myBL = FactAndSIngBL.GetBL();
            InitializeComponent();
            h = new HostingUnit();
            this.DataContext = h;
            this.AreaCB.ItemsSource = Enum.GetValues(typeof(Area));
            AreaCB.SelectedIndex = 1;
            this.typeCB.ItemsSource = Enum.GetValues(typeof(BE.Type));
            typeCB.SelectedIndex = 0;
            branchAddressEXEP_Copy.Content = "";

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            
            System.Windows.Data.CollectionViewSource hostViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hostViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource bankBranchViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("bankBranchViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // bankBranchViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource hostingUnitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostingUnitViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hostingUnitViewSource.Source = [generic data source]
        }

        private void bankAccountNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                accontNumberEXEP.Content = "";
                long.Parse(bankAccountNumberTextBox.Text);
            }
            catch (Exception)
            {

                accontNumberEXEP.Content = "Bank account number must contain only numbers.";
            }
        }

        private void hostingUnitNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                hostingUnitNAmeExep.Content = "";
                Configuration.isValidName(hostingUnitNameTextBox.Text);
            }
            catch (Exception ex)
            {
                hostingUnitNAmeExep.Content = ex.Message;

            }
        }

        private void privateNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                firstNameEXEP_Copy.Content = "";
                Configuration.isValidName(privateNameTextBox.Text);
            }
            catch (Exception ex)
            {

                firstNameEXEP_Copy.Content = ex.Message;
            }
        }

        private void familyNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                lastNameEXEP_Copy1.Content = "";
                Configuration.isValidName(familyNameTextBox.Text);
            }
            catch (Exception ex)
            {

                lastNameEXEP_Copy1.Content = ex.Message;
            }
        }

        private void hostKeyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                
                idEXEP.Content = "";
                Configuration.isValidId(hostKeyTextBox.Text);
            }
            catch (Exception ex)
            {

                idEXEP.Content = ex.Message;
            }
        }

        private void mailAddressTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                emailEXEP_Copy3.Content = "";
                Configuration.isValidEmail(mailAddressTextBox.Text);
            }
            catch (Exception ec)
            {

                emailEXEP_Copy3.Content = ec.Message;
            }
        }

        private void phoneNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                phoneEXEP_Copy4.Content = "";
                Configuration.isValidCell(phoneNumberTextBox.Text);
            }
            catch (Exception ex)
            {

                phoneEXEP_Copy4.Content = ex.Message;
            }
        }

        private void bankNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bankNameEXEP_Copy5.Content = "";
                Configuration.isValidName(bankNameTextBox.Text);
            }
            catch (Exception ex)
            {

                bankNameEXEP_Copy5.Content = ex.Message;
            }
        }

        private void bankNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bankNumberEXEP_Copy6.Content = "";
                int.Parse(bankNumberTextBox.Text);
            }
            catch (Exception )
            {

                bankNumberEXEP_Copy6.Content = "Bank number must contain only numbers.";
            }
        }

        private void branchCityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                branchCityEXEP_Copy8.Content = "";
                Configuration.isValidName(branchCityTextBox.Text);
            }
            catch (Exception ex)
            {

                branchCityEXEP_Copy8.Content = ex.Message; 
            }
        }

        private void branchNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                branchNUmberEXEP_Copy9.Content = "";
                int.Parse(branchNumberTextBox.Text);
            }
            catch (Exception)
            {

                branchNUmberEXEP_Copy9.Content= "Bank branch number must contain only numbers.";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((string)bankNameEXEP_Copy5.Content != "" || (string)accontNumberEXEP.Content != "" ||(string)hostingUnitNAmeExep.Content!=""|| (string)bankNumberEXEP_Copy6.Content != ""|| (string)firstNameEXEP_Copy.Content != ""|| (string)lastNameEXEP_Copy1.Content != ""|| (string)idEXEP.Content != ""|| (string)emailEXEP_Copy3.Content != ""|| (string)phoneEXEP_Copy4.Content != ""|| (string)branchAddressEXEP_Copy.Content != ""|| (string)branchCityEXEP_Copy8.Content != ""|| (string)branchNUmberEXEP_Copy9.Content != "")
                {
                    MessageBox.Show("Please fill in the fields correctly", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if ( AreaCB.SelectedItem == null || typeCB.SelectedItem == null )
                {
                    MessageBox.Show("Please choose an option.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (bankAccountNumberTextBox.Text == "" || branchNumberTextBox.Text == "" || branchCityTextBox.Text == "" || bankNumberTextBox.Text == "" || bankNameTextBox.Text == "" || phoneNumberTextBox.Text == "" || mailAddressTextBox.Text == "" || hostKeyTextBox.Text == "" || familyNameTextBox.Text == "" || privateNameTextBox.Text == ""|| hostingUnitNameTextBox.Text==""||branchAddressTextBox.Text=="")
                {
                    if (bankAccountNumberTextBox.Text == "")
                    {
                        accontNumberEXEP.Content = "Please fill in the field.";
                    }
                    if (branchNumberTextBox.Text == "")
                    {
                        branchNUmberEXEP_Copy9.Content= "Please fill in the field.";
                    }
                    if (branchCityTextBox.Text == "")
                    {
                        branchCityEXEP_Copy8.Content= "Please fill in the field.";
                    }
                    if (bankNumberTextBox.Text == "")
                    {
                        bankNumberEXEP_Copy6.Content= "Please fill in the field.";
                    }
                    if (bankNameTextBox.Text == "")
                    {
                        bankNameEXEP_Copy5.Content= "Please fill in the field.";
                    }
                    if (phoneNumberTextBox.Text == "")
                    {
                        phoneEXEP_Copy4.Content= "Please fill in the field.";
                    }
                    if (mailAddressTextBox.Text == "")
                    {
                        emailEXEP_Copy3.Content= "Please fill in the field.";
                    }
                    if (hostKeyTextBox.Text == "")
                    {
                        idEXEP.Content= "Please fill in the field.";
                    }
                    if (familyNameTextBox.Text == "")
                    {
                        lastNameEXEP_Copy1.Content= "Please fill in the field.";
                    }
                    if (privateNameTextBox.Text == "")
                    {
                        firstNameEXEP_Copy.Content = "Please fill in the field.";
                    }
                    if (hostingUnitNameTextBox.Text == "")
                    {
                        hostingUnitNAmeExep.Content= "Please fill in the field.";
                    }
                    if (branchAddressTextBox.Text == "")
                    {
                        branchAddressEXEP_Copy.Content= "Please fill in the field.";
                    }
                    return;
                }
                else
                {
                    hosting = new HostingUnit()
                    {
                        Owner = new Host()
                        {
                            BankAccountNumber = int.Parse(bankAccountNumberTextBox.Text),
                            BankBranchDetails = new BankBranch()
                            {
                                BankName = bankNameTextBox.Text,
                                BankNumber = int.Parse(bankNumberTextBox.Text),
                                BranchAddress = branchAddressTextBox.Text,
                                BranchCity = branchCityTextBox.Text,
                                BranchNumber = int.Parse(branchNumberTextBox.Text)

                            },
                            HostKey = long.Parse(hostKeyTextBox.Text),
                            PrivateName = privateNameTextBox.Text,
                            FamilyName = familyNameTextBox.Text,
                            PhoneNumber = phoneNumberTextBox.Text,
                            MailAddress = mailAddressTextBox.Text,
                            CollectionClearance = (bool)collectionClearanceCheckBox.IsChecked,
                           
                        },
                        HostingUnitName=hostingUnitNameTextBox.Text,
                        IsChildrenAttractions=(bool)kidsattCB.IsChecked,
                        IsGarden=(bool)gardenCB.IsChecked,
                        IsJacuzzi=(bool)jacuzziCB.IsChecked,
                        IsPool=(bool)poolCB.IsChecked,
                        Area = (Area)AreaCB.SelectedItem,
                        Type=(BE.Type)typeCB.SelectedItem,
                        Diary=new bool[12,31]
                    };
                    myBL.AddHostingUnit(hosting);
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
