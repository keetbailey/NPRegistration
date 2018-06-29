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
        string connectionString;


        //Constructor
        public UserFacingCLI()
        {
            connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        }

        //Methods
        public void RunCLI()
        {
            ParkSqlDAL parkSqlDAL = new ParkSqlDAL(connectionString);
            Dictionary<int, Park> parkList = new Dictionary<int, Park>();

            while (true)//parks
            {
                parkList = parkSqlDAL.ListAllParks();
                Console.WriteLine("Please select a park to view details: ");

                PrintAllParks(parkList);

                Console.WriteLine("Q) Enter 'Q' to quit");

                string command = Console.ReadLine();
                int intParkSelection = 0;

                if (int.TryParse(command, out intParkSelection) && parkList.ContainsKey(intParkSelection))
                {
                    bool done = false;
                    while (!done)
                    {
                        Park userParkChoice = parkList[intParkSelection];

                        PrintParkInformation(userParkChoice);

                        Console.WriteLine();
                        Console.WriteLine("Select a Commmand");
                        Console.WriteLine(" 1) View Campgrounds");
                        Console.WriteLine(" 2) Search For Reservation");
                        Console.WriteLine(" 3) Return to Previous Menu");

                        string choice = Console.ReadLine();
                        int intChoice = 0;

                        CampGroundSqlDAL campGroundSql = new CampGroundSqlDAL(connectionString);
                        Dictionary<int, CampGround> campgrounds = new Dictionary<int, CampGround>();

                        if (int.TryParse(choice, out intChoice) && intChoice == 1)
                        {
                            campgrounds = campGroundSql.ListCampground(intParkSelection);

                            Console.WriteLine("Park Campgrounds");
                            Console.WriteLine("{0} National Park Campgrounds", userParkChoice.Name);
                            Console.WriteLine();
                            PrintCampgrounds(campgrounds);

                            Console.WriteLine();
                            Console.WriteLine("Select a Commmand");
                            Console.WriteLine(" 1) Search for available reservation by campground ID");
                            Console.WriteLine(" 2) Return to previous menu");
                            string choice2 = Console.ReadLine();
                            int intChoice2 = 0;

                            if (int.TryParse(choice2, out intChoice2) && intChoice2 == 1)
                            {
                                intChoice++;
                                choice = "2";
                            }
                            else if (int.TryParse(choice2, out intChoice2) && intChoice2 == 2)//return
                            {
                                return;
                            }
                            else
                            {
                                Console.WriteLine("please make a valid selection");
                            }
                        }//View Campgrounds
                        if (int.TryParse(choice, out intChoice) && intChoice == 2)// Search reservation
                        {
                            Console.WriteLine("Search for campground reservation");
                            PrintCampgrounds(campgrounds);
                            Console.WriteLine();
                            Console.Write("Which campground(enter 0 to cancel)? ");

                            string campgroundSelection = Console.ReadLine();
                            int intcampgroundSelection = 0;
                            if (int.TryParse(campgroundSelection, out intcampgroundSelection) && campgrounds.ContainsKey(intcampgroundSelection))
                            {
                                DateTime[] reservationRange = new DateTime[2];
                                Console.Write("What is arrival date(dd/mm/yyyy)?");
                                reservationRange[0] = DateTime.Parse(Console.ReadLine());
                                Console.Write("What is the departure date(dd/mm/yyyy)?");
                                reservationRange[1] = DateTime.Parse(Console.ReadLine());

                                SearchReservation(reservationRange);
                            }
                            else if (int.TryParse(campgroundSelection, out intcampgroundSelection) && intcampgroundSelection == 0)
                            {
                                intChoice = 1;
                                choice = "1";
                            }
                            else
                            {
                                Console.WriteLine("please make a valid selection");
                            }


                            SearchReservation();
                        }
                        else if (int.TryParse(choice, out intChoice) && intChoice == 3)//return
                        {
                            done = true;
                        }
                        else
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
                    Console.WriteLine("please make a valid selection");
                }

            }
        }

        public void PrintAllParks(Dictionary<int, Park> parks)
        {

            foreach (KeyValuePair<int, Park> park in parks)
            {
                Console.WriteLine("{0}) {1}", park.Key, park.Value.Name);
            }
        }

        private void PrintParkInformation(Park park)
        {
            Console.WriteLine(park.Name);
            Console.WriteLine("Location {0}", park.Location);
            Console.WriteLine("Established {0}", park.Establish_Date);
            Console.WriteLine("Area {0}sq km", park.Area);
            Console.WriteLine("Annual Visitors {0}", park.Visitors);
            Console.WriteLine();
            Console.WriteLine(park.Description);
        }

        public void PrintCampgrounds(Dictionary<int, CampGround> campgrounds)//UNDONE - format column print, convert mm int to month name
        {
            Console.WriteLine("Name Open Closed Daily Fee");
            foreach (KeyValuePair<int, CampGround> campground in campgrounds)
            {
                Console.WriteLine("#{0} {1} {2} {3} {4:C}",
                    campground.Key,
                    campground.Value.Name,
                    campground.Value.Open_From_mm,//<
                    campground.Value.Open_To_mm,//<
                    campground.Value.Daily_Fee);
            }
        }

        private void SearchReservation(DateTime[] reservationRange, int campgroundSelection)
        {

        }
    }
}
