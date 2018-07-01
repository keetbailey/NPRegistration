using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;
using Capstone.Classes;
using System.Configuration;
using System.Transactions;
using System.Data.SqlClient;


namespace Capstone.Tests
{
    [TestClass]
    public class ReservationTest
    {
        private TransactionScope tran;
        Dictionary<int, Reservation> output = new Dictionary<int, Reservation>();

        [TestInitialize]
        public void initialize()
        {
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void CreateReservationIfAvailable()  // test that the dictionary item exists 
        {
            ReservationSqlDAL reservationSql = new ReservationSqlDAL();
            Reservation newReservation = new Reservation();
            

            //output = reservationSql.AddNewReservation(1, "Keet Bailey", new DateTime(2018,06,20) ;
            Assert.IsNotNull(output);
        }

    }
}
