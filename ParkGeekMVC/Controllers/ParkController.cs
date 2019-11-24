using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkGeek.DAL;
using ParkGeek.DAL.Models;
using ParkGeekMVC.Models;
using Security.DAO;

namespace ParkGeekMVC.Controllers
{
    public class ParkController : AuthenticationController
    {
        private readonly IParkGeekDAO _dao = null;
        private const string UseCelciusKey = "UseCelcius";

        public ParkController(IParkGeekDAO dao, IUserSecurityDAO db, IHttpContextAccessor httpContext) : base(db, httpContext)
        {
            _dao = dao;
        }


        /// <summary>
        /// Main page after login for displaying a list for
        /// all parks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            IList<Park> parks = _dao.GetParks();
            return GetAuthenticatedView("Index", parks);
        }

        /// <summary>
        /// Page for displaying the details and weather of a park.
        /// </summary>
        /// <param name="parkCode"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Detail(string parkCode)
        {
            DetailViewModel vm = new DetailViewModel();
            vm.Park = _dao.GetPark(parkCode);
            vm.Forecast = _dao.GetWeathers(parkCode);

            if (GetSessionData<bool>(UseCelciusKey))
            {
                vm.PrefersFarenheit = false;
            }
            
            return GetAuthenticatedView("Detail", vm);
        }

        /// <summary>
        /// Switching the tempature units on the details page
        /// and saves the users preference for the rest of the
        /// session.
        /// </summary>
        /// <param name="parkCode"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SwitchDetailTemp(string parkCode)
        {
            if (GetSessionData<bool>(UseCelciusKey))
            {
                SetSessionData(UseCelciusKey, false);
            }
            else
            {
                SetSessionData(UseCelciusKey, true);
            }
            
            return RedirectToAction("Detail", new { @parkCode = parkCode});
        }
    }
}
