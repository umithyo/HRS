using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRS.Filters;
using HRS.Data;
using HRS.Helpers;
using Microsoft.AspNetCore.Mvc;
using static HRS.Helpers.Utils;

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

        [PermissionAuthorize(Permissions = new string[] { RoleConfig.Admin }, RedirectUri = "/Home/Login")]
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
    }
}