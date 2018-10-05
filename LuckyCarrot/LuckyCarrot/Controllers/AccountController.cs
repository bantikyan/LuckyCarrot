using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using DataModels;
using DataModels.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LuckyCarrot.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper)
            : base(unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        #region Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null, bool provider = false)
        {
            ViewBag.HideFooter = true;
            ViewData["ReturnUrl"] = returnUrl;
            RegisterModel model = new RegisterModel { IsActive = true };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleEnum.User.ToString());
                    //if (model.IsProfessional)
                    //{
                    //    await _userManager.AddToRoleAsync(user, RoleEnum.Provider.ToString());
                    //}
                    
                    
                    return RedirectToAction("CheckEmail");
                }
            }
            ViewBag.HideFooter = true;
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion
    }
}