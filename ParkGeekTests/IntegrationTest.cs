using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkGeek.DAL.Models;



namespace ParkGeekTests
{
    /// <summary>
    /// Test class to check DAO integration 
    /// successful with SQL database
    /// </summary>
    [TestClass]
    public class IntegrationTest : DAOTestInitializer
    {
        /// <summary>
        /// Verify Database Create DAO Method successful
        /// </summary>
        [TestMethod]
        public void CreateTest()
        {
            //CreateDAO();
            int result;

            Survey survey = new Survey()
            {
                ParkCode = "AAAA",
                EmailAddress = "AAAA",
                State = "AAAA",
                ActivityLevel = "AAAA"
            };

            result = _dao.AddSurvey(survey);

            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Verify Database Retrieve DAO Method succesful
        /// </summary>
        [TestMethod]
        public void ReadTest()
        {
            //CreateDAO();

            var result = _dao.GetParks();

            Assert.IsNotNull(result);

        }


    }
}
