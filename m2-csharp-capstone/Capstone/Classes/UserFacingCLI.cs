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

            while (true)
            {
                parkList = parkSqlDAL.ListAllParks();
                Console.WriteLine("Please select a park to view details: ");

                PrintAllParks(parkList);
                string command = Console.ReadLine();
                int intcommand = 0;

                if (int.TryParse(command, out intcommand) && parkList.ContainsKey(intcommand) )
                {
                    PrintParkInformation(parkList[intcommand]);

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

        private void ParkInfoCommand()
        {

        }

        private void ViewCampgrouds()
        {

        }

        private void SearchReservation()
        {

        }
    }
}
