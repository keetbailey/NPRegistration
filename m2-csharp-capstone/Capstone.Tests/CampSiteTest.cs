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
        private string connectionString = @
        

        [TestInitialize]
        public void initialize()
        {
            CampSiteSqlDAL campSiteSql = new CampSiteSqlDAL();

            Dictionary<int, CampSite> output = new Dictionary<int, CampSite>();

            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("");
                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void ListCampSites_Test()
        {
            
        }
    }
}
