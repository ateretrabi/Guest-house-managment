using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class GuestRequest
    {
        #region names and keys
        private string p_PrivateName;

        public string PrivateName
        {
            get { return p_PrivateName; }
            set { p_PrivateName = value; }
        }

        private string p_FamilyName;

        public string FamilyName
        {
            get { return p_FamilyName; }
            set { p_FamilyName = value; }
        }

        public string MyfullNameProperty { get { return PrivateName + " " + FamilyName; } }

        private string p_MailAddress;

        public string MailAddress
        {
            get { return p_MailAddress; }
            set { p_MailAddress = value; }
        }

        //unique number for every customer
        private long p_GuestRequestKey;

        public long GuestRequestKey
        {
            get { return p_GuestRequestKey; }
            set { p_GuestRequestKey = value; }
        }
        #endregion

        #region dates
        private DateTime p_RegistrationDate;

        public DateTime RegistrationDate
        {
            get { return p_RegistrationDate; }
            set { p_RegistrationDate = value; }
        }


        private DateTime p_EntryDate;

        public  DateTime EntryDate
        {
            get { return p_EntryDate; }
            set { p_EntryDate = value; }
        }

        private DateTime p_ReleaseDate;

        public DateTime ReleaseDate
        {
            get { return p_ReleaseDate; }
            set { p_ReleaseDate = value; }
        }

        public int NumOfNights;
        #endregion
        public GuestRequestStatus Status { get; set; }
     
        public Type Type { get; set; }
        public Area Area { get; set; }
       

        public int Adults;
        public int Children;
        public int numOfGuests { get { return Adults + Children; } }

        public Additions IsPool;
        public Additions IsJacuzzi;
        public Additions IsGarden;
        public Additions IsChildrenAttractions;

        public override string ToString()
        {
            string a ="guest request key: "+GuestRequestKey+ "\nguest name: " + PrivateName + " " + FamilyName + ".\n";
            a += "status: " + Status.ToString() + ".\n" + "registration date: " + RegistrationDate.ToString("dd.mm.yyyy") + ".\n";
            a += "entry date: " + EntryDate.ToString("dd.mm.yyyy") + ".\n" + "release date: " + ReleaseDate.ToString("dd.mm.yyyy") + ".\n";
            a += "area: " + Area.ToString() + ".\n" + "hostel type: " + Type.ToString() + ".\n" + "number of adults: " + Adults + ".\n" + "number of children: " + Children + ".\n";
            a += "would like a pool? " + IsPool.ToString() + ".\n" + "would like a jacuzzi? " + IsJacuzzi.ToString() + ".\n" + "would like a garden? " + IsGarden.ToString() + ".\n" + "would like children's attractions? " + IsChildrenAttractions.ToString() + ".\n";

            return a;
        }
        
    }
}
