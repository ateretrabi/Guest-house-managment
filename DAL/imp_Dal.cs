using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    class imp_Dal :IDal
    {
        #region guest request
        public void AddGuestRequest(BE.GuestRequest guestRequest)
        {
            if (guestRequest.GuestRequestKey==0)
            {
                guestRequest.GuestRequestKey = BE.Configuration.GuestRequestKey++;
                DS.DataSource.guestRequests.Add(cloning.clone(guestRequest));
                return;
            }
            var result = DS.DataSource.guestRequests.Exists(x => x.GuestRequestKey == guestRequest.GuestRequestKey);
            if (result == false)
                DS.DataSource.guestRequests.Add(cloning.clone(guestRequest));
            else if (result==true)
                throw new MyException("the guest request: " + guestRequest + "already exists in data base (DAL)\n");

        }
        
        public void UpdateGuestRequest(BE.GuestRequest guestRequest)
        {
            bool found = false;
            if (guestRequest.GuestRequestKey==0)
            {
                guestRequest.GuestRequestKey = Configuration.GuestRequestKey++;
            }
            foreach (var item in DS.DataSource.guestRequests)
            {
               
                if (item.GuestRequestKey == guestRequest.GuestRequestKey)
                {
                    item.Status = guestRequest.Status;
                    found = true;
                }
            }
            if (found==false)
            {
                throw new MyException("guest request: " + guestRequest + "was not found (DAL)\n");
            }
        }

        public BE.GuestRequest GetGuestRequest(long key)
        {
            int index = DS.DataSource.guestRequests.FindIndex(t => t.GuestRequestKey == key);
            if (index==-1)
            {
                throw new MyException("there is no guest request with the key: " + key + " (DAl)\n");
            }
            BE.GuestRequest a = cloning.clone(DS.DataSource.guestRequests[index]);
            return a;
        }

        #endregion

        #region HostingUnit
        public void AddHostingUnit(BE.HostingUnit hostingUnit)
        {
            if (hostingUnit.HostingUnitKey==0)
            {
                hostingUnit.HostingUnitKey = Configuration.HostingUnitKey++;
            }
            var a = DS.DataSource.hostingUnits.Exists((x) => x.HostingUnitKey == hostingUnit.HostingUnitKey);

            if (!a)
                DS.DataSource.hostingUnits.Add(cloning.clone(hostingUnit));
            else
                throw new MyException("This hosting unit:"+hostingUnit+" already exists. (DAL)\n");
        }
        public void DeleteHostingUnit(BE.HostingUnit hostingUnit)
        {
            var b = from item in DS.DataSource.orders
                    where ((item.HostingUnitKey == hostingUnit.HostingUnitKey) && (item.Status != BE.OrderStatus.NotYetAddressed) && (item.Status != BE.OrderStatus.SentAnEmail))
                    select item;
            
            if (b.Count() == 0)
            {
                var c = from item in DS.DataSource.hostingUnits
                        where (item.HostingUnitKey != hostingUnit.HostingUnitKey)
                        select item;
                DS.DataSource.hostingUnits = c.ToList();
                
            }
            else
                throw new MyException("its not possible to delete hosting unit: " + hostingUnit.HostingUnitName+"\n" + "there is an open order connected to it (DAL)\n");
            
            
           

        }
        public void UpdateHostingUnit(BE.HostingUnit hostingUnit)
        {
            bool found = false;
            if (hostingUnit.HostingUnitKey==0)
            {
                hostingUnit.HostingUnitKey = Configuration.HostingUnitKey++;
            }
            for (int i = 0; i < DS.DataSource.hostingUnits.Count; i++)
            {
                
                if (DS.DataSource.hostingUnits[i].HostingUnitKey==hostingUnit.HostingUnitKey)
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
            if (found==false)
            {
                throw new MyException("hostin unit: " + hostingUnit + "was not found (DAL)\n");
            }
            
            
            
        }
        public void changeCllectionClearence(BE.HostingUnit hostingUnitFrom, int i)
        {
            DS.DataSource.hostingUnits[i].Owner = hostingUnitFrom.Owner;
        }

        public BE.HostingUnit GetHostingUnit(long key)
        {
            int index = DS.DataSource.hostingUnits.FindIndex(t => t.HostingUnitKey == key);
            if (index==-1)
            {
                throw new MyException("there is no hostging unit with the key:" + key + " (DAL)\n");
            }
            BE.HostingUnit a = cloning.clone(DS.DataSource.hostingUnits[index]);
            return a;

        }

        #endregion

        #region Order
        public void AddOrder(BE.Order order)
        {
            if (order.OrderKey==0)
            {
                order.OrderKey = Configuration.OrderKey++;
            }
            var result = DS.DataSource.orders.Exists(x => (x.GuestRequestKey == order.GuestRequestKey)&&(x.HostingUnitKey==order.HostingUnitKey));
            if (result == false)
                DS.DataSource.orders.Add(cloning.clone(order));
            else if (result == true) 
                throw new MyException("the order: " + order + "already exists (DAL)\n");
        }
        public void UpdateOrder(BE.Order order)
        {
            bool found = false;
            if (order.OrderKey==0)
            {
                order.OrderKey = Configuration.OrderKey++;
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
        }
        #endregion

        #region lists
        public List<BE.HostingUnit> HostingUnitList(Func<BE.HostingUnit, bool> predicat = null)
        {
            var a = from item in DS.DataSource.hostingUnits
                    select cloning.clone(item);
            if (predicat==null)
            {
                return a.ToList();
            }

            
            return a.Where(predicat).ToList();
        }

        public List<BE.GuestRequest> GuestRequestList(Func<BE.GuestRequest, bool> predicat = null)
        {

            var a = from item in DS.DataSource.guestRequests
                    select cloning.clone(item);

            if (predicat == null)
            {
                return a.ToList();
            }


            return a.Where(predicat).ToList();
        }

        public List<BE.Order> OrderList(Func<BE.Order, bool> predicat = null)
        {
            var a = from item in DS.DataSource.orders
                    select cloning.clone(item);

            if (predicat == null)
            {
                return a.ToList();
            }


            return a.Where(predicat).ToList();
        }

        public List<BE.BankBranch> BankBranchList(Func<BE.BankBranch, bool> predicat = null)
        {
            List<BE.BankBranch> a = new List<BE.BankBranch>()
            {
                new BE.BankBranch()
                {
                    BankName="דיסקונט",
                        BranchAddress="יפו 220",
                        BankNumber=11,
                        BranchCity="ירושלים",
                        BranchNumber=982
                },
                new BE.BankBranch()
                {
                      BankName="לאומי",
                        BranchAddress="רוטשילד 64",
                        BankNumber=10,
                        BranchCity="פתח תקווה",
                        BranchNumber=984
                },
                new BE.BankBranch()
                {
                     BankName="אוצר החייל",
                        BranchAddress="אלוף דוד 187",
                        BankNumber=14,
                        BranchCity="רמת גן",
                        BranchNumber=986
                },
                new BE.BankBranch()
                {
                    BankName="מזרחי טפחות",
                    BankNumber=20,
                    BranchAddress="התבור 7",
                    BranchCity="ראשון לציון",
                    BranchNumber=987
                },
                new BE.BankBranch()
                {
                    BankName="בנק הפועלים",
                    BankNumber=12,
                    BranchAddress="זבוטינסקי 7",
                    BranchCity="ראשון לציון",
                    BranchNumber=943
                }
               
            };
            var t = from item in a
                    select cloning.clone(item);

            if (predicat == null)
            {
                return a.ToList();
            }


            return a.Where(predicat).ToList();
         
        }
        #endregion
    }
}
