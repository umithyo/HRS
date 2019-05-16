﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRS.Data;
using HRS.Filters;
using HRS.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HRS.Data.Constants;
using static HRS.Helpers.Utils;

namespace HRS.Controllers
{
    [PermissionAuthorize(Permissions = RoleConfig.Admin+","+RoleConfig.Operator, UnauthorizedRedirectUri = "/Home/Index")]
    public class ManagementController : Controller
    {
        private readonly ManagerContext context;
        private readonly ISessionManager sessionManager;
        private readonly IHospitalManager hospitalManager;
        public ManagementController(ManagerContext _context, ISessionManager _sessionManager, IHospitalManager _hospitalManager)
        {
            context = _context;
            sessionManager = _sessionManager;
            hospitalManager = _hospitalManager;
        }

        public IActionResult Index()
        {
            ViewBag.Counts = new Dictionary<string, int>
            {
                { "Hospitals", context.Hospitals.Count() },
                { "Users", context.Users.Count() },
                { "Polyclinics", context.Polyclinics.Count() },
                { "Clinics", context.Clinics.Count() },
                { "Cities", context.Cities.Count() },
                { "Towns", context.Towns.Count() },
            };
            return View();
        }

        public IActionResult Hospitals()
        {
            ViewBag.Cities = context.Cities.ToList();
            ViewBag.Clinics = context.Clinics.ToList();
            return View();
        }

        public IActionResult Clinics()
        {
            return View();
        }

        public IActionResult Users()
        {
            ViewBag.Clinics = context.Clinics.ToList();
            ViewBag.Hospitals = context.Hospitals.ToList();
            return View();
        }

        public IActionResult Polyclinics()
        {
            ViewBag.Clinics = context.Clinics.ToList();
            ViewBag.Hospitals = context.Hospitals.ToList();
            return View();
        }

        public IActionResult Appointments()
        {
            return View();
        }
    }
}