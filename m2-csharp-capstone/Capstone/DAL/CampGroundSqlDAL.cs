using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    class CampGroundSqlDAL
    {
        //InstanceVariables
        private string connectionString = "";
        private const string SQL_Campground = "SELECT * FROM campground ORDER BY campground_id";

        //constructor
        public CampGroundSqlDAL()
        {
            connectionString = Properties.Settings.Default.ConnectionString;
        }

        //Methods
        public List<CampGround> ListCampground()
        {
            List<CampGround> output = new List<CampGround>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_Campground, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CampGround campground = new CampGround
                        {
                            Campground_Id = Convert.ToInt32(reader["campground_id"]),
                            Park_Id = Convert.ToInt32(reader["park_id"]),
                            Name = Convert.ToString(reader["name"]),
                            Open_From_mm = Convert.ToInt32(reader["open_from_mm"]),
                            Open_To_mm = Convert.ToInt32(reader["open_to_mm"]),
                            Daily_Fee = Convert.ToInt32(reader["daily_fee"])


                        };

                        output.Add(campground);
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
