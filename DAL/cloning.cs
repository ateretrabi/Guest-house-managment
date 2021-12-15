using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class cloning
    {
        public static BE.BankBranch clone(this BE.BankBranch org)
        {
            BE.BankBranch target = new BE.BankBranch()
            {
                BankName = org.BankName,
                BankNumber = org.BankNumber,
                BranchNumber = org.BranchNumber,
                BranchAddress = org.BranchAddress,
                BranchCity = org.BranchCity,
            };
            return target;
        }

        public static BE.GuestRequest clone(this BE.GuestRequest org)
        {
            BE.GuestRequest target = new BE.GuestRequest()
            {
                GuestRequestKey=org.GuestRequestKey,
                PrivateName=org.PrivateName,
                FamilyName=org.FamilyName,
                MailAddress=org.MailAddress,
                Status=org.Status,
                RegistrationDate=org.RegistrationDate,
                EntryDate=org.EntryDate,
                ReleaseDate=org.ReleaseDate,
                Area=org.Area,
                Type=org.Type,
                Adults=org.Adults,
                Children=org.Children,
                IsChildrenAttractions=org.IsChildrenAttractions,
                IsGarden=org.IsGarden,
                IsJacuzzi=org.IsJacuzzi,
                IsPool=org.IsPool
            };
            return target;
        }

        public static BE.Host clone(this BE.Host org)
        {
            BE.Host target = new BE.Host()
            {
               HostKey=org.HostKey,
               PrivateName=org.PrivateName,
               FamilyName=org.FamilyName,
               PhoneNumber=org.PhoneNumber,
               MailAddress=org.MailAddress,
               BankAccountNumber=org.BankAccountNumber,
               BankBranchDetails=org.BankBranchDetails,
               CollectionClearance=org.CollectionClearance
            };
            return target;
        }

        public static BE.HostingUnit clone(this BE.HostingUnit org)
        {
            BE.HostingUnit target = new BE.HostingUnit()
            {
                HostingUnitKey = org.HostingUnitKey,
                Owner = clone(org.Owner),
                HostingUnitName=org.HostingUnitName,
                Diary=new bool[12,31]
            };
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    target.Diary[i, j] = org.Diary[i, j];
                }
            }

            target.IsChildrenAttractions = org.IsChildrenAttractions;
            target.IsGarden = org.IsGarden;
            target.IsJacuzzi = org.IsJacuzzi;
            target.IsPool = org.IsPool;
            target.Area = org.Area;
            target.Type = org.Type;
            
            return target;
        }


        public static BE.Order clone(this BE.Order org)
        {
            BE.Order target = new BE.Order()
            {
                HostingUnitKey = org.HostingUnitKey,
                GuestRequestKey = org.GuestRequestKey,
                OrderDate = org.OrderDate,
                OrderKey = org.OrderKey,
                Status = org.Status,
                CreateDate = org.CreateDate,
                EntryDate=org.EntryDate,
                ReleaseDate=org.ReleaseDate,
                GuestName=org.GuestName, 
                NumOfGuests=org.NumOfGuests
                
                
            };
            return target;
        }
    }
}
