using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkGeek.DAL.Models;

namespace ParkGeekTests
{
    [TestClass]
    public class UnitTests
    {




        #region WeatherTempProperties

        /// <summary>
        /// check dervied property formula for farenheight to celsius conversion
        /// </summary>
        /// <param name="farenheit"></param>
        /// <param name="expectedCelsius"></param>
        [DataTestMethod]
        [DataRow(-7, -22)]
        [DataRow(-6, -21)]
        [DataRow(3, -16)]
        [DataRow(4, -16)]
        [DataRow(5, -15)]
        [DataRow(65, 18)]
        [DataRow(82, 28)]
        [DataRow(100, 38)]
        [DataRow(101, 38)]
        [DataRow(109, 43)]
        [DataRow(110, 43)]
        [DataRow(111, 44)]
        [DataRow(112, 44)]
        [DataRow(113, 45)]
        [DataRow(114, 46)]
        public void TestHighTempConversion(int farenheit, int expectedCelsius)
        {
            Weather weather = new Weather()
            {
                HighFarenheit = farenheit
            };

            Assert.AreEqual(weather.HighCelsius, expectedCelsius);
        }
        /// <summary>
        /// check dervied property formula for farenheight to celsius conversion
        /// </summary>
        /// <param name="farenheit"></param>
        /// <param name="expectedCelsius"></param>
        [DataTestMethod]
        [DataRow(-7, -22)]
        [DataRow(-6, -21)]
        [DataRow(3, -16)]
        [DataRow(4, -16)]
        [DataRow(5, -15)]
        [DataRow(65, 18)]
        [DataRow(82, 28)]
        [DataRow(100, 38)]
        [DataRow(101, 38)]
        [DataRow(109, 43)]
        [DataRow(110, 43)]
        [DataRow(111, 44)]
        [DataRow(112, 44)]
        [DataRow(113, 45)]
        [DataRow(114, 46)]
        public void TestLowTempConversion(int farenheit, int expectedCelsius)
        {
            Weather weather = new Weather()
            {
                LowFarenheit = farenheit
            };

            Assert.AreEqual(expectedCelsius, weather.LowCelsius);
        }
        #endregion

        /// <summary>
        /// checks string output of WeatherImage helper property
        /// </summary>
        /// <param name="input"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DataRow("cloudy", "cloudy.png")]
        [DataRow("partly cloudy", "partly_cloudy.png")]
        [DataRow("rain", "rain.png")]
        [DataRow("snow", "snow.png")]
        [DataRow("sunny", "sunny.png")]
        [DataRow("thunderstorms", "thunderstorms.png")]
        public void CheckImageFileString(string input, string expected)
        {
            Weather weather = new Weather()
            {
                Forecast = input
            };
            Assert.AreEqual(expected, weather.WeatherImage);
        }
        /// <summary>
        /// Checks output string of weather reccomendations for park detail view
        /// </summary>
        /// <param name="forecast"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DataRow("thunderstorms", 59, 83, "Seek shelter and avoid hiking on exposed ridges, bring an extra gallon of water, wear breathable layers.")]
        [DataRow("thunderstorms", 53, 80, "Seek shelter and avoid hiking on exposed ridges, bring an extra gallon of water, wear breathable layers.")]
        [DataRow("snow", 12, 107, "Pack snowshoes, bring an extra gallon of water, warning: danger of exposure to frigid temperatures, wear breathable layers.")]
        [DataRow("rain", -9, 96, "Pack rain gear and wear waterproof shoes, bring an extra gallon of water, warning: danger of exposure to frigid temperatures, wear breathable layers.")]
        [DataRow("thunderstorms", 56, 62, "Seek shelter and avoid hiking on exposed ridges.")]
        [DataRow("rain", 53, 84, "Pack rain gear and wear waterproof shoes, bring an extra gallon of water, wear breathable layers.")]
        [DataRow("cloudy", 57, 77, "Bring an extra gallon of water.")]
        [DataRow("partly cloudy", 59, 114, "Bring an extra gallon of water, wear breathable layers.")]
        [DataRow("cloudy", 44, 111, "Bring an extra gallon of water, wear breathable layers.")]
        [DataRow("thunderstorms", -6, 73, "Seek shelter and avoid hiking on exposed ridges, warning: danger of exposure to frigid temperatures, wear breathable layers.")]
        [DataRow("snow", 57, 75, "Pack snowshoes.")]
        [DataRow("thunderstorms", 22, 47, "Seek shelter and avoid hiking on exposed ridges, wear breathable layers.")]
        [DataRow("sunny", 7, 119, "Bring an extra gallon of water, warning: danger of exposure to frigid temperatures, wear breathable layers.")]
        [DataRow("rain", -20, 100, "Pack rain gear and wear waterproof shoes, bring an extra gallon of water, warning: danger of exposure to frigid temperatures, wear breathable layers.")]
        [DataRow("snow", 79, 119, "Pack snowshoes, bring an extra gallon of water, wear breathable layers.")]
        [DataRow("rain", 90, 114, "Pack rain gear and wear waterproof shoes, bring an extra gallon of water, wear breathable layers.")]
        [DataRow("rain", 34, 58, "Pack rain gear and wear waterproof shoes, wear breathable layers.")]
        [DataRow("rain", 27, 54, "Pack rain gear and wear waterproof shoes, wear breathable layers.")]
        [DataRow("cloudy", 67, 88, "Bring an extra gallon of water, wear breathable layers.")]
        [DataRow("thunderstorms", 65, 118, "Seek shelter and avoid hiking on exposed ridges, bring an extra gallon of water, wear breathable layers.")]
        [DataRow("snow", 88, 102, "Pack snowshoes, bring an extra gallon of water.")]
        [DataRow("thunderstorms", 33, 102, "Seek shelter and avoid hiking on exposed ridges, bring an extra gallon of water, wear breathable layers.")]
        [DataRow("cloudy", 58, 94, "Bring an extra gallon of water, wear breathable layers.")]
        [DataRow("cloudy", 7, 86, "Bring an extra gallon of water, warning: danger of exposure to frigid temperatures, wear breathable layers.")]
        [DataRow("cloudy", 33, 70, "Wear breathable layers.")]
        [DataRow("partly cloudy", 73, 102, "Bring an extra gallon of water, wear breathable layers.")]
        [DataRow("snow", 49, 111, "Pack snowshoes, bring an extra gallon of water, wear breathable layers.")]
        [DataRow("sunny", 51, 100, "Bring an extra gallon of water, wear breathable layers.")]
        [DataRow("cloudy", 24, 24, "")]
        [DataRow("thunderstorms", 71, 99, "Seek shelter and avoid hiking on exposed ridges, bring an extra gallon of water, wear breathable layers.")]
        [DataRow("snow", 42, 51, "Pack snowshoes.")]
        [DataRow("thunderstorms", 47, 92, "Seek shelter and avoid hiking on exposed ridges, bring an extra gallon of water, wear breathable layers.")]
        [DataRow("snow", 81, 86, "Pack snowshoes, bring an extra gallon of water.")]
        public void CheckWeatherMessageString(string forecast, int low, int high, string expected)
        {
            Weather weather = new Weather()
            {
                Forecast = forecast,
                LowFarenheit = low,
                HighFarenheit = high
            };

            Assert.AreEqual(expected, weather.GetWeatherRecommendation());


        }







    }
}
