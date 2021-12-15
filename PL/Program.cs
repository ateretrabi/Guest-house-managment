using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace PL
{
    class Program
    {
        public static BL.IBL bl = BL.FactAndSIngBL.GetBL();

        private static void ProtectedInvocation(Action method)
        {
            try
            {
                method();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            #region hosting units
            HostingUnit neveGalil = new HostingUnit()
            {
                HostingUnitKey = BE.Configuration.HostingUnitKey++,
                Owner = new BE.Host()
                {
                    HostKey = 322807694,
                    PrivateName = "Yossi",
                    FamilyName = "Cohen",
                    PhoneNumber = "0509876543",
                    MailAddress = "yossic@gmail.com",
                    BankBranchDetails = new BE.BankBranch()
                    {
                        BankName = "discont",
                        BranchAddress = "Jaffa 220",
                        BankNumber = 11,
                        BranchCity = "Jerusalem",
                        BranchNumber = 982

                    },
                    BankAccountNumber = 123456,
                    CollectionClearance = true
                },
                HostingUnitName = "Neve Hagalil",

                Diary = new bool[12, 31],
                IsChildrenAttractions = true,
                Area = Area.Center,
                Type = BE.Type.Zimmer,
                IsPool = true,
                IsJacuzzi = false,
                IsGarden = true
            };
            HostingUnit shomron = new HostingUnit()
            {
                HostingUnitKey = BE.Configuration.HostingUnitKey++,
                Owner = new BE.Host()
                {
                    HostKey = 322347694,
                    PrivateName = "Chaim",
                    FamilyName = "Levi",
                    PhoneNumber = "0509878943",
                    MailAddress = "chaimlevic@gmail.com",
                    BankBranchDetails = new BE.BankBranch()
                    {
                        BankName = "leumi",
                        BranchAddress = "Rotshild 64",
                        BankNumber = 10,
                        BranchCity = "Petah Tikva",
                        BranchNumber = 984
                    },
                    BankAccountNumber = 765432,
                    CollectionClearance = true
                },
                HostingUnitName = "Neve Shomron",

                Diary = new bool[12, 31],
                Area = Area.North,
                Type = BE.Type.Hotel,
                IsPool = true,
                IsJacuzzi = true,
                IsGarden = false
            };
            HostingUnit shalom = new HostingUnit()
            {
                HostingUnitKey = BE.Configuration.HostingUnitKey++,
                Owner = new BE.Host()
                {
                    HostKey = 252347694,
                    PrivateName = "Avi",
                    FamilyName = "Katz",
                    PhoneNumber = "0534878943",
                    MailAddress = "avikatz@gmail.com",
                    BankBranchDetails = new BE.BankBranch()
                    {
                        BankName = "otzar hachayal",
                        BranchAddress = "Aluf David 187",
                        BankNumber = 14,
                        BranchCity = "Ramat Gan",
                        BranchNumber = 986
                    },
                    BankAccountNumber = 761232,
                    CollectionClearance = true
                },
                HostingUnitName = "Neve Shalom",

                Diary = new bool[12, 31],
                Area = Area.Center,
                Type = BE.Type.Zimmer,
                IsPool = false,
                IsJacuzzi = true,
                IsGarden = true
            };

            ProtectedInvocation(() => bl.AddHostingUnit(neveGalil));
            ProtectedInvocation(() => bl.AddHostingUnit(shomron));
            ProtectedInvocation(() => bl.AddHostingUnit(shalom));

            foreach (var item in bl.HostingUnitList())
            {
                Console.WriteLine("Hosting unit:\n" + item);
            }
            #endregion

            #region guest requests
            GuestRequest reuven = new GuestRequest()
            {
                PrivateName = "Reuven",
                FamilyName = "Ochayon",
                GuestRequestKey = BE.Configuration.GuestRequestKey++,
                MailAddress = "ruvi@gmail.com",
                Status = BE.GuestRequestStatus.Active,
                RegistrationDate = new DateTime(2020, 02, 02),
                EntryDate = new DateTime(2020, 03, 02),
                ReleaseDate = new DateTime(2020, 03, 08),
                Area = BE.Area.All,
                Type = BE.Type.Zimmer,
                Adults = 2,
                Children = 2,
                IsPool = BE.Additions.Necessary,
                IsJacuzzi = BE.Additions.NotInterested,
                IsGarden = BE.Additions.Necessary,
                IsChildrenAttractions = BE.Additions.Possible,
            };
            GuestRequest shimon = new GuestRequest()
            {
                PrivateName = "Shimon",
                FamilyName = "Babai",
                GuestRequestKey = BE.Configuration.GuestRequestKey++,
                MailAddress = "shimi@gmail.com",
                Status = BE.GuestRequestStatus.Active,
                RegistrationDate = new DateTime(2020, 11, 13),
                EntryDate = new DateTime(2020, 12, 02),
                ReleaseDate = new DateTime(2020, 03, 05),
                Area = BE.Area.Center,
                Type = BE.Type.Hotel,
                Adults = 3,
                Children = 1,
                IsPool = BE.Additions.Possible,
                IsJacuzzi = BE.Additions.Necessary,
                IsGarden = BE.Additions.Possible,
                IsChildrenAttractions = BE.Additions.NotInterested,
            };
            GuestRequest levi = new GuestRequest()
            {
                PrivateName = "levi",
                FamilyName = "Dubinky",
                GuestRequestKey = BE.Configuration.GuestRequestKey++,
                MailAddress = "levi@gmail.com",
                Status = BE.GuestRequestStatus.Active,
                RegistrationDate = new DateTime(2020, 07, 19),
                EntryDate = new DateTime(2020, 08, 02),
                ReleaseDate = new DateTime(2020, 08, 03),
                Area = BE.Area.North,
                Type = BE.Type.Camping,
                Adults = 4,
                Children = 0,
                IsPool = BE.Additions.NotInterested,
                IsJacuzzi = BE.Additions.NotInterested,
                IsGarden = BE.Additions.Necessary,
                IsChildrenAttractions = BE.Additions.Necessary
            };

            ProtectedInvocation(()=> bl.AddGuestRequest(reuven));
            ProtectedInvocation(() => bl.AddGuestRequest(shimon));
            ProtectedInvocation(() => bl.AddGuestRequest(levi));
            foreach (var item in bl.GuestRequestList())
            {
                Console.WriteLine("Guest request:\n" + item);
            }




            #endregion

            #region orders
            ProtectedInvocation(()=>bl.creatAllOrders());
            foreach (var item in bl.OrderList())
            {
                Console.WriteLine("Order:\n" + item);
            }
            #endregion


            Order or = new Order();

            if (bl.OrderList()[0] != null)
            {
                or = bl.OrderList()[0];
            }
            ProtectedInvocation(() => bl.SendInvite(or)); ;
            foreach (var item in bl.OrderList())
            {
                Console.WriteLine("Order:\n" + item);
            }

            int com = bl.CloseDeal(or);

            foreach (var item in bl.AvailableUnitInDate(2, DateTime.Today))
            {
                Console.WriteLine("unit availabe from today for 2 nights:\n" + item);
            }


            


            foreach (var item in bl.AllOrdersMoreDaysThanWanted(Configuration.Expiration))
            {
                Console.WriteLine("Order expired:\n" + item);
            }


            Console.WriteLine("Number of hosting unit owned by yossi: " + bl.numOfUnitsSameHost(neveGalil.Owner));
            Console.WriteLine("Number of invites to reuven: " + bl.NumOfInvitesToRequest(reuven));
            Console.WriteLine("Number of deals in progress with yossi: " + bl.numOfGoodDealsOrInvites(neveGalil));

            #region grouping

            var v = bl.GetAllHostsByNumOfUnitsTheyOwn();
            foreach (var item in v)
            {
                Console.WriteLine(item.Key);
                foreach (var w in item)
                    Console.WriteLine(w);
            }

            var b = bl.GetAllGuestsGroupedByArea();
            foreach (var item in b)
            {
                Console.WriteLine(item.Key);
                foreach (var w in item)
                    Console.WriteLine(w);
            }

            var c = bl.GetAllGuestsGroupedByAmountOfPeople();
            foreach (var item in c)
            {
                Console.WriteLine(item.Key);
                foreach (var w in item)
                    Console.WriteLine(w);
            }

            var a = bl.GetAllUnitsGroupedByArea();
            foreach (var item in a)
            {
                Console.WriteLine(item.Key);
                foreach (var w in item)
                    Console.WriteLine(w);
            }
            #endregion

            Console.ReadKey();
        }
    }
}
