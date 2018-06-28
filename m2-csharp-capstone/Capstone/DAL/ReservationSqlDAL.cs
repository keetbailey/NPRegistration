using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Capstone.DAL
{
    class ReservationSqlDAL
    {
        //InstanceVariables
        private string connectionString = "";
        private const string SQL_Reservation = "SELECT * FROM reservation ORDER BY reservation_id";

        //constructor
        public ReservationSqlDAL()
        {
            connectionString = Properties.Settings.Default.ConnectionString;
        }

        //Methods
        public List<Reservation> ReservationList()
        {
            List<Reservation> output = new List<Reservation>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_Reservation, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation
                        {
                            Reservation_Id = Convert.ToInt32(reader["reservation_id"]),
                            Site_Id = Convert.ToInt32(reader["site_id"]),
                            Name = Convert.ToString(reader["name"]),
                            From_Date = Convert.ToDateTime(reader["from_date"]),
                            To_Date = Convert.ToDateTime(reader["to_date"]),
                            Create_Date = Convert.ToDateTime(reader["create_date"])


                        };

                        output.Add(reservation);
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
