using System;
using System.Collections.Generic;

namespace ParkGeek.DAL.Models
{
    /// <summary>
    /// Data model of weather table row and provides
    /// methods and properties associated with weather data
    /// </summary>
    public class Weather
    {


        ///<value>
        ///Primary Key Column 1 (Composite Key)
        ///</value>
        public string ParkCode { get; set; }

        /// <value>
        /// Primary Key Column 2 (Composite Key)
        /// Foreign Key
        /// </value>
        public int FiveDayForecastValue { get; set; }

        public int LowFarenheit { get; set; }
        public int HighFarenheit { get; set; }

        public string Forecast { get; set; }

        /// <value>
        /// Derived Property to convert Farenheit to Celsius 
        /// </value>
        public int LowCelsius
        {
            get => Convert.ToInt32(((double)LowFarenheit - 32) * 5 / 9);
        }
        /// <value>
        /// Derived Property to convert Farenheit to Celsius 
        /// </value>
        public int HighCelsius
        {
            get => Convert.ToInt32(((double)HighFarenheit - 32) * 5 / 9);
        }
        /// <value>
        /// Derived Property to show if this weather
        /// object represents today's values 
        /// </value>
        public bool IsFirstDay
        {
            get => (FiveDayForecastValue == 1);

        }
        /// <value>
        /// Derived property handler for converting Forecast 
        /// value to its corresponding image filename
        /// </value>
        public string WeatherImage
        {
            get => Forecast.Replace(' ', '_') + ".png";
        }

        /// <summary>
        /// Weather model helper method for generating concatenated string of
        /// all weather related messages
        /// </summary>
        /// <returns>String of recommendations based on Weather property values</returns>
        public string GetWeatherRecommendation()
        {
            string msg = "";

            List<string> msgList = new List<string>();


            //Forecast Messages
            if (Forecast == "snow")
            {
                msgList.Add("pack snowshoes");
            }
            else if (Forecast == "rain")
            {
                msgList.Add("pack rain gear and wear waterproof shoes");
            }
            else if (Forecast == "thunderstorms")
            {
                msgList.Add("seek shelter and avoid hiking on exposed ridges");
            }
            else if (Forecast == "sun")
            {
                msgList.Add("pack sunblock");
            }

            // Temperature Messages
            if (HighFarenheit > 75)
            {
                msgList.Add("bring an extra gallon of water");
            }

            if (LowFarenheit < 20)
            {
                msgList.Add("warning: danger of exposure to frigid temperatures");
            }

            if (HighFarenheit - LowFarenheit > 20)
            {
                msgList.Add("wear breathable layers");
            }

            msg = String.Join(", ", msgList);


            return String.IsNullOrEmpty(msg) ? msg : (Char.ToUpper(msg[0]) + msg.Substring(1) + ".");
        }
    }


}
