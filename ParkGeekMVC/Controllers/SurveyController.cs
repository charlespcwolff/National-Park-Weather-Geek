using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkGeek.DAL;
using ParkGeek.DAL.Models;
using ParkGeekMVC.Models;
using Security.DAO;

namespace ParkGeekMVC.Controllers
{
    public class SurveyController : AuthenticationController
    {
        private readonly IParkGeekDAO _dao = null;

        public SurveyController(IParkGeekDAO dao, IUserSecurityDAO db, IHttpContextAccessor httpContext) : base(db, httpContext)
        {
            _dao = dao;
        }

        /// <summary>
        /// Main page for taking the favorite parks survey.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            SurveyViewModel vm = GetSurveyModel();
            return GetAuthenticatedView("Index", vm);
        }

        /// <summary>
        /// For adding survey to the database.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSurvey(SurveyViewModel vm)
        {
            IActionResult result = null;

            if (ModelState.IsValid)
            {
                try
                {
                    Survey survey = new Survey();
                    survey.ParkCode = vm.ParkCode;
                    survey.EmailAddress = vm.EmailAddress;
                    survey.State = vm.State;
                    survey.ActivityLevel = vm.ActivityLevel;
                    _dao.AddSurvey(survey);

                    result = RedirectToAction("FavoriteParks");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Invalid Survey.", ex.Message);
                }
            }
            else
            {
                vm = GetSurveyModel();
                result = View("Index", vm);
            }

            return result;
        }

        /// <summary>
        /// Displays a list of parks ranked by the most
        /// votes for them.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FavoriteParks()
        {
            IList<SurveySubtotal> vm = _dao.GetSurveySubtotals();
            return GetAuthenticatedView("FavoriteParks", vm);
        }

        /// <summary>
        /// Retrieve the survey view model for the survey form methods.
        /// </summary>
        /// <returns></returns>
        private SurveyViewModel GetSurveyModel()
        {
            SurveyViewModel vm = new SurveyViewModel();
            IList<Park> parksFound = _dao.GetParks();
            foreach (var park in parksFound)
            {
                SelectListItem item = new SelectListItem();
                item.Text = park.ParkName;
                item.Value = park.ParkCode;
                vm.Parks.Add(item);
            }

            return vm;
        }
    }
}