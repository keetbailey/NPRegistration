﻿using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Capstone.DAL
{
    public class CampSiteSqlDAL
    {
        //InstanceVariables
        private string connectionString = "";
        private const string SQL_CampSite =
            "Select s.site_id " +
            "FROM site s " +
            "WHERE s.campground_id = @campground_id " +
            "AND s.site_id NOT IN " +
            "(SELECT site_id from reservation  " +
            "WHERE @requested_start < to_date " +
            "AND @requested_end > from_date); ";

        //constructor
        public CampSiteSqlDAL()
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

                    SqlDataReader reader = cmd.ExecuteReader();
                    cmd.Parameters.AddWithValue("@campground_id", campgroundSelection.ToString());
                    cmd.Parameters.AddWithValue("@requested_start", reservationRange[0].ToString());
                    cmd.Parameters.AddWithValue("@requested_end", reservationRange[1].ToString());

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
