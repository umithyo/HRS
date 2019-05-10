using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRS.Filters;
using HRS.Data;
using HRS.Helpers;
using Microsoft.AspNetCore.Mvc;
using static HRS.Helpers.Utils;
using HRS.Models.ViewModels;
using HRS.Models;
using Microsoft.EntityFrameworkCore;
using static HRS.Data.Constants;

namespace HRS
{
    public class HomeController : Controller
    {
        private readonly ManagerContext context;
        private readonly IUserManager userManager;
        private readonly ISessionManager sessionManager;
       
        public HomeController(ManagerContext _context, IUserManager _userManager, ISessionManager _sessionManager)
        {
            context = _context;
            userManager = _userManager;
            sessionManager = _sessionManager;
        }

        [PermissionAuthorize]
        [RedirectFilter(Permissions = RoleConfig.Admin, AuthorizedRedirectUri = "/Management/Index", RedirectOnce = true)]
        public IActionResult Index()
        {
            ViewBag.Cities = context.Cities.ToList();
            ViewBag.Clinics = context.Clinics.ToList();
            ViewBag.Hospitals = context.Hospitals.ToList();
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
       
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult SignOut()
        {
            sessionManager.SetLoggedOut();
            return RedirectToAction(nameof(Login));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(User user)
        {
            var status = userManager.SignIn(user);
            if (status != ManagerStatus.OK)
            {
                ViewBag.Error = GetErrorString(status);
                return View(user);
            }
            return RedirectToAction(nameof(Index));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Register(LoginVM vm)
        {
            var status = userManager.Register(vm.User, vm.UserInfo);
            if (status != ManagerStatus.OK)
            {
                ViewBag.Error = GetErrorString(status);
                return View(vm);
            }
            return RedirectToAction(nameof(Login));
        }
    }
}