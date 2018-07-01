using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Capstone.DAL
{
    public class CampSiteSQLDAL
    {
        //InstanceVariables
        private string connectionString = "";
        private const string SQL_CampSite =
            "Select TOP 5 * " +
            "FROM site s " +
            "WHERE s.campground_id = @campground_id " +
            "AND s.site_id NOT IN " +
            "(SELECT site_id from reservation  " +
            "WHERE @requested_start < to_date " +
            "AND @requested_end > from_date); ";

        //constructor
        public CampSiteSQLDAL()
        {
            connectionString = Properties.Settings.Default.ConnectionString;
        }

        //Methods
        public Dictionary<int, CampSite> ListCampSites(DateTime[] reservationRange, int campgroundSelection)
        {
            Dictionary<int, CampSite> output = new Dictionary<int, CampSite>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_CampSite, conn);
                    

                    cmd.Parameters.AddWithValue("@requested_start", reservationRange[0]);
                    cmd.Parameters.AddWithValue("@requested_end", reservationRange[1]);
                    cmd.Parameters.AddWithValue("@campground_id", campgroundSelection);
                    

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int key = Convert.ToInt32(reader["site_id"]);

                        CampSite site = new CampSite
                        {
                            Campground_Id = Convert.ToInt32(reader["campground_id"]),
                            Site_Number = Convert.ToInt32(reader["site_number"]),
                            Max_Occupancy = Convert.ToInt32(reader["max_occupancy"]),
                            Max_RV_Length = Convert.ToInt32(reader["max_rv_length"]),
                            AccessibleBool = Convert.ToBoolean(reader["accessible"]),
                            UtilitiesBool = Convert.ToBoolean(reader["utilities"])
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
