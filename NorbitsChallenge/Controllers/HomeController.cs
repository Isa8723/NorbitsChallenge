using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Dal;
using NorbitsChallenge.Helpers;
using NorbitsChallenge.Models;

namespace NorbitsChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            var model = GetCompanyModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult Index(int companyId, string licensePlate)
        {
            List<string> licensePl = new CarDb(_config).GetList(companyId);
            bool lPexists = licensePl.Contains(licensePlate);

            var model = GetCompanyModel();
            model.LPexists = lPexists;

            if (lPexists)
            {
                var tireCount = new CarDb(_config).GetTireCount(companyId, licensePlate);
                var description = new CarDb(_config).GetDescription(companyId, licensePlate);
                var carmodel = new CarDb(_config).GetModel(companyId, licensePlate);
                var brand = new CarDb(_config).GetBrand(companyId, licensePlate);

                model.TireCount = tireCount;
                model.Description = description;
                model.Carmodel = carmodel;
                model.Brand = brand;
            }

                return Json(model);
        }

        [HttpPost]
        public JsonResult List(int companyId)
        {
            List<string> licensePl = new CarDb(_config).GetList(companyId);

            var model = GetCompanyModel();
            model.LicensePl = licensePl;

            return Json(model);
        }

        [HttpPost]
        public JsonResult AddCar(int companyId, string licensePlate, string description, string carmodel, string brand, int tireCount)
        {
            string addCarReturn = new CarDb(_config).AddNewCar(companyId, licensePlate, description, carmodel, brand, tireCount);

            var model = GetCompanyModel();
            model.AddCarReturn = addCarReturn;

            return Json(model);
        }

        [HttpPost]
        public JsonResult DeleteACar(int companyId, string licensePlate)
        {
            List<string> licensePl = new CarDb(_config).GetList(companyId);
            bool lPexists = licensePl.Contains(licensePlate);

            var model = GetCompanyModel();
            string deleteCarReturn;

            if (lPexists)
            {
                deleteCarReturn = new CarDb(_config).DeleteCar(companyId, licensePlate);
            }
            else
            {
                deleteCarReturn = "No car registered with this license plate.";
            }

            model.DeleteCarReturn = deleteCarReturn;

            return Json(model);
        }

        [HttpPost]
        public JsonResult ModifyACar(int companyId, string licensePlate, string description, string carmodel, string brand, int tireCount)
        {
            List<string> licensePl = new CarDb(_config).GetList(companyId);
            bool lPexists = licensePl.Contains(licensePlate);

            var model = GetCompanyModel();
            string modifyCarReturn;

            if (lPexists)
            {
                modifyCarReturn = new CarDb(_config).ModifyCar(companyId, licensePlate, description, carmodel, brand, tireCount);
            }
            else
            {
                modifyCarReturn = "No car registered with this license plate.";
            }

            model.ModifyCarReturn = modifyCarReturn;

            return Json(model);
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private HomeModel GetCompanyModel()
        {
            var companyId = UserHelper.GetLoggedOnUserCompanyId();
            var companyName = new SettingsDb(_config).GetCompanyName(companyId);
            return new HomeModel { CompanyId = companyId, CompanyName = companyName };
        }
    }
}
