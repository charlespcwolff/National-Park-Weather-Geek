using ParkGeek.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ParkGeek.DAL
{


    /// <summary>
    /// Class for CRUD operations with NPGeek database and
    /// mapping to data model objects
    /// </summary>
    public class ParkGeekSqlDAO : IParkGeekDAO
    {
        private readonly string _connectionString;

        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";

        public ParkGeekSqlDAO(string connectionString)
        {
            this._connectionString = connectionString;
        }

        #region ParkTable

        /// <summary>
        /// Returns Park data model object
        /// from park table
        /// </summary>
        /// <param name="parkCode">Park's unique identifier code (primary key)</param>
        /// <returns>Park data model object</returns>

        public Park GetPark(string parkCode)
        {
            Park park = new Park();
            const string sql = "SELECT * FROM park WHERE parkCode = @code";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@code", parkCode);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        park = GetParkFromReader(reader);
                    }
                }
            }

            return park;
        }
        /// <summary>
        /// Returns collection of all parks
        /// as Park data model objects 
        /// </summary>
        /// <returns>Collection of Park data model objects</returns>
        public IList<Park> GetParks()
        {
            var parkList = new List<Park>();

            const string sql = "SELECT * FROM park";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Park park = GetParkFromReader(reader);
                        parkList.Add(park);
                    }
                }
            }


            return parkList;

        }

        /// <summary>
        /// Helper method to create instance of Park 
        /// data model object from data row in park table
        /// </summary>
        /// <param name="reader">Data row from Park table</param>
        /// <returns>Park data model object</returns>
        private Park GetParkFromReader(SqlDataReader reader)
        {
            Park park = new Park();

            park.ParkCode = Convert.ToString(reader["parkCode"]);
            park.ParkName = Convert.ToString(reader["parkName"]);
            park.State = Convert.ToString(reader["state"]);
            park.Acreage = Convert.ToInt32(reader["acreage"]);
            park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
            park.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
            park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
            park.Climate = Convert.ToString(reader["climate"]);
            park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
            park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
            park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
            park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
            park.ParkDescription = Convert.ToString(reader["parkDescription"]);
            park.EntryFee = Convert.ToInt32(reader["entryFee"]);
            park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);

            return park;

        }

        #endregion

        #region SurveyTable

        /// <summary>
        /// Returns collection of SurveySubTotal objects from all surveys
        /// in survey_result table aggregated by table field parkCode
        /// </summary>
        /// <returns>List of all SurveySubtotal objects</returns>
        public IList<SurveySubtotal> GetSurveySubtotals()
        {
            IList<SurveySubtotal> surveySubtotalList = new List<SurveySubtotal>();



            const string sql = "SELECT survey_result.parkCode,park.parkName, " +
                         "COUNT(survey_result.parkCode) AS count_survey " +
                         "FROM survey_result JOIN park ON park.parkCode = survey_result.parkCode " +
                         "GROUP BY survey_result.parkCode, park.parkName " +
                         "HAVING COUNT(survey_result.parkCode) >= 1 " +
                         "ORDER BY count_survey DESC, park.parkName ASC";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SurveySubtotal surveyTotal = GetSurveySubtotalFromReader(reader);
                        surveySubtotalList.Add(surveyTotal);
                    }
                }
            }
            return surveySubtotalList;


        }

        /// <summary>
        /// Helper method to populate and return SurveySubtotal 
        /// object from survey_result table query
        /// </summary>
        /// <param name="reader">A datarow from survey_result table</param>
        /// <returns>SurveySubtotal object</returns>
        private SurveySubtotal GetSurveySubtotalFromReader(SqlDataReader reader)
        {
            SurveySubtotal surveySubtotal = new SurveySubtotal();

            surveySubtotal.ParkCode = Convert.ToString(reader["parkCode"]);
            surveySubtotal.ParkName = Convert.ToString(reader["parkName"]);
            surveySubtotal.Total = Convert.ToInt32(reader["count_survey"]);

            return surveySubtotal;

        }

        /// <summary>
        /// Returns collection of Survey data model objects 
        /// for all Surveys in survey_result table
        /// </summary>
        /// <returns>Collection of Survey data model objects</returns>
        public IList<Survey> GetSurveys()
        {

            IList<Survey> surveyList = new List<Survey>();
            const string sql = "SELECT * FROM survey_result";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Survey survey = GetSurveyFromReader(reader);
                        surveyList.Add(survey);
                    }
                }
            }
            return surveyList;
        }

        /// <summary>
        /// Helper method to populate and return Survey data model
        /// object from survey_result table data row
        /// </summary>
        /// <param name="reader">Datarow from survey_result table</param>
        /// <returns>Survey data model object</returns>
        private Survey GetSurveyFromReader(SqlDataReader reader)
        {
            Survey survey = new Survey();

            survey.SurveyId = Convert.ToInt32(reader["surveyId"]);
            survey.ParkCode = Convert.ToString(reader["parkCode"]);
            survey.EmailAddress = Convert.ToString(reader["emailAddress"]);
            survey.State = Convert.ToString(reader["state"]);
            survey.ActivityLevel = Convert.ToString(reader["activityLevel"]);

            return survey;
        }

        /// <summary>
        /// Add new survey to survey_result table in
        /// database from Survey data model object
        /// </summary>
        /// <param name="newSurvey">Survey data model object</param>
        /// <returns>Index of new data row in survey_result if successful</returns>
        public int AddSurvey(Survey newSurvey)
        {
            int result;
            const string sql = "INSERT INTO survey_result VALUES (@code,@email,@state,@activity)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@code", newSurvey.ParkCode);
                cmd.Parameters.AddWithValue("@email", newSurvey.EmailAddress);
                cmd.Parameters.AddWithValue("@state", newSurvey.State);
                cmd.Parameters.AddWithValue("@activity", newSurvey.ActivityLevel);

                result = (int)cmd.ExecuteScalar();

            }
            return result;
        }


        #endregion

        #region WeatherTable

        /// <summary>
        /// Returns collection of Weather data
        /// model objects for a single park
        /// </summary>
        /// <param name="parkCode">Park's unique code</param>
        /// <returns>Collection of Weather data model objects</returns>
        public IList<Weather> GetWeathers(string parkCode)
        {
            var weatherList = new List<Weather>();

            const string sql = "SELECT * FROM weather WHERE parkCode = @code ORDER BY fiveDayForecastValue ASC";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@code", parkCode);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Weather weather = GetWeatherFromReader(reader);
                        weatherList.Add(weather);
                    }
                }
            }


            return weatherList;
        }

        /// <summary>
        /// Overload for GetWeathers method
        /// </summary>
        /// <param name="park">Park data model object</param>
        /// <returns>Collection of Weather data model objects</returns>
        /// <seealso cref="ParkGeekSqlDAO.GetWeathers(string)"/>
        public IList<Weather> GetWeathers(Park park)
        {
            return GetWeathers(park.ParkCode);
        }

        /// <summary>
        /// Helper method to populate and return Weather
        /// data model object from weather table data row
        /// </summary>
        /// <param name="reader">Data row from weather table</param>
        /// <returns>Weather data model object</returns>
        private Weather GetWeatherFromReader(SqlDataReader reader)
        {
            Weather weather = new Weather();

            weather.ParkCode = Convert.ToString(reader["parkCode"]);
            weather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
            weather.LowFarenheit = Convert.ToInt32(reader["low"]);
            weather.HighFarenheit = Convert.ToInt32(reader["high"]);
            weather.Forecast = Convert.ToString(reader["forecast"]);


            return weather;

        }

        #endregion







    }
}
