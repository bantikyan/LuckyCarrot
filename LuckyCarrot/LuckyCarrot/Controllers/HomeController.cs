using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LuckyCarrot.Models;
using DataAccess.Repositories.Interfaces;
using DataModels;

namespace LuckyCarrot.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IActionResult> Index()
        {
            int companyID = 1;

            var model = new WallModel();
            model.PointTransfers = await _unitOfWork.PointRepository.GetTransfers(companyID);

            var reasons = await _unitOfWork.PointRepository.GetReasons(companyID);
            var users = await _unitOfWork.UserRepository.Get(companyID);

            model.Reasons = reasons.ToDictionary(p => p.Id, pp => pp.Name);
            model.Users = users.ToDictionary(p => p.Id, pp => pp.FirstName + " " + pp.LastName);

            return View(model);
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
    }
}
