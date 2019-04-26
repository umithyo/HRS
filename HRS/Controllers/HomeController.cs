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

namespace HRS
{
    public class HomeController : Controller
    {
        private readonly ManagerContext context;
        private readonly IUserManager userManager;
       
        public HomeController(ManagerContext _context, IUserManager _userManager)
        {
            context = _context;
            userManager = _userManager;
        }

        [PermissionAuthorize]
        public IActionResult Index()
        {
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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Register(LoginVM vm)
        {
            var status = userManager.Register(vm.User, vm.UserInfo);
            if (status != ManagerStatus.OK)
            {
                ViewBag.Error = userManager.GetErrorString(status);
                return View(vm);
            }
            return RedirectToAction(nameof(Login), vm.User);
        }
    }
}