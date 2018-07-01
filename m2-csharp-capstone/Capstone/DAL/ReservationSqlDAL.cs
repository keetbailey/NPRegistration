using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Classes;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ReservationSqlDAL
    {
        //InstanceVariables
        private string connectionString = "";
        private const string SQL_Reservation =
            "SELECT r.* " +
            "FROM reservation r " +
            "JOIN site s ON s.site_id = r.site_id" +
            "ORDER BY reservation_id";
        private const string SQL_AddReservation =
            "INSERT INTO reservation" +
            "(site_id, name, from_date, to_date, create_date)" +
            "VALUES (@site_id, @name, @from_date, @to_date, @create_date)";
        private const string SQL_ReturnLastReservation =
            "SELECT TOP 1 *" +
            "FROM reservation " +
            "ORDER BY reservation_id DESC";



        //constructor
        public ReservationSqlDAL()
        {
            connectionString = Properties.Settings.Default.ConnectionString;
        }

        //Methods
       
        public Dictionary<int, Reservation> AddNewReservation(int siteSelection, string reservationName, DateTime[] reservationRange)
        {
            Dictionary<int, Reservation> output = new Dictionary<int, Reservation>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AddReservation, conn);

                    cmd.Parameters.AddWithValue("@site_id", siteSelection);
                    cmd.Parameters.AddWithValue("@name", reservationName);
                    cmd.Parameters.AddWithValue("@from_date", reservationRange[0]);
                    cmd.Parameters.AddWithValue("@to_date", reservationRange[1]);
                    cmd.Parameters.AddWithValue("@create_date", DateTime.Now);

                    cmd.ExecuteNonQuery(); //CODE REVIEW QUESTION - why does execute non-query followed by reader write to table twice 

                    cmd = new SqlCommand(SQL_ReturnLastReservation, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int key = Convert.ToInt32(reader["reservation_id"]);

                        Reservation reservation = new Reservation
                        {
                            Site_Id = Convert.ToInt32(reader["site_id"]),
                            Name = Convert.ToString(reader["name"]),
                            From_Date = Convert.ToDateTime(reader["from_date"]),
                            To_Date = Convert.ToDateTime(reader["to_date"]),
                            Create_Date = Convert.ToDateTime(reader["create_date"])
                        };
                        output[key] = reservation;
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
