using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CampGroundSqlDAL
    {
        //InstanceVariables
        private string connectionString = "";
        private const string SQL_Campground = 
            "SELECT c.* " +
            "FROM campground c " +
            "JOIN park p ON p.park_id = c.park_id " +
            "WHERE c.park_id = @ParkId " +
            "ORDER BY campground_id";

        //constructor
        public CampGroundSqlDAL()
        {
            connectionString = Properties.Settings.Default.ConnectionString;
        }

        //Methods
        public Dictionary<int, CampGround> ListCampground(int parkSelection)
        {

            Dictionary<int, CampGround> output = new Dictionary<int, CampGround>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_Campground, conn);

                    cmd.Parameters.AddWithValue("@ParkId", parkSelection.ToString());

                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        int key = Convert.ToInt32(reader["campground_id"]);

                        CampGround campground = new CampGround
                        {
                            Campground_Id = Convert.ToInt32(reader["campground_id"]),
                            Park_Id = Convert.ToInt32(reader["park_id"]),
                            Name = Convert.ToString(reader["name"]),
                            Daily_Fee = Convert.ToInt32(reader["daily_fee"]),
                            Open_From_Int = Convert.ToInt32(reader["open_from_mm"]),
                            Open_To_Int = Convert.ToInt32(reader["open_to_mm"]),
                        };


                        output[key] = campground;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return output;
        }

    }
}
