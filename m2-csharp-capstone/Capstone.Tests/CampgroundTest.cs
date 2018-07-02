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
    public class CampgroundTest
    {
        private TransactionScope tran;
        static CampGroundSqlDAL campGroundSql = new CampGroundSqlDAL();
        Dictionary<int, CampGround> output = new Dictionary<int, CampGround>();
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
        public void ListCampGrounds_Test()
        {
            int parkSelection = 1;

            output = campGroundSql.ListCampground(parkSelection);
            Assert.IsNotNull(output);

        }

    }
}
