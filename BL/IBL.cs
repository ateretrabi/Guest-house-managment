using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {

        #region guest request
        void AddGuestRequest(BE.GuestRequest guestRequest);
        void UpdateGuestRequest(BE.GuestRequest guestRequest);
        GuestRequest getGuestRequest(long key);
        #endregion

        #region hosting unit
        void AddHostingUnit(BE.HostingUnit hostingUnit);
        void DeleteHostingUnit(BE.HostingUnit hostingUnit);
        void UpdateHostingUnit(BE.HostingUnit hostingUnit);
        #endregion

        #region order
        void AddOrder(BE.Order order);
        void UpdateOrder(BE.Order order);
        #endregion

        #region lists
        /// <summary>
        /// this function returns a list with all hosting units
        /// </summary>
        /// <returns></returns>
        List<BE.HostingUnit> HostingUnitList(Func<BE.HostingUnit, bool> predicat = null);

        /// <summary>
        ///  this function returns a list with all guest request 
        /// </summary>
        /// <returns></returns>
        List<BE.GuestRequest> GuestRequestList(Func<BE.GuestRequest, bool> predicat = null);

        /// <summary>
        /// this function returns a list with all orders 
        /// </summary>
        /// <returns></returns>
        List<BE.Order> OrderList(Func<BE.Order, bool> predicat = null);

       
        #endregion

        #region helpers
        bool AbleToSendInvite(BE.HostingUnit hosting);
        void SendInvite(BE.Order order);
        bool checkFreeDiary(BE.HostingUnit hosting, BE.GuestRequest guestRequest);
        void MarkDiary(BE.HostingUnit hosting, BE.GuestRequest guestRequest);
        void CreateOrder(BE.HostingUnit hosting, BE.GuestRequest guestRequest);

        bool matchHostingUnithToGuestRequest(BE.GuestRequest guestRequest, BE.HostingUnit hostingUnit);
        bool MatchEnumToBool(bool b, BE.Additions a);
        int CalculateCommission(int numOfNights);
        int CloseDeal(BE.Order order);
        List<BE.HostingUnit> AvailableUnitInDate(int numNights, DateTime date);
        bool AvailableUnitInDate(int numNights, DateTime date, BE.HostingUnit hosting);
        int NumDaysBetweenDatesOrToday(params DateTime[] must);
        int NumOfInvitesToRequest(BE.GuestRequest guestRequest);
        int numOfGoodDealsOrInvites(BE.HostingUnit hosting);
        int numOfUnitsSameHost(BE.Host host);
        void creatAllOrders();
        List<BE.Order> AllOrdersMoreDaysThanWanted(int numNights);
        bool isOrderDate(int num, BE.Order o);

        int siteProfit();

        void fixOrders();
        void fixDiaries();
        #endregion

        #region grouping
        List<IGrouping<BE.Area, BE.GuestRequest>> GetAllGuestsGroupedByArea();
        List<IGrouping<int, BE.GuestRequest>> GetAllGuestsGroupedByAmountOfPeople();
        List<IGrouping<int, BE.Host>> GetAllHostsByNumOfUnitsTheyOwn();
        List<IGrouping<BE.Area, BE.HostingUnit>> GetAllUnitsGroupedByArea();
        #endregion

        
    }
}
