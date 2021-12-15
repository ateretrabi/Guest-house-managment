using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    [Serializable]
    public class HostingUnit
    {
        #region HostingUnitKey
        private long p_HostingUnitKey;

        public long HostingUnitKey
        {
            get { return p_HostingUnitKey; }
            set { p_HostingUnitKey = value; }
        }
        #endregion

        #region Owner
        //public Host Owner { get; set; }
        private Host _owner;

        public Host Owner

        {
            get { return _owner; }
            set { _owner = value; }
        }


        #endregion

        #region HostingUnitName
        private string p_HostingUnitName;

        public string HostingUnitName
        {
            get { return p_HostingUnitName; }
            set
            {
                Configuration.isValidName(value);
                p_HostingUnitName = value; 
            }
        }
        #endregion

        #region additions
        public Type Type { get; set; }
        public Area Area { get; set; }
        public bool IsPool { get; set; }
        public bool IsJacuzzi { get; set; }
        public bool IsGarden { get; set; }
        public bool IsChildrenAttractions { get; set; }
        #endregion

        [XmlIgnore]
        public bool[,] Diary;

        [XmlArray("Diary")]
        public bool[] DiaryDto
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(12); } //5 is the number of rows in the matrix
        }


        public override string ToString()
        {
            string a = "hosting unit key: " + HostingUnitKey + "\nhosting unit name: " + HostingUnitName + ".\n";
            a += Owner.ToString();
            if (IsPool)
            {
                a += "includes a pool\n";
            }
            if (IsJacuzzi)
            {
                a += "includes a jacuzzi\n";
            }
            if (IsGarden)
            {
                a += "includes a garden\n";
            }
            if (IsChildrenAttractions)
            {
                a += "includes children attractions\n";
            }
            a += "area: " + Area.ToString() + ".\n" + "hostel type: " + Type.ToString() + ".\n";
            return a;
        }

       

        //indexer
        public bool this[DateTime index]
        {
            get { return Diary[index.Month - 1, index.Day - 1]; }
            set { Diary[index.Month - 1, index.Day - 1] = value; }
        }
    }

}
