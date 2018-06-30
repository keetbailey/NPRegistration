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
        private TransactionScope tran;
        Dictionary<int, Park> output = new Dictionary<int, Park>();

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
        public void ViewParkName()  //pulls in valid park name 
        {
            ParkSqlDAL parkSql = new ParkSqlDAL();
            Park newPark = new Park();
            newPark.Name = "Arches"; 

            output = parkSql.ListAllParks(); //UNDONE - ListAllParks does not take in parameter - was hoping to do output = parkSql.ListAllParks(newPark.Name);
            Assert.AreEqual("Arches", newPark.Name.ToString());
        }

        [TestMethod]
        public void ParkList()  //UNDONE - Wanting to ensure contents of parks is available 
        {
            ParkSqlDAL parkSql = new ParkSqlDAL();

            output = parkSql.ListAllParks(); 
            Assert.IsNotNull(output);
        }
    }
}
