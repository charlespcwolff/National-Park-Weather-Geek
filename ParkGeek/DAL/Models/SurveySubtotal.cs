namespace ParkGeek.DAL.Models
{
    /// <summary>
    /// Data model of survey_result table aggregated by ParkCode 
    /// </summary>
    public class SurveySubtotal
    {
        public string ParkCode { get; set; }
        public string ParkName { get; set; }

        /// <value>
        /// COUNT of surveys for ParkCode
        /// </value>
        public int Total { get; set; }
    }
}
