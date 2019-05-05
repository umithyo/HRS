﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRS.Data;
using HRS.Filters;
using HRS.Helpers;
using HRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using static HRS.Helpers.Utils;

namespace HRS.Controllers
{
    [Route("api")]
    [ApiController]
    //[PermissionAuthorize]
    public class AjaxController : ControllerBase
    {
        private readonly ManagerContext context;
        private readonly IHospitalManager hospitalManager;

        public AjaxController(ManagerContext _context, IHospitalManager _hospitalManager)
        {
            context = _context;
            hospitalManager = _hospitalManager;
        }

        [HttpGet("GetCities")]
        public IActionResult GetCities()
        {
            var cities = context.Cities.ToList();
            return Ok(cities);
        }

        [HttpGet("GetTowns/{id}")]         
        public IActionResult GetTowns(int? id)
        {
            if (id == null)
                return BadRequest("Hatalı id.");               
            var towns = context.Towns.Where(x => x.City.Id == id).ToList();
            return Ok(towns);
        }

        [HttpGet("GetHospitals")]
        public IActionResult GetHospitals()
        {
            var hospitals = context.Hospitals
                .Include(x => x.City)
                .Include(x => x.Town)
                .Select(x => new { id = x.Id, name = x.Name, cityName = x.City.Name, townName = x.Town.Name, createdAt = x.CreatedAt })
                .OrderByDescending(x=>x.createdAt)
                .ToList();
            return Ok(hospitals);
        }

        [HttpGet("GetHospital/{id}")]
        public IActionResult GetHospital(int id)
        {
            var hospital = context.Hospitals.FirstOrDefault(x => x.Id == id);
            if (hospital == null)
                return NotFound();
            return Ok(hospital);
        }

        [HttpPost("CreateHospital")]
        public IActionResult CreateHospital([FromForm] Hospital hospital, [FromForm] IFormCollection form)
        {
            var form_clinics = form["Clinics"];
            var clinics = new List<Clinic>();
            foreach (var item in form_clinics)
            {
                var converted = Int32.TryParse(item, out int id);
                if (converted)
                {
                    var clinic = context.Clinics.FirstOrDefault(x => x.Id == id);
                    clinics.Add(clinic);
                }
            }
            var status = hospitalManager.CreateHospital(hospital, clinics);
            if (status == ManagerStatus.OK)
                return Ok();
            else
                return BadRequest(GetErrorString(status));
        }

        [HttpGet("GetClinics")]
        public IActionResult GetClinics()
        {
            return Ok(context.Clinics.ToList());
        }

        [HttpGet("GetClinic/{id}")]
        public IActionResult GetClinic(int id)
        {
            var clinic = context.Clinics.FirstOrDefault(x => x.Id == id);
            if (clinic == null)
                return NotFound();
            return Ok(clinic);
        }

        [HttpPost("CreateClinic")]
        public IActionResult CreateClinic([FromForm] Clinic clinic)
        {
            var status = hospitalManager.CreateClinic(clinic);
            if (status == ManagerStatus.OK)
                return Ok();
            else
                return BadRequest(GetErrorString(status));
        }

        [HttpPut("UpdateClinic/{id}")]
        public IActionResult UpdateClinic (int id, [FromForm] Clinic clinic)
        {
            var status = hospitalManager.UpdateClinic(id, clinic);
            if (status == ManagerStatus.OK)
                return Ok();
            else
                return BadRequest(GetErrorString(status));
        }

        [HttpPost("DeleteClinic")]
        public IActionResult DeleteClinic([FromBody] JObject clinics)
        {
            var error = "";
            foreach (var item in clinics["clinics"])
            {
                var status = hospitalManager.RemoveClinic(Convert.ToInt32(item["id"]));
                if (status != ManagerStatus.OK)
                    error = GetErrorString(status);
            }
            
            if (string.IsNullOrEmpty(error))
                return Ok();
            else
                return BadRequest(error);
        }
    }
}