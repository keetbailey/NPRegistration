using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Capstone.DAL
{
    class CampSiteSqlDAL
    {
        //InstanceVariables
        private string connectionString = "";
        private const string SQL_CampSite = "SELECT * FROM site ORDER BY site_id";

        //constructor
        public CampSiteSqlDAL()
        {
            connectionString = Properties.Settings.Default.ConnectionString;
        }

        //Methods
        public Dictionary<int, CampSite> ListCampSites()
        {
            Dictionary<int, CampSite> output = new Dictionary<int, CampSite>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_CampSite, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int key = Convert.ToInt32(reader["site_id"]);

                        CampSite site = new CampSite
                        {
                            Campground_Id = Convert.ToInt32(reader["campground_id"]),
                            Site_Number = Convert.ToInt32(reader["site_number"]),
                            Max_Occupancy = Convert.ToInt32(reader["max_occupancy"]),
                            Accessible = Convert.ToBoolean(reader["accessible"]),
                            Max_RV_Length = Convert.ToInt32(reader["max_rv_length"]),
                            Utilities = Convert.ToBoolean(reader["utilities"])
                        };

                        output[key] = site;
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
