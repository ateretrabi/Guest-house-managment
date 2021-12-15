using BE;
using DS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL
{
    public class Dal_XML_imp : IDal
    {
        XElement orderRoot;
        XElement configRoot;
        //BackgroundWorker bankWorker;
        //List<BankBranch> bankBranches;
        //bool banksdownloaded = false;

        string configPath = @"config.xml";
        string guestRequestPath = @"guestRequest.xml";
        string hostingUnitPath = @"hostingUnit.xml";
        string orderPath = @"order.xml";

        private static Dal_XML_imp instance = null;

        public static Dal_XML_imp getDal_XML()
        {
            if (instance == null)
                instance = new Dal_XML_imp();
            return instance;
        }

        private Dal_XML_imp()
        {
            if (!File.Exists(orderPath))
                CreateFilesOrder();
            else
                LoadDataOrder();

            if (!File.Exists(configPath))
                CreateFilesCode();
            else
                LoadDataCode();

            if (!File.Exists(hostingUnitPath))
            {
                FileStream fileHostingUnit = new FileStream(hostingUnitPath, FileMode.Create);
                
                fileHostingUnit.Close();
            }
            else
                DataSource.hostingUnits = (loadListFromXML<HostingUnit>(hostingUnitPath));

            if (!File.Exists(guestRequestPath))
            {
                FileStream fileGuestRequest = new FileStream(guestRequestPath, FileMode.Create);
                fileGuestRequest.Close();
            }
            else
                DataSource.guestRequests = (loadListFromXML<GuestRequest>(guestRequestPath));

            saveListToXML<HostingUnit>(DataSource.hostingUnits, hostingUnitPath);
            saveListToXML<GuestRequest>(DataSource.guestRequests, guestRequestPath);

            //bankWorker = new BackgroundWorker();
            //bankWorker.DoWork += BankWorker_DoWork;
            //bankWorker.RunWorkerCompleted += BankWorker_RunWorkerCompleted;

            //bankWorker.WorkerReportsProgress = true;
            //bankWorker.RunWorkerAsync();


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

        private void LoadDataCode()
        {
            try
            {
                configRoot = XElement.Load(configPath);
            }
            catch
            {
                throw new MyException("File upload problem");
            }
        }

        private void LoadDataOrder()
        {
            try
            {
                orderRoot = XElement.Load(orderPath);
            }
            catch
            {
                throw new MyException("File upload problem");
            }
        }

        private void CreateFilesOrder()
        {
            orderRoot = new XElement("orders");
            orderRoot.Save(orderPath);
        }

        public static void saveListToXML<T>(List<T> list, string Path)
        {
            FileStream file = new FileStream(Path, FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }

        public static List<T> loadListFromXML<T>(string path)
        {
            if (File.Exists(path))
            {
                List<T> list;
                XmlSerializer x = new XmlSerializer(typeof(List<T>));
                FileStream file = new FileStream(path, FileMode.Open);
                list = (List<T>)x.Deserialize(file);
                file.Close();
                return list;
            }
            else return new List<T>();
        }

        private void CreateFilesCode()
        {
            XElement guestRequestKey = new XElement("GuestRequestKey", "10000000");
            XElement hostingKey = new XElement("HostingUnitKey", "20000000");
            XElement orderKey = new XElement("OrderKey", "30000000");
            configRoot = new XElement("Config", guestRequestKey, hostingKey, orderKey);
            configRoot.Save(configPath);
        }



        #region guestRequestFuncs
        void IDal.AddGuestRequest(GuestRequest guestRequest)
        {
            LoadDataCode();
            long guestKey = long.Parse(configRoot.Element("GuestRequestKey").Value);
            if (guestRequest.GuestRequestKey == 0)
            {
                guestRequest.GuestRequestKey = guestKey++;

                DataSource.guestRequests = (loadListFromXML<GuestRequest>(guestRequestPath));
                DataSource.guestRequests.Add(cloning.clone(guestRequest));
                saveListToXML<GuestRequest>(DataSource.guestRequests, guestRequestPath);
                configRoot.Element("GuestRequestKey").Value = guestKey.ToString();
                configRoot.Save(configPath);
                return;
            }
            var result = DS.DataSource.guestRequests.Exists(x => x.GuestRequestKey == guestRequest.GuestRequestKey);
            if (result == false)
            {
                DataSource.guestRequests = (loadListFromXML<GuestRequest>(guestRequestPath));
                DataSource.guestRequests.Add(cloning.clone(guestRequest));
                saveListToXML<GuestRequest>(DataSource.guestRequests, guestRequestPath);
                configRoot.Element("GuestRequestKey").Value = guestKey.ToString();
                configRoot.Save(configPath);
                return;

            }
            else if (result == true)
                throw new MyException("the guest request: " + guestRequest + "already exists in data base (DAL)\n");

        }

        GuestRequest IDal.GetGuestRequest(long key)
        {
            DataSource.guestRequests = (loadListFromXML<GuestRequest>(guestRequestPath));
            int index = DS.DataSource.guestRequests.FindIndex(t => t.GuestRequestKey == key);
            if (index == -1)
            {
                throw new MyException("there is no guest request with the key: " + key + " (DAl)\n");
            }
            BE.GuestRequest a = cloning.clone(DS.DataSource.guestRequests[index]);
            return a;
        }

        List<GuestRequest> IDal.GuestRequestList(Func<GuestRequest, bool> predicat)
        {

            DataSource.guestRequests = (loadListFromXML<GuestRequest>(guestRequestPath));
            var a = from item in DS.DataSource.guestRequests
                    select cloning.clone(item);

            if (predicat == null)
            {
                return a.ToList();
            }


            return a.Where(predicat).ToList();
        }
        void IDal.UpdateGuestRequest(GuestRequest guestRequest)
        {
            DataSource.guestRequests = (loadListFromXML<GuestRequest>(guestRequestPath));
            bool found = false;
            LoadDataCode();
            long guestKey = long.Parse(configRoot.Element("GuestRequestKey").Value);
            if (guestRequest.GuestRequestKey == 0)
            {
                guestRequest.GuestRequestKey = guestKey++;
                configRoot.Element("GuestRequestKey").Value = guestKey.ToString();
                configRoot.Save(configPath);
            }
            foreach (var item in DS.DataSource.guestRequests)
            {

                if (item.GuestRequestKey == guestRequest.GuestRequestKey)
                {
                    item.Status = guestRequest.Status;
                    found = true;
                }
            }
            if (found == false)
            {
                throw new MyException("guest request: " + guestRequest + "was not found (DAL)\n");
            }
        }
        #endregion

        #region hostingUnitFuncs
        void IDal.AddHostingUnit(HostingUnit hostingUnit)
        {
            LoadDataCode();
            long hostingKey = long.Parse(configRoot.Element("HostingUnitKey").Value);
            DataSource.hostingUnits = (loadListFromXML<HostingUnit>(hostingUnitPath));
            if (hostingUnit.HostingUnitKey == 0)
            {
                hostingUnit.HostingUnitKey = hostingKey++;
            }
            var a = DS.DataSource.hostingUnits.Exists((x) => x.HostingUnitKey == hostingUnit.HostingUnitKey);

            if (!a)
            {
                DS.DataSource.hostingUnits.Add(cloning.clone(hostingUnit));
                saveListToXML<HostingUnit>(DataSource.hostingUnits, hostingUnitPath);
                configRoot.Element("HostingUnitKey").Value = hostingKey.ToString();
                configRoot.Save(configPath);
            }
            else
                throw new MyException("This hosting unit:" + hostingUnit + " already exists. (DAL)\n");
        }

        void IDal.DeleteHostingUnit(HostingUnit hostingUnit)
        {
            GetOrdersList();
            DataSource.hostingUnits = (loadListFromXML<HostingUnit>(hostingUnitPath));
            DataSource.orders = (loadListFromXML<Order>(orderPath));
            var b = from item in DS.DataSource.orders
                    where ((item.HostingUnitKey == hostingUnit.HostingUnitKey) && ((item.Status == BE.OrderStatus.NotYetAddressed) || (item.Status == BE.OrderStatus.SentAnEmail)))
                    select item;

            if (b.Count() == 0)
            {
                var c = from item in DS.DataSource.hostingUnits
                        where (item.HostingUnitKey != hostingUnit.HostingUnitKey)
                        select item;
                DS.DataSource.hostingUnits = c.ToList();
                saveListToXML<HostingUnit>(DataSource.hostingUnits, hostingUnitPath);
            }
            else
                throw new MyException("its not possible to delete hosting unit: " + hostingUnit.HostingUnitName + "\n" + "there is an open order connected to it (DAL)\n");
        }

        HostingUnit IDal.GetHostingUnit(long key)
        {
            DataSource.hostingUnits = (loadListFromXML<HostingUnit>(hostingUnitPath));
            int index = DS.DataSource.hostingUnits.FindIndex(t => t.HostingUnitKey == key);
            if (index == -1)
            {
                throw new MyException("there is no hostging unit with the key:" + key + " (DAL)\n");
            }
            BE.HostingUnit a = cloning.clone(DS.DataSource.hostingUnits[index]);
            return a;
        }
        List<HostingUnit> IDal.HostingUnitList(Func<HostingUnit, bool> predicat)
        {
            DataSource.hostingUnits = (loadListFromXML<HostingUnit>(hostingUnitPath));
            var a = from item in DS.DataSource.hostingUnits
                    select cloning.clone(item);

            if (predicat == null)
            {
                return a.ToList();
            }
            return a.Where(predicat).ToList();
        }

        void IDal.UpdateHostingUnit(HostingUnit hostingUnit)
        {
            LoadDataCode();
            long hostingKey = long.Parse(configRoot.Element("HostingUnitKey").Value);
            DataSource.hostingUnits = (loadListFromXML<HostingUnit>(hostingUnitPath));

            bool found = false;
            if (hostingUnit.HostingUnitKey == 0)
            {
                hostingUnit.HostingUnitKey = hostingKey++;
                configRoot.Element("HostingUnitKey").Value = hostingKey.ToString();
                configRoot.Save(configPath);

            }
            for (int i = 0; i < DS.DataSource.hostingUnits.Count; i++)
            {

                if (DS.DataSource.hostingUnits[i].HostingUnitKey == hostingUnit.HostingUnitKey)
                {
                    found = true;

                    for (int k = 0; k < 12; k++)
                    {
                        for (int j = 0; j < 31; j++)
                        {
                            DS.DataSource.hostingUnits[i].Diary[k, j] = hostingUnit.Diary[k, j];
                        }
                    }
                    DS.DataSource.hostingUnits[i].Area = hostingUnit.Area;
                    DS.DataSource.hostingUnits[i].HostingUnitName = hostingUnit.HostingUnitName;
                    DS.DataSource.hostingUnits[i].Type = hostingUnit.Type;
                    DS.DataSource.hostingUnits[i].IsPool = hostingUnit.IsPool;
                    DS.DataSource.hostingUnits[i].IsJacuzzi = hostingUnit.IsJacuzzi;
                    DS.DataSource.hostingUnits[i].IsGarden = hostingUnit.IsGarden;
                    DS.DataSource.hostingUnits[i].IsChildrenAttractions = hostingUnit.IsChildrenAttractions;
                    DS.DataSource.hostingUnits[i].Owner = hostingUnit.Owner;
                    changeCllectionClearence(hostingUnit, i);
                }

            }
            if (found == false)
            {
                throw new MyException("hostin unit: " + hostingUnit + "was not found (DAL)\n");
            }

            saveListToXML<HostingUnit>(DataSource.hostingUnits, hostingUnitPath);
            
        }
        public void changeCllectionClearence(BE.HostingUnit hostingUnitFrom, int i)
        {
            DS.DataSource.hostingUnits[i].Owner = hostingUnitFrom.Owner;
        }
        #endregion

        #region orderFuncs
        void IDal.AddOrder(Order order)
        {
            GetOrdersList();
            LoadDataCode();
            long orderKey = long.Parse(configRoot.Element("OrderKey").Value);

            if (order.OrderKey == 0)
            {
                order.OrderKey = orderKey++;
                configRoot.Element("OrderKey").Value = orderKey.ToString();
                configRoot.Save(configPath);
            }
            var result = DS.DataSource.orders.Exists(x => (x.GuestRequestKey == order.GuestRequestKey) && (x.HostingUnitKey == order.HostingUnitKey));
            if (result == false)
            {
                DS.DataSource.orders.Add(cloning.clone(order));
                SaveOrderistLinq(DataSource.orders);
            }

            else if (result == true)
                throw new MyException("the order: " + order + "already exists (DAL)\n");
        }
        public void SaveOrderistLinq(List<Order> orders)
        {
            orderRoot = new XElement("Orders",
            from p in orders
            select new XElement("order",
            new XElement("HostingUnitKey", p.HostingUnitKey),
            new XElement("GuestRequestKey", p.GuestRequestKey),
            new XElement("OrderKey", p.OrderKey),
            new XElement("GuestName", p.GuestName),
            new XElement("NumOfGuests", p.NumOfGuests),
            new XElement("EntryDate", p.EntryDate),
            new XElement("ReleaseDate", p.ReleaseDate),
            new XElement("CreateDate", p.CreateDate),
            new XElement("OrderDate", p.OrderDate),
            new XElement("Status", p.Status),
            new XElement("Commission", p.Commission)
            )
            );
            orderRoot.Save(orderPath);
        }
        public void GetOrdersList()
        {
            LoadDataOrder();
            List<Order> orders;
            try
            {
                orders = (from p in orderRoot.Elements()
                          select new Order()
                          {
                              HostingUnitKey = long.Parse(p.Element("HostingUnitKey").Value),
                              GuestRequestKey = long.Parse(p.Element("GuestRequestKey").Value),
                              OrderKey = long.Parse(p.Element("OrderKey").Value),
                              GuestName = p.Element("GuestName").Value,
                              NumOfGuests = int.Parse(p.Element("NumOfGuests").Value),
                              EntryDate=(DateTime)p.Element("EntryDate"),
                              CreateDate = (DateTime)p.Element("CreateDate"),
                              ReleaseDate = (DateTime)p.Element("ReleaseDate"),
                              OrderDate = (DateTime)p.Element("OrderDate"),
                              Status =(OrderStatus) Enum.Parse(typeof(OrderStatus), p.Element("Status").Value),
                              Commission = int.Parse( p.Element("Commission").Value),
                             
                          }).ToList();
            }
            catch
            {
                orders = null;
            }
            DataSource.orders = orders;
        }

        List<Order> IDal.OrderList(Func<Order, bool> predicat)
        {
            GetOrdersList();
            var a = from item in DS.DataSource.orders
                    select cloning.clone(item);

            if (predicat == null)
            {
                return a.ToList();
            }


            return a.Where(predicat).ToList();
        }

        void IDal.UpdateOrder(Order order)
        {
            GetOrdersList();
            LoadDataCode();
            long orderKey = long.Parse(configRoot.Element("OrderKey").Value);
            bool found = false;
            if (order.OrderKey == 0)
            {
                order.OrderKey = orderKey++;
                configRoot.Element("OrderKey").Value = orderKey.ToString();
                configRoot.Save(configPath);
            }
            foreach (var item in DS.DataSource.orders)
            {
                if (item.OrderKey == order.OrderKey)
                {
                    item.Status = order.Status;
                    found = true;
                }
            }
            if (found == false)
            {
                throw new MyException("the order: " + order + "was not found (DAL)\n");
            }
            SaveOrderistLinq(DataSource.orders);
        }

        #endregion
















        private class ExtendedTimeoutWebClient : WebClient
        {
            public TimeSpan Timeout { get; set; }

            public ExtendedTimeoutWebClient(TimeSpan timeout)
            {
                this.Timeout = timeout;
            }

            protected override WebRequest GetWebRequest(Uri uri)
            {
                WebRequest w = base.GetWebRequest(uri);
                w.Timeout = (int)this.Timeout.TotalMilliseconds;
                return w;
            }
        }
    }
}
