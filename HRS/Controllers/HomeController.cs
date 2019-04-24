using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRS.Data;
using HRS.Helpers;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            HttpContext.Session.
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