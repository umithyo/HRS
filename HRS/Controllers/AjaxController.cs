using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRS.Data;
using HRS.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRS.Controllers
{
    [Route("api")]
    [ApiController]
    [PermissionAuthorize]
    public class AjaxController : ControllerBase
    {
        private readonly ManagerContext context;

        public AjaxController(ManagerContext _context)
        {
            context = _context;
        }

        [HttpGet("GetTowns/{id}")]
        public IActionResult GetTowns(int? id)
        {
            if (id == null)
                return BadRequest("Hatalı id.");               
            var towns = context.Towns.Where(x => x.City.Id == id).ToList();
            return Ok(towns);
        }
    }
}