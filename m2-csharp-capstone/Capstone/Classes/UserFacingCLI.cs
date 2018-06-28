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
                string command = Console.ReadLine();
                int intcommand = 0;

                if (int.TryParse(command, out intcommand) && parkList.ContainsKey(intcommand))
                {
                    bool done = false;
                    while (!done)
                    {
                        int intcommandCampground = 0;

                        PrintParkInformation(parkList[intcommand]);
                        Console.WriteLine();
                        Console.WriteLine("Select a Commmand");
                        Console.WriteLine("\t1) View Campgrounds");
                        Console.WriteLine("\t2) Search For Reservation");
                        Console.WriteLine("\t3) Return to Previous Menu");
                        command = Console.ReadLine();

                        if (int.TryParse(command, out intcommandCampground) && intcommandCampground == 1)//campground
                        {
                            CampGroundSqlDAL campGroundSql = new CampGroundSqlDAL(connectionString);
                            Dictionary<int, CampGround> CampGround = new Dictionary<int, CampGround>();

                            Console.WriteLine(parkList[intcommand].Name);
                            Console.ReadLine();
                            ViewCampgrounds(CampGround);
                        }
                        else if (int.TryParse(command, out intcommandCampground) && intcommandCampground == 2)//reservation
                        {
                            SearchReservation();
                        }
                        else if (int.TryParse(command, out intcommandCampground) && intcommandCampground == 3)//return
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

        public void ViewCampgrounds(Dictionary<int, CampGround> campgrounds)
        {
            foreach (KeyValuePair<int, CampGround> campground in campgrounds)
            {
                Console.WriteLine("#{0} {1} {2} {3} {4:C}", campground.Key, campground.Value.Name, campground.Value.Open_From_mm, campground.Value.Open_To_mm, campground.Value.Daily_Fee);
            }
        }

        private void SearchReservation()
        {

        }
    }
}
