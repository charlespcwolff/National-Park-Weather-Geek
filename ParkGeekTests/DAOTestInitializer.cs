using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkGeek.DAL;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;




namespace ParkGeekTests
{
    /// <summary>
    /// Base class to initialize database tests
    /// </summary>
    [TestClass]
    public class DAOTestInitializer
    {

        private TransactionScope transaction;
        protected IParkGeekDAO _dao;
        protected string _connectionString;


        /// <summary>
        /// Helper method for initializing database tests. Creates
        /// configuration object from json file and instance of
        /// ParkGeekSqlDAO
        /// </summary>
        /// <seealso cref="ParkGeekSqlDAO.ParkGeekSqlDAO(string)"/>
        public void CreateDAO()
        {

            IConfigurationBuilder builder = new ConfigurationBuilder().
            SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("NPGeek");

            _dao = new ParkGeekSqlDAO(_connectionString);
            // this._dao = new ParkGeekSqlDAO(_connectionString);
        }

        /// <summary>
        /// TestInitialize method evaluates before all other methods to
        /// begin database transaction and insert data into tables for
        /// testing
        /// </summary>
        [TestInitialize]
        [DeploymentItem(@"..\..\scripts\test-query.sql")]
        public void Setup()
        {
            CreateDAO();
            // Begin the transaction
            transaction = new TransactionScope();
            string s = System.IO.Directory.GetCurrentDirectory();
            // Get the SQL Script to run
            string sql = File.ReadAllText(@"scripts\test-query.sql");

            // Execute the script
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Test cleanup method to rollback database transaction
        /// and restore all values prior to tests
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            transaction.Dispose();
        }
    }
}
