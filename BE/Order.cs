
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Order
    {
        private long myVp_HostingUnitKeyar;

        public long HostingUnitKey
        {
            get { return myVp_HostingUnitKeyar; }
            set { myVp_HostingUnitKeyar = value; }
        }
        private long p_GuestRequestKey;

        public long GuestRequestKey
        {
            get { return p_GuestRequestKey; }
            set { p_GuestRequestKey = value; }
        }
        private long p_OrderKey;

        public long OrderKey
        {
            get { return p_OrderKey; }
            set { p_OrderKey = value; }
        }

        public string GuestName { get; set; }
        public int NumOfGuests { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int Commission = 0;
        public OrderStatus Status { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime OrderDate = new DateTime(2000, 01, 01);

        public override string ToString()
        {
            string a = "hosting unit key: " + HostingUnitKey + ".\n";
            a += "guest request key: " + GuestRequestKey + ".\n";
            a += "order key: " + OrderKey + ".\n";
            a += "order status: " + Status + ".\n";
            a += "order creation: " + CreateDate.ToString() + ".\n";
            a+= "order date: "+OrderDate.ToString() + ".\n";
            return a;
        }
    }
}
