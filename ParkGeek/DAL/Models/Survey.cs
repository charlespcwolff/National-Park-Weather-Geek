namespace ParkGeek.DAL.Models
{
    /// <summary>
    /// Data model of survey_result table row
    /// </summary>
    public class Survey
    {
        /// <value>
        /// Primary Key
        /// </value>
        public int SurveyId { get; set; }

        /// <value>
        /// Foreign Key
        /// </value>
        public string ParkCode { get; set; }

        /// <summary>
        /// klklk
        /// </summary>
        public string EmailAddress { get; set; }
        public string State { get; set; }
        public string ActivityLevel { get; set; }
    }
}
