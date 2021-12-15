using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IBL myBL;
        //BackgroundWorker bankWorker;
        //List<BankBranch> bankBranches;
        //bool banksdownloaded = false;
        public MainWindow()
        {
            InitializeComponent();
            myBL = FactAndSIngBL.GetBL();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            myBL.fixOrders();
            myBL.fixDiaries();
            //bankWorker = new BackgroundWorker();
            //bankWorker.DoWork += BankWorker_DoWork;
            //bankWorker.RunWorkerCompleted += BankWorker_RunWorkerCompleted;

            //bankWorker.WorkerReportsProgress = true;
            ////bankWorker.RunWorkerAsync();
        }

        //private void BankWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    try
        //    {
        //        if (banksdownloaded)
        //        {
        //            string atmPath = @"atm.xml";
        //            XElement bankRoot = XElement.Load(atmPath);
        //            bankBranches = (from item in bankRoot.Elements()
        //                            select new BankBranch()
        //                            {
        //                                BankNumber = int.Parse(item.Element("קוד_בנק").Value),
        //                                BankName = item.Element("שם_בנק").Value,
        //                                BranchNumber = int.Parse(item.Element("קוד_סניף").Value),
        //                                BranchAddress = item.Element("כתובת_ה-ATM").Value,
        //                                BranchCity = item.Element("ישוב").Value
        //                            }).ToList();

        //        }
        //    }
        //    catch (Exception)
        //    {


        //    }
        //}

        //private void BankWorker_DoWork(object sender, DoWorkEventArgs e)
        //{

        //    const string xmlLocalPath = @"atm.xml";
        //    WebClient wc = new ExtendedTimeoutWebClient(TimeSpan.FromHours(1));
        //    //wc.DownloadProgressChanged += (newData) =>
        //    try
        //    {
        //        //press start Lets make sure that we enter the inner private class.. let put a break point and make sure that line 116 is calling our private class
        //        string xmlServerPath = @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
        //        wc.DownloadFile(xmlServerPath, xmlLocalPath);
        //        banksdownloaded = true;

        //    }
        //    catch (Exception)
        //    {
        //        try
        //        {
        //            string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
        //            wc.DownloadFile(xmlServerPath, xmlLocalPath);
        //            banksdownloaded = true;
        //        }
        //        catch (Exception)
        //        {


        //        }

        //    }
        //    finally
        //    {
        //        wc.Dispose();
        //    }

        //}




        private void siteOwnerButton_Click(object sender, RoutedEventArgs e)
        {
            new SiteOwnerWindow().ShowDialog();
        }

        private void hostingUnitButton_Click(object sender, RoutedEventArgs e)
        {
            new HostingUnitOptionsWindow().ShowDialog();
        }

        private void guestRequestButton_Click(object sender, RoutedEventArgs e)
        {
            new GuestRequestOptionsWindow().ShowDialog();
        }

        private void moreOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            new MoreOptionsWindow().ShowDialog();
        }
    }
}
