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
        private const string SQL_ListCampSite = "SELECT * FROM site ORDER BY site_id";

        //constructor
        public CampSiteSqlDAL()
        {
            connectionString = Properties.Settings.Default.ConnectionString;
        }

        //Methods
        public List<CampSite> ListCampSites()
        {
            List<CampSite> output = new List<CampSite>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_ListCampSite, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CampSite site = new CampSite
                        {
                            Site_Id = Convert.ToInt32(reader["site_id"]),
                            Campground_Id = Convert.ToInt32(reader["campground_id"]),
                            Site_Number = Convert.ToInt32(reader["site_number"]),
                            Max_Occupancy = Convert.ToInt32(reader["max_occupancy"]),
                            Accessible = Convert.ToBoolean(reader["accessible"]),
                            Max_RV_Length = Convert.ToInt32(reader["max_rv_length"]),
                            Utilities = Convert.ToBoolean(reader["utilities"])
                        };

                        output.Add(site);
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
