using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ParkSqlDAL
    {
        //InstanceVariables
        private string connectionString = "";
        private const string SQL_ListAllParks = "SELECT * FROM park ORDER BY name";

        //Properties
        public int ParkCount { get; set; }

        //constructor
        public ParkSqlDAL()
        {
            connectionString = Properties.Settings.Default.ConnectionString;
        }

        //Methods
        public Dictionary<int, Park> ListAllParks()
        {
            Dictionary<int, Park> output = new Dictionary<int, Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_ListAllParks, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        int key = Convert.ToInt32(reader["park_id"]);

                        Park park = new Park
                        {
                            //Park_Id = Convert.ToInt32(reader["park_id"]),
                            Name = Convert.ToString(reader["name"]),
                            Establish_Date = Convert.ToDateTime(reader["establish_date"]),
                            Location = Convert.ToString(reader["location"]),
                            Area = Convert.ToInt32(reader["area"]),
                            Visitors = Convert.ToInt32(reader["visitors"]),
                            Description = Convert.ToString(reader["description"])
                        };

                        output[key] = park;
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return output;
        }

        public Park ParkChoice(int parkChoice)
        {
            return null;
        }
    }
}
