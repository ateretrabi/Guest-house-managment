using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public class MyBL : IBL
    {
        public static DAL.IDal dal = DAL.FactoryDal.GetDal();

        


        #region guest request funcs
        /// <summary>
        /// checks for true number of nights and sets in list
        /// </summary>
        /// <param name="guestRequest"></param>
        public void AddGuestRequest(GuestRequest guestRequest)
        {
            try
            {
                if (guestRequest.EntryDate < guestRequest.ReleaseDate)
                {
                    guestRequest.NumOfNights = NumDaysBetweenDatesOrToday(guestRequest.EntryDate, guestRequest.ReleaseDate);
                    dal.AddGuestRequest(guestRequest);
                }
                    
                else
                    throw new MyException("release date in guest request: " + guestRequest + "happends before or during arrival (BL)\n");
            }
            catch (Exception e)
            {

                throw e;
            }
           

        }


        /// <summary>
        /// this func changes the stutus of a guest request
        /// </summary>
        /// <param name="guestRequest"></param>
        public void UpdateGuestRequest(GuestRequest guestRequest)
        {
            try
            {
                if (dal.GetGuestRequest(guestRequest.GuestRequestKey).Status == GuestRequestStatus.ADealWasClosedThroughTheWebsite)
                {
                    throw new MyException("it's not possible to update guest request: " + guestRequest + "beacause the deal is closed. (BL)\n");
                }
                else
                    dal.UpdateGuestRequest(guestRequest);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public GuestRequest getGuestRequest(long key)
        {
            try
            {
                return dal.GetGuestRequest(key);
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        /// <summary>
        /// this func returns a list guest requests that answers a condition
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public List<GuestRequest> GuestRequestList(Func<BE.GuestRequest, bool> predicat = null)
        {
            return dal.GuestRequestList(predicat);
        }
        #endregion

        #region hosting unit funcs
        /// <summary>
        /// this func adds a new hosting unit to the list
        /// </summary>
        /// <param name="hostingUnit"></param>
        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            try
            {
                if (HostingUnitList().Exists(x => (x.HostingUnitName == hostingUnit.HostingUnitName) && (x.Owner.HostKey == hostingUnit.Owner.HostKey)) == true) 
                {
                    throw new MyException("the hosting unit: " + hostingUnit + "already exists. (BL)\n");
                }
                dal.AddHostingUnit(hostingUnit);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        /// <summary>
        /// this func updates a hosting unit 
        /// </summary>
        /// <param name="hostingUnit"></param>
        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {
            try
            {
                dal.UpdateHostingUnit(hostingUnit);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        /// <summary>
        /// this func deletes a hostinh unit if no order relies on it
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <returns></returns>
        public void DeleteHostingUnit(HostingUnit hostingUnit)
        {
            try
            {
                var a = from item in dal.OrderList()
                        where (item.HostingUnitKey == hostingUnit.HostingUnitKey) && ((item.Status != BE.OrderStatus.NotYetAddressed) || (item.Status != BE.OrderStatus.SentAnEmail))
                        select item;
                
                if (a.Count()==0)
                {
                    dal.DeleteHostingUnit(hostingUnit);
                }

                //foreach (var item in dal.OrderList())
                //{
                //    if (item.HostingUnitKey == hostingUnit.HostingUnitKey)
                //    {
                //        if ((item.Status != BE.OrderStatus.NotYetAddressed) || (item.Status != BE.OrderStatus.SentAnEmail))
                //            dal.DeleteHostingUnit(hostingUnit);

                //    }
                //}

            }
            catch (Exception e)
            {

                throw e;
            }
            
            
        }

        /// <summary>
        /// this func returns a list of hosting unit that answers a condition
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public List<HostingUnit> HostingUnitList(Func<BE.HostingUnit, bool> predicat = null)
        {
            return dal.HostingUnitList(predicat);
        }

        #endregion

        #region order funcs

        /// <summary>
        /// adds a new order to data base
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            try
            {
                if (OrderList().Exists(x => (x.GuestRequestKey == order.GuestRequestKey) && (x.HostingUnitKey == order.HostingUnitKey)))
                {
                    throw new MyException("the order: " + order + "already exists. (BL)\n");
                } 
                dal.AddOrder(order);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        /// <summary>
        /// updates an order
        /// </summary>
        /// <param name="order"></param>
        public void UpdateOrder(Order order)
        {
            try
            {
                dal.UpdateOrder(order);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        /// <summary>
        /// returns a list of orders that answers a condition
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public List<Order> OrderList(Func<BE.Order, bool> predicat = null)
        {
            return dal.OrderList(predicat);
        }
        #endregion

        /// <summary>
        /// this func creates every order possible between a guest request and a hosting unit
        /// </summary>
        public void creatAllOrders()
        {
            try
            {
                for (int i = 0; i < dal.HostingUnitList().Count; i++)
                {
                    for (int j = 0; j < dal.GuestRequestList().Count; j++)
                    {
                        CreateOrder(dal.HostingUnitList()[i], dal.GuestRequestList()[j]);
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

       
       


        #region helpers
        /// <summary>
        /// help func check if to send an invite to guest
        /// </summary>
        /// <param name="hosting"></param>
        /// <returns></returns>
        public bool AbleToSendInvite(BE.HostingUnit hosting)
        {
           
            if (hosting.Owner.CollectionClearance == true)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// changes the status to sent email
        /// </summary>
        /// <param name="order"></param>
        public void SendInvite(BE.Order order)
        {
            try
            {
                if (AbleToSendInvite(dal.GetHostingUnit(order.HostingUnitKey)) == true)
                {
                    order.Status = BE.OrderStatus.SentAnEmail;
                    order.OrderDate = DateTime.Today;
                    Console.WriteLine(order.ToString());
                    UpdateOrder(order);
                }
                else
                    throw new MyException("it's not possible to invite for host to send an invitation because he won't pay the site. (BL)\n");
            }
            catch (Exception e)
            {

                throw e;
            }
            

        }

        /// <summary>
        /// check if the wanted days are free
        /// </summary>
        /// <param name="hosting"></param>
        /// <param name="guestRequest"></param>
        /// <returns></returns>
        public bool checkFreeDiary(BE.HostingUnit hosting, BE.GuestRequest guestRequest)
        {
            DateTime a = guestRequest.EntryDate;

            for (int i = 0; i < guestRequest.NumOfNights; i++)
            {
                if (hosting[a.AddDays(i)] == true)
                {
                    return false;
                }
            }
            return true;

        }

        /// <summary>
        /// days in diary are true now
        /// </summary>
        /// <param name="hosting"></param>
        /// <param name="guestRequest"></param>
        public void MarkDiary(BE.HostingUnit hosting, BE.GuestRequest guestRequest)
        {
            DateTime a = guestRequest.EntryDate;

            for (int i = 0; i < guestRequest.NumOfNights; i++)
            {
                hosting[a] = true;
            }
            
        }

        /// <summary>
        /// creates a new order and puts it in list of orders
        /// </summary>
        /// <param name="hosting"></param>
        /// <param name="guestRequest"></param>
        public void CreateOrder(BE.HostingUnit hosting, BE.GuestRequest guestRequest)
        {
            try
            {
                if (matchHostingUnithToGuestRequest(guestRequest, hosting))
                {
                    if (!(dal.OrderList().Exists(x=> x.GuestRequestKey==guestRequest.GuestRequestKey&&x.HostingUnitKey==hosting.HostingUnitKey)))
                    {
                        BE.Order order = new Order()
                        {
                            HostingUnitKey = hosting.HostingUnitKey,
                            GuestRequestKey = guestRequest.GuestRequestKey,
                            
                            Status = BE.OrderStatus.NotYetAddressed,
                            CreateDate = DateTime.Today,
                            EntryDate = guestRequest.EntryDate,
                            ReleaseDate = guestRequest.ReleaseDate,
                            GuestName = guestRequest.PrivateName + " " + guestRequest.FamilyName,
                            NumOfGuests = guestRequest.Adults + guestRequest.Children
                        };
                        dal.AddOrder(order);
                    }
                   
                }
                else
                    throw new MyException("it's not possible to create an order for hosting unit: " + hosting + "and guest reqest: " + guestRequest + "because they don't match (BL)\n");
            }
            catch (Exception e)
            {

                throw e;
            }
            
            
        }

        /// <summary>
        /// checks if unit fits request
        /// </summary>
        /// <param name="guestRequest"></param>
        /// <param name="hostingUnit"></param>
        /// <returns></returns>
        public bool matchHostingUnithToGuestRequest(BE.GuestRequest guestRequest, BE.HostingUnit hostingUnit)
        {
            if ((guestRequest.EntryDate >= DateTime.Today) && (guestRequest.Status != GuestRequestStatus.ADealWasClosedThroughTheWebsite) && (guestRequest.Status != GuestRequestStatus.ClosedDueToExpiration)) 
            {
                if (checkFreeDiary(hostingUnit, guestRequest))
                {
                    if (((guestRequest.Area == hostingUnit.Area) || (guestRequest.Area == BE.Area.All)) && (guestRequest.Type == hostingUnit.Type))
                    {
                        if (MatchEnumToBool(hostingUnit.IsPool, guestRequest.IsPool) && MatchEnumToBool(hostingUnit.IsJacuzzi, guestRequest.IsJacuzzi) && MatchEnumToBool(hostingUnit.IsGarden, guestRequest.IsGarden) && MatchEnumToBool(hostingUnit.IsChildrenAttractions, guestRequest.IsChildrenAttractions))
                        {
                            if (!(dal.OrderList().Exists(x => x.HostingUnitKey == hostingUnit.HostingUnitKey && x.GuestRequestKey == guestRequest.GuestRequestKey)))
                            {
                                return true;
                            }

                        }
                    }
                }
            }
            
            return false;
        }

        /// <summary>
        /// check match of bool to enum
        /// </summary>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public bool MatchEnumToBool(bool b, BE.Additions a)
        {
            if (a==BE.Additions.Possible)
            {
                return true;
            }
            if ((a==BE.Additions.Necessary)&&(b==true))
            {
                return true;
            }
            if ((a == BE.Additions.NotInterested) && (b == false))
            {
                return true;
            }
            return false;
        }

        
        public int CalculateCommission(int numOfNights)
        {
            int a = numOfNights * BE.Configuration.Commission;
            return a;
        }

        /// <summary>
        /// change all statuses of guest and close deal so set matrix of unit
        /// </summary>
        /// <param name="order"></param>
        public int CloseDeal(BE.Order order)
        {
            try
            {
                BE.HostingUnit h = dal.GetHostingUnit(order.HostingUnitKey);
                BE.GuestRequest g = dal.GetGuestRequest(order.GuestRequestKey);
                if (matchHostingUnithToGuestRequest(g, h))
                {
                    MarkDiary(h, g);
                }
                dal.UpdateHostingUnit(h);
                int com = CalculateCommission(g.NumOfNights);
                order.Commission = com;
                //send email to host to pay the site

                g.Status = BE.GuestRequestStatus.ADealWasClosedThroughTheWebsite;
                dal.UpdateGuestRequest(g);

                var guest = dal.GuestRequestList(x => x.PrivateName == g.PrivateName && x.FamilyName == g.FamilyName && x.EntryDate == g.EntryDate);
                for (int i = 0; i < guest.Count; i++)
                {
                    guest[i].Status = BE.GuestRequestStatus.ADealWasClosedThroughTheWebsite;
                    dal.UpdateGuestRequest(guest[i]);
                }

                order.Status = BE.OrderStatus.ClosedWithCustomerResponse;
                dal.UpdateOrder(order);
                return com;
            }
            catch (Exception e)
            {

                throw e;
            }
           
        
        }

        /// <summary>
        /// returns a list of units free in that time
        /// </summary>
        /// <param name="numNights"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<BE.HostingUnit> AvailableUnitInDate(int numNights, DateTime date)
        {
           var h= dal.HostingUnitList(x=> AvailableUnitInDate(numNights,date,x));

            return h;
        }

        /// <summary>
        /// checks if unit is free in wanted time
        /// </summary>
        /// <param name="numNights"></param>
        /// <param name="date"></param>
        /// <param name="hosting"></param>
        /// <returns></returns>
        public bool AvailableUnitInDate(int numNights, DateTime date, BE.HostingUnit hosting)
        {
            for (int i = 0; i < numNights; i++)
            {
                if (hosting[date.AddDays(i)] == true)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// number of days between tow dates or till today
        /// </summary>
        /// <param name="must"></param>
        /// <returns></returns>
        public int NumDaysBetweenDatesOrToday(params DateTime[] must)
        {
            int i = 0;

            if (must.Length>1)
            {
                
                for (; must[0] < must[1]; i++)
                {
                    must[0] = must[0].AddDays(1);
                    
                }
                return i;
            }

            DateTime a = DateTime.Today;
            
            for (; must[0] < a; i++)
            {
                must[0]=must[0].AddDays(1);

            }
            return i;

        }
          
        /// <summary>
        /// num of units inviting guest
        /// </summary>
        /// <param name="guestRequest"></param>
        /// <returns></returns>
        public int NumOfInvitesToRequest(BE.GuestRequest guestRequest)
        {
           var v= dal.OrderList(x => x.GuestRequestKey == guestRequest.GuestRequestKey&&x.Status==BE.OrderStatus.SentAnEmail);
            return v.Count;
        }

        /// <summary>
        /// all unit sent or closed with guest
        /// </summary>
        /// <param name="hosting"></param>
        /// <returns></returns>
        public int numOfGoodDealsOrInvites(BE.HostingUnit hosting)
        {
            var v = dal.OrderList(x => x.HostingUnitKey == hosting.HostingUnitKey &&( (x.Status == BE.OrderStatus.SentAnEmail)||(x.Status == BE.OrderStatus.ClosedWithCustomerResponse)));
            return v.Count;
        }

        public int numOfUnitsSameHost(BE.Host host)
        {
            var a = dal.HostingUnitList(x => x.Owner.HostKey == host.HostKey);
            return a.Count;
        }

        public List<Order> AllOrdersMoreDaysThanWanted(int numNights)
        {
           return dal.OrderList(x => (NumDaysBetweenDatesOrToday(x.CreateDate) >= numNights) || isOrderDate(numNights,x) );
            
        }

        public bool isOrderDate(int num, Order o)
        {
            DateTime defalt = new DateTime(2000, 1, 1);
            DateTime today = DateTime.Today;
            if (o.OrderDate!= defalt)
            {
                if (NumDaysBetweenDatesOrToday(o.OrderDate)>=num)
                {
                    return true;
                }
            }
            return false;
        }

        public int siteProfit()
        {
            int com = 0;
            foreach (var item in dal.OrderList())
            {
                com += item.Commission;
            }
            return com;
        }

        #endregion

        #region grouping
        public List<IGrouping<BE.Area, BE.GuestRequest>> GetAllGuestsGroupedByArea()
        {
           var a= from item in dal.GuestRequestList()
                               group item by item.Area into f1
                               select f1;

            return a.ToList();
        }

        public List<IGrouping<int, BE.GuestRequest>> GetAllGuestsGroupedByAmountOfPeople()
        {
            var a = from item in dal.GuestRequestList()
                    group item by item.Adults+item.Children into f1
                    select f1;

            return a.ToList();
        }
        public List<IGrouping<int, BE.Host>> GetAllHostsByNumOfUnitsTheyOwn()
        {
            var a = from item in dal.HostingUnitList()
                    group item.Owner by numOfUnitsSameHost(item.Owner) into f1
                    select f1;

            return a.ToList();
        }

        public List<IGrouping<BE.Area, BE.HostingUnit>> GetAllUnitsGroupedByArea()
        {
            var a = from item in dal.HostingUnitList()
                    group item by item.Area into f1
                    select f1;

            return a.ToList();
        }
        #endregion

        #region others
        public void fixOrders()
        {
            foreach (var item in OrderList())
            {
                if (item.EntryDate.AddDays(1) < DateTime.Today)
                {
                    item.Status = OrderStatus.ClosedOutOfCustomerunresponsiveness;
                }
            }
            foreach (var item in AllOrdersMoreDaysThanWanted(30))
            {
                if (item.Status!=OrderStatus.ClosedWithCustomerResponse)
                {
                    item.Status = OrderStatus.ClosedOutOfCustomerunresponsiveness;
                    UpdateOrder(item);
                }
            }
        }
        public void fixDiaries()
        {
            foreach (var item in HostingUnitList())
            {
                item[DateTime.Today.AddDays(-1)] = false;
            }
        }
        #endregion

    }
}
