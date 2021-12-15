using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDal
    {
        #region guest request
        void AddGuestRequest(BE.GuestRequest guestRequest);
        void UpdateGuestRequest(BE.GuestRequest guestRequest);
        #endregion

        #region hosting unit
        void AddHostingUnit(BE.HostingUnit hostingUnit);
        void DeleteHostingUnit(BE.HostingUnit hostingUnit);
        void UpdateHostingUnit(BE.HostingUnit hostingUnit);
        BE.GuestRequest GetGuestRequest(long key);
        BE.HostingUnit GetHostingUnit(long key);
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

    }
}
