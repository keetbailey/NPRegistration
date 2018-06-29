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
    public class CampSiteTest
    {
        private TransactionScope tran;
        CampSiteSqlDAL campSiteSql = new CampSiteSqlDAL();
        Dictionary<int, CampSite> output = new Dictionary<int, CampSite>();


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
        public void ListCampSites_Test()
        {
            int campgroundSelection = 1;
            DateTime[] dateTimeTest = new DateTime[2];

            dateTimeTest[0] = new DateTime(2018, 10, 20);
            dateTimeTest[1] = new DateTime(2018, 10, 25);
 
            output = campSiteSql.ListCampSites(dateTimeTest, campgroundSelection);
            Assert.IsNotNull(output);

        }
    }
}
