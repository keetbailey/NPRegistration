using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;
using Capstone.Classes;
using System.Configuration;

namespace Capstone.Classes
{
    public class UserFacingCLI
    {
        //Instance Variables
        private string connectionString;


        //Constructor
        public UserFacingCLI()
        {
            connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        }

        //Methods
        public void RunCLI()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please select a park to view details: ");

                Dictionary<int, Park> allParks = PrintAllParks();

                Console.WriteLine("Q) Enter 'Q' to quit");

                string command = Console.ReadLine();
                Console.Clear();

                if (int.TryParse(command, out int intParkSelection) && allParks.ContainsKey(intParkSelection))
                {
                    Park userParkChoice = allParks[intParkSelection];

                    PrintParkInformation(userParkChoice);
                    bool done = false;
                    while (!done)
                    {

                        PrintCampgroundReservationMenu();

                        string choice = Console.ReadLine();

                        Dictionary<int, CampGround> campgrounds = ParkCampgrounds(intParkSelection);

                        if (int.TryParse(choice, out int intChoice) && intChoice == 1)
                        {
                            Console.WriteLine("{0} National Park Campgrounds", userParkChoice.Name);
                            Console.WriteLine();

                            PrintCampgrounds(campgrounds);

                            Console.WriteLine();
                            Console.WriteLine("Select a Commmand");
                            Console.WriteLine(" 1) Search for available reservation by campground ID");
                            Console.WriteLine(" 2) Return to previous menu");

                            string subChoice = Console.ReadLine();
                            if (int.TryParse(subChoice, out int intSubChoice) && intSubChoice == 1)
                            {
                                intChoice++;
                                choice = "2";
                            }
                            else if (int.TryParse(subChoice, out intSubChoice) && intSubChoice == 2)//return
                            {
                                done = false;
                            }
                            else if (!int.TryParse(subChoice, out intSubChoice))
                            {
                                Console.WriteLine("please make a valid selection");
                            }
                        }
                        if (int.TryParse(choice, out intChoice) && intChoice == 2)// Search reservation
                        {
                            Console.WriteLine("{0} National Park", userParkChoice.Name);
                            Console.WriteLine("Search for campground reservation");
                            PrintCampgrounds(campgrounds);
                            Console.WriteLine();
                            Console.Write("Which campground(enter 0 to cancel)? ");

                            string campgroundSelection = Console.ReadLine();

                            Dictionary<int, CampSite> campSites = new Dictionary<int, CampSite>();

                            if (int.TryParse(campgroundSelection, out int intcampgroundSelection) && campgrounds.ContainsKey(intcampgroundSelection))
                            {

                                DateTime[] reservationRange = ReservationDates();


                                decimal dailyFee = campgrounds[intcampgroundSelection].Daily_Fee;

                                campSites = SearchReservation(reservationRange, intParkSelection);

                                PrintAvailableCampsites(campSites, reservationRange, dailyFee);//UNDONE need to continue adding campsite selection here 
                                Console.WriteLine();
                                int siteSelection = SiteSelection();
                                string reservationName = ReservationName();
                                Dictionary<int, Reservation> newReservation = AddNewReservation(siteSelection, reservationName, reservationRange);
                                ReservationConfirmation(newReservation);
                            }
                            else if (int.TryParse(campgroundSelection, out intcampgroundSelection) && intcampgroundSelection == 0)
                            {
                                intChoice = 1;
                                choice = "1";
                            }
                            else if (!int.TryParse(campgroundSelection, out intcampgroundSelection))
                            {
                                Console.WriteLine("please make a valid selection");
                            }
                        }
                        else if (int.TryParse(choice, out intChoice) && intChoice == 3)//return
                        {
                            done = true;
                        }
                        else if (!int.TryParse(choice, out intChoice))
                        {
                            Console.WriteLine("please make a valid selection");
                        }
                    }
                }
                else if (command.ToLower() == "q")
                {
                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("please make a valid selection");
                }
            }
        }

        public Dictionary<int, Park> PrintAllParks()
        {
            ParkSqlDAL parkSqlDAL = new ParkSqlDAL();
            Dictionary<int, Park> allParks = new Dictionary<int, Park>();

            allParks = parkSqlDAL.ListAllParks();

            foreach (KeyValuePair<int, Park> park in allParks)
            {
                Console.WriteLine("{0}) {1}", park.Key, park.Value.Name);
            }
            return allParks;
        }

        public void PrintCampgroundReservationMenu()
        {
            Console.Clear();
            Console.WriteLine("Select a Commmand");
            Console.WriteLine(" 1) View Campgrounds");
            Console.WriteLine(" 2) Search For Reservation");
            Console.WriteLine(" 3) Return to Previous Menu");
        }

        public void PrintParkInformation(Park park)
        {
            Console.WriteLine(park.Name);
            Console.WriteLine("Location {0}", park.Location);
            Console.WriteLine("Established {0}", park.Establish_Date);
            Console.WriteLine("Area {0}sq km", park.Area);
            Console.WriteLine("Annual Visitors {0}", park.Visitors);
            Console.WriteLine();
            Console.WriteLine(park.Description);
            Console.WriteLine();
            Console.Write("Press enter to continue: ");
            Console.ReadLine();
        }

        public Dictionary<int, CampGround> ParkCampgrounds(int parkSelection)
        {
            CampGroundSqlDAL campGroundSql = new CampGroundSqlDAL();
            Dictionary<int, CampGround> campgrounds = new Dictionary<int, CampGround>();
            campgrounds = campGroundSql.ListCampground(parkSelection);

            return campgrounds;
        }

        public void PrintCampgrounds(Dictionary<int, CampGround> campgrounds)//UNDONE - format column print
        {
            List<string> header = new List<string>
            {
                "", "Name","Open","Closed","Daily Fee"
            };

            Console.Clear();
            Console.WriteLine("{0, -4} {1, -20} {2, -12} {3, -12} {4, -6}", header[0], header[1], header[2], header[3], header[4]);

            foreach (KeyValuePair<int, CampGround> campground in campgrounds)
            {
                Console.WriteLine("#{0, -3} {1, -20} {2, -12} {3, -12} {4, -6:C}",
                    campground.Key,
                    campground.Value.Name,
                    campground.Value.Open_From_Month,//<
                    campground.Value.Open_To_Month,//<
                    campground.Value.Daily_Fee);
            }
        }

        private Dictionary<int, CampSite> SearchReservation(DateTime[] reservationRange, int campgroundSelection)
        {
            CampSiteSQLDAL campSiteSql = new CampSiteSQLDAL();

            Dictionary<int, CampSite> output = new Dictionary<int, CampSite>();

            output = campSiteSql.ListCampSites(reservationRange, campgroundSelection);
            return output;
        }

        public void PrintAvailableCampsites(Dictionary<int, CampSite> campsites, DateTime[] reservationRange, decimal dailyFee)//UNDONE - added input args to print the resrvation fee
        {
            TimeSpan ts = reservationRange[1] - reservationRange[0];
            decimal cost = (decimal)ts.TotalDays * dailyFee;

            Console.Clear();
            List<string> header = new List<string>
                {
                    "Site No.", "Max Occup.", "Accessible?", "Max RV Length", "Utilities", "Cost"
                };
            Console.WriteLine("Results matching your search criteria:");
            Console.WriteLine("{0, -8}|{1, -10}|{2, -11}|{3, -5}|{4, -5}|{5, -5}",
                header[0],
                header[1],
                header[2],
                header[3],
                header[4],
                header[5]);

            foreach (KeyValuePair<int, CampSite> site in campsites)
            {

                Console.WriteLine(" {0, -7}| {1, -9}| {2, -10}| {3, -12}| {4, -8}| {5, -3:C}",

                    site.Key.ToString(),
                    site.Value.Max_Occupancy,
                    site.Value.AccessibleString,
                    site.Value.Max_RV_Length,
                    site.Value.UtilitiesString,
                    cost);//UNDONE - add Cost

            }
        }

        public DateTime[] ReservationDates()
        {
            DateTime[] output = new DateTime[2];//UNDONE I want to move this to a method
            Console.Write("What is arrival date?");//FIX - date format, unhandled exception here if inrecognized string
            output[0] = DateTime.Parse(Console.ReadLine());//FIX - date format
            Console.Write("What is the departure date?");
            output[1] = DateTime.Parse(Console.ReadLine());

            return output;
        }

        public Dictionary<int, Reservation> AddNewReservation(int siteSelection, string reservationName, DateTime[] reservationRange)
        {
            ReservationSqlDAL reservationSql = new ReservationSqlDAL();
            Dictionary<int, Reservation> newReservation = new Dictionary<int, Reservation>();

            newReservation = reservationSql.AddNewReservation(siteSelection, reservationName, reservationRange);

            return newReservation;
        }

        public int SiteSelection()
        {
            int output = 0;
            string siteSelection = "";
            int intSiteSelection = 0;

            while (!int.TryParse(siteSelection, out intSiteSelection))
            {
                Console.Write("Which site should be reserved (enter 0 to cancel)");
                siteSelection = Console.ReadLine();
                if (int.TryParse(siteSelection, out intSiteSelection) && intSiteSelection > 0)
                {
                    output = intSiteSelection;
                }
                else
                {
                    Console.Write("Please make a valid selection");
                }

            }
            return output;
        }

        public string ReservationName()
        {
            Console.Write("What name should the reservation be made under?");
            string output = Console.ReadLine();

            return output;
        }

        public void ReservationConfirmation(Dictionary<int, Reservation> reservations)
        {
            //int reservationId = reservation[0].Reservation_Id;
            foreach (KeyValuePair<int, Reservation> reservation in reservations)
            {

                Console.WriteLine("The reservation has been made and the confirmation ID is {0}", reservation.Key);
            }
            Console.ReadLine();
        }
    }

}
