using ParkGeek.DAL.Models;
using System.Collections.Generic;

namespace ParkGeek.DAL
{
    /// <summary>
    /// Interface for all CRUD operation methods of ParkGeekSqlDAO class 
    /// </summary>
    public interface IParkGeekDAO
    {
        Park GetPark(string parkCode);
        IList<Park> GetParks();

        IList<Survey> GetSurveys();
        IList<SurveySubtotal> GetSurveySubtotals();
        int AddSurvey(Survey survey);

        IList<Weather> GetWeathers(string parkCode);
        IList<Weather> GetWeathers(Park park);
    }
}
