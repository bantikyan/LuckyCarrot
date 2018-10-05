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

        public async Task<ActionResult> UserDDL(string searchTerm, int pageSize, int pageNum, int companyId)
        {

            //List<NameAndIDModel> lst = null;
            //if (string.IsNullOrEmpty(searchTerm) && pageNum == 1)
            //{
            //    var products = await unitOfWork.ProductRepository.GetAll(whiteLabelID, excludeID: excludeID, licenseType: licenseType);
            //    if (includeAll)
            //    {
            //        products.Insert(0, new ProductModel { ID = 0, Title = "All" });
            //    }
            //    lst = products.Select(x => new NameAndIDModel { ID = x.ID, Name = x.SubTitle != null ? x.Title + " (" + x.SubTitle + ")" : x.Title }).ToList();

            //    cacheManager.ProductDDL = lst;
            //}
            //else
            //{
            //    lst = cacheManager.ProductDDL as List<NameAndIDModel>;
            //}

            var items = await _unitOfWork.UserRepository.Get(companyId);

            //Search
            if (searchTerm != null)
                searchTerm = searchTerm.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                items = items.Where(s => s.FirstName.ToLower().Contains(searchTerm) || s.LastName.ToLower().Contains(searchTerm)).ToList();
            }

            Select2PagedResult pItems = new Select2PagedResult();
            pItems.Results = items.Skip(pageSize * (pageNum - 1))
                                        .Take(pageSize)
                                        .Select(e => new Select2Result
                                        {
                                            id = e.Id.ToString(),
                                            text = e.FirstName + " " + e.LastName,
                                        }).ToList();

            pItems.Total = items.Count();
            return Json(pItems);
        }

        public async Task<ActionResult> ReasonDDL(string searchTerm, int pageSize, int pageNum, int companyId)
        {

            //List<NameAndIDModel> lst = null;
            //if (string.IsNullOrEmpty(searchTerm) && pageNum == 1)
            //{
            //    var products = await unitOfWork.ProductRepository.GetAll(whiteLabelID, excludeID: excludeID, licenseType: licenseType);
            //    if (includeAll)
            //    {
            //        products.Insert(0, new ProductModel { ID = 0, Title = "All" });
            //    }
            //    lst = products.Select(x => new NameAndIDModel { ID = x.ID, Name = x.SubTitle != null ? x.Title + " (" + x.SubTitle + ")" : x.Title }).ToList();

            //    cacheManager.ProductDDL = lst;
            //}
            //else
            //{
            //    lst = cacheManager.ProductDDL as List<NameAndIDModel>;
            //}

            var items = await _unitOfWork.PointRepository.GetReasons(companyId);

            //Search
            if (searchTerm != null)
                searchTerm = searchTerm.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                items = items.Where(s => s.Name.ToLower().Contains(searchTerm)).ToList();
            }

            Select2PagedResult pItems = new Select2PagedResult();
            pItems.Results = items.Skip(pageSize * (pageNum - 1))
                                        .Take(pageSize)
                                        .Select(e => new Select2Result
                                        {
                                            id = e.Id.ToString(),
                                            text = e.Name,
                                        }).ToList();

            pItems.Total = items.Count();
            return Json(pItems);
        }

        public IActionResult Redeem()
        {

            return View();
        }
        public IActionResult MyProfile()
        {

            return View();
        }
    }
}
