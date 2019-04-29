using System;
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
using static HRS.Helpers.Utils;

namespace HRS.Controllers
{
    [Route("api")]
    [ApiController]
    //[PermissionAuthorize(IsApi = true)]
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
                .Include(x=>x.Town)
                .Select(x => new { id = x.Id, name = x.Name, cityName = x.City.Name, townName = x.Town.Name, createdAt = x.CreatedAt });
            return Ok(hospitals);
        }

        [HttpPost("CreateHospital")]
        public IActionResult CreateHospital([FromForm] IFormCollection form)
        {
            var hospital = new Hospital();
            int cityId, townId;

            if (!Int32.TryParse(form["City.Id"].ToString(), out cityId) || !Int32.TryParse(form["Town.Id"].ToString(), out townId))
                return BadRequest("Hatalı şehir ve/veya ilçe seçimi.");
            hospital.Name = form["Name"];
            hospital.City = context.Cities.FirstOrDefault(x=>x.Id == cityId);
            hospital.Town = context.Towns.FirstOrDefault(x => x.Id == townId);
            hospitalManager.CreateHospital(hospital);
            return Ok();
        }
    }
}