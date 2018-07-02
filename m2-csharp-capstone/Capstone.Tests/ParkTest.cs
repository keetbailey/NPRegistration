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
    public class ParkSqlTests
    {
        Dictionary<int, Park> output = new Dictionary<int, Park>();
        private TransactionScope tran;
        string connectionString = @"Data Source=DESKTOP-79DH3VU\SQLEXPRESS;Initial Catalog = NationalPark; Integrated Security = True";


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
        public void ParkHasList()  
        {
            ParkSqlDAL parkSql = new ParkSqlDAL();

            output = parkSql.ListAllParks();
            Assert.IsNotNull(output);
        }


    }
}
