using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   
    #region enums
        public enum Area
        {
            All, North, South, Center, Jerusalem
        }

        public enum Type
        {
            Zimmer, Hotel, Camping, Apartment
        }

        public enum OrderStatus
        {
            NotYetAddressed, SentAnEmail, ClosedOutOfCustomerunresponsiveness, ClosedWithCustomerResponse
        }

        public enum GuestRequestStatus
        {
            Active, ClosedDueToExpiration, ADealWasClosedThroughTheWebsite
        }

        public enum Additions
        {
            Necessary, Possible, NotInterested
        }
    #endregion

    #region structs
    public struct Host
    {
        #region HostKey
        private long p_HostKey;

        public long HostKey
        {
            get { return p_HostKey; }
            set 
            {
                Configuration.isValidId(value.ToString());
                p_HostKey = value;
            }
        }
        #endregion


        #region PrivateName
        private string p_PrivateName;

        public string PrivateName
        {
            get { return p_PrivateName; }
            set 
            {
                Configuration.isValidName(value);
                p_PrivateName = value;
            }
        }
        #endregion

        #region FamilyName
        private string p_FamilyName;

        public string FamilyName
        {
            get { return p_FamilyName; }
            set 
            {
                Configuration.isValidName(value);
                p_FamilyName = value; 
            }
        }
        #endregion
        public string fullName { get { return PrivateName + " " + FamilyName; } }

        #region PhoneNumber
        private string p_FhoneNumber;

        public string PhoneNumber
        {
            get { return p_FhoneNumber; }
            set
            {
                Configuration.isValidCell(value);
                p_FhoneNumber = value;
            }
        }
        #endregion

        #region MailAddress
        private string p_MailAddress;

        public string MailAddress
        {
            get { return p_MailAddress; }
            set
            {
                Configuration.isValidEmail(value);
                p_MailAddress = value;
            }
        }
        #endregion

        #region bankAccount
        public BankBranch BankBranchDetails { get; set; }

        private long p_BankAccountNumber;

        public long BankAccountNumber
        {
            get { return p_BankAccountNumber; }
            set { p_BankAccountNumber = value; }
        }

       


        #endregion


        // public bool CollectionClearance;
        public bool CollectionClearance { get; set; }
        public override string ToString()
        {
            string a = "host's details: ID:" + HostKey + ", name: " + PrivateName + " " + FamilyName + ".\n";
            a += "phone number: " + PhoneNumber + ".\n";
            a += "Email address: " + MailAddress + ".\n";
            a += BankBranchDetails.ToString();
            a += "bank account number: " + BankAccountNumber + ".\n";
            a += "collection clearance: " + CollectionClearance + ".\n";

            return a;
        }
    }

    public struct BankBranch
        {
            private int p_BankNumber;
            public int BankNumber
            {
                get { return p_BankNumber; }
                set { p_BankNumber = value; }
            }


            private string p_BankName;
            public string BankName
            {
                get { return p_BankName; }
                set { p_BankName = value; }
            }


            private int p_BranchNumber;
            public int BranchNumber
            {
                get { return p_BranchNumber; }
                set { p_BranchNumber = value; }
            }


            private string p_BranchAddress;
            public string BranchAddress
            {
                get { return p_BranchAddress; }
                set { p_BranchAddress = value; }
            }


            private string p_BranchCity;
            public string BranchCity
            {
                get { return p_BranchCity; }
                set { p_BranchCity = value; }
            }


            public override string ToString()
            {
                string a = "bank: " + BankNumber + ", " + BankName + ".\n";
                a += "bank address: " + BranchAddress + ", " + BranchCity + ".\n";


                return a;
            }
        }

    public struct SiteOwner
    {
        public long password;
    }

    #endregion

    public static class Tools
    {
        public static T[] Flatten<T>(this T[,] arr)
        {
            int rows = arr.GetLength(0);
            int columns = arr.GetLength(1);
            T[] arrFlattened = new T[rows * columns];
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    var test = arr[j, i];
                    arrFlattened[i * rows + j] = arr[j, i];
                }
            }
            return arrFlattened;
        }
        public static T[,] Expand<T>(this T[] arr, int rows)
        {
            int length = arr.GetLength(0);
            int columns = length / rows;
            T[,] arrExpanded = new T[rows, columns];
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    arrExpanded[j, i] = arr[i * rows + j];
                }
            }
            return arrExpanded;
        }
    }
   

}
