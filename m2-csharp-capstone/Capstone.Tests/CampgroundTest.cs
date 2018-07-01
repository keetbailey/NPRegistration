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
        CampGroundSqlDAL campGroundSql = new CampGroundSqlDAL();
        Dictionary<int, CampGround> output = new Dictionary<int, CampGround>();


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

        [TestMethod]
        public void VerifyCampGroundMatchesToPark() //verify that if using "Acadia" park, campgrounds for Blackwoods, Seawall and Schoodic Woods populates 
        {
            
            string result = "";
            string resulttwo = "";
            string resultthree = "";

            output = campGroundSql.ListCampground(1);

            Assert.AreEqual("Blackwoods", result);
            Assert.AreEqual("Seawall", resulttwo);
            Assert.AreEqual("Schoodic Woods", resultthree);

        }

    }
}
