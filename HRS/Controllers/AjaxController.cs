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
        private readonly IUserManager userManager;
        private readonly IAppointmentManager appointmentManager;
        private readonly ISessionManager sessionManager;

        public AjaxController(
            ManagerContext _context,
            IHospitalManager _hospitalManager,
            IUserManager _userManager,
            IAppointmentManager appointmentManager,
            ISessionManager sessionManager)
        {
            context = _context;
            hospitalManager = _hospitalManager;
            userManager = _userManager;
            this.appointmentManager = appointmentManager;
            this.sessionManager = sessionManager;
        }
        #region CRUD

        #region Cities
        [HttpGet("GetCities")]
        public IActionResult GetCities()
        {
            var cities = context.Cities.ToList();
            return Ok(cities);
        }
        #endregion

        #region Towns
        [HttpGet("GetTowns/{id}")]
        public IActionResult GetTowns(int? id)
        {
            if (id == null)
                return BadRequest("Hatalı id.");
            var towns = context.Towns.Where(x => x.City.Id == id).ToList();
            return Ok(towns);
        }
        #endregion

        #region Hospitals
        [HttpGet("GetHospitals")]
        public IActionResult GetHospitals()
        {
            var hospitals = context.Hospitals
                .Include(x => x.City)
                .Include(x => x.Town)
                .Select(x => new { id = x.Id, name = x.Name, cityName = x.City.Name, townName = x.Town.Name, createdAt = x.CreatedAt })
                .OrderByDescending(x => x.createdAt)
                .ToList();
            return Ok(hospitals);
        }

        [HttpGet("GetHospital/{id}")]
        public IActionResult GetHospital(int id)
        {
            var hospital = context.Hospitals
                .Select(x => new { x.Id, x.Name, cityId = x.City.Id, townId = x.Town.Id, x.CreatedAt, clinics = x.HospitalClinics.Where(k => k.HospitalId == id).Select(k => k.Clinic).ToList() })
                .FirstOrDefault(x => x.Id == id);
            if (hospital == null)
                return NotFound();
            return Ok(hospital);
        }

        [HttpGet("GetHospitalByCity/{id}")]
        public IActionResult GetHospitalByCity(int id)
        {
           
            var hospitals = context.Hospitals.Include(x => x.City).Where(x => x.City.Id == id).ToList();
            return Ok(hospitals);
        }

        [HttpGet("GetHospitalByTown/{id}")]
        public IActionResult GetHospitalByTown(int id)
        {
            var hospitals = context.Hospitals.Include(x => x.Town).Where(x => x.Town.Id == id).ToList();
            return Ok(hospitals);
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

        [HttpPut("UpdateHospital/{id}")]
        public IActionResult UpdateHospital(int id, [FromForm] Hospital hospital, [FromForm] IFormCollection form)
        {
            var form_clinics = form["Clinics"];
            var clinics = new List<Clinic>();
            foreach (var item in form_clinics)
            {
                var converted = Int32.TryParse(item, out int cid);
                if (converted)
                {
                    var clinic = context.Clinics.FirstOrDefault(x => x.Id == cid);
                    clinics.Add(clinic);
                }
            }
            hospital.Id = id;
            var status = hospitalManager.UpdateHospital(hospital, clinics);
            if (status == ManagerStatus.OK)
                return Ok();
            else
                return BadRequest(GetErrorString(status));
        }

        [HttpDelete("DeleteHospital")]
        public IActionResult DeleteHospital([FromBody] JObject hospitals)
        {
            var error = "";
            foreach (var item in hospitals["hospitals"])
            {
                var status = hospitalManager.RemoveHospital(Convert.ToInt32(item["id"]));
                if (status != ManagerStatus.OK)
                    error = GetErrorString(status);
            }

            if (string.IsNullOrEmpty(error))
                return Ok();
            return BadRequest(error);
        }
        #endregion

        #region Clinics
        [HttpGet("GetClinics")]
        public IActionResult GetClinics()
        {
            return Ok(context.Clinics.ToList());
        }

        [HttpGet("GetClinic/{id}")]
        public IActionResult GetClinic(int id)
        {
            var clinic = context.Clinics
                .Select(x => new { x.Id, x.Name, x.CreatedAt, hospitals = x.HospitalClinics.Where(k=>k.ClinicId == id).Select(k => k.Hospital).ToList() })
                .FirstOrDefault(x => x.Id == id);
                
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
        public IActionResult UpdateClinic(int id, [FromForm] Clinic clinic)
        {
            var status = hospitalManager.UpdateClinic(id, clinic);
            if (status == ManagerStatus.OK)
                return Ok();
            else
                return BadRequest(GetErrorString(status));
        }

        [HttpDelete("DeleteClinic")]
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
            return BadRequest(error);
        }
        #endregion

        #region Users
        [HttpGet("GetUsers/{byRole?}")]
        public IActionResult GetUsers(string byRole)
        {
            if (!string.IsNullOrEmpty(byRole))
            {
                return Ok(context.Users.Include(x=>x.UserInfo).Where(x => x.Role == byRole).ToList());
            }
            return Ok(context.Users.Include(x=>x.UserInfo).ToList());
        }

        [HttpGet("GetUser/{id}")]
        public IActionResult GetUser(Guid id)
        {
            var user = context.Users.Include(x=>x.UserInfo).FirstOrDefault(x => x.Id == id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromForm] User user)
        {
            var status = userManager.Register(user, user.UserInfo);
            if (status == ManagerStatus.OK)
                return Ok();
            else
                return BadRequest(GetErrorString(status));
        }

        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(Guid id, [FromForm] User _user)
        {

            _user.Id = id;
            var status = userManager.UpdateUser(_user, _user.UserInfo);
            if (status == ManagerStatus.OK)
                return Ok();
            else
                return BadRequest(GetErrorString(status));
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser([FromBody] JObject users)
        {
            var error = "";
            foreach (var item in users["users"])
            {
                var status = userManager.RemoveUser(new Guid(item["id"].ToString()));
                if (status != ManagerStatus.OK)
                    error = GetErrorString(status);
            }

            if (string.IsNullOrEmpty(error))
                return Ok();
            return BadRequest(error);
        }
        #endregion
        #region Polyclinics
        [HttpGet("GetPolyclinics")]
        public IActionResult GetPolyclinics()
        {
            return Ok(context.Polyclinics.Include(x=>x.Hospital).Include(x=>x.Clinic).ToList());
        }

        [HttpGet("GetPolyclinic/{id}")]
        public IActionResult GetPolyclinic(int id)
        {
            var polyclinic = context.Polyclinics
                .Include(x=>x.Clinic)
                .Include(x=>x.Hospital)
                .FirstOrDefault(x => x.Id == id);

            if (polyclinic == null)
                return NotFound();
            return Ok(polyclinic);
        }

        [HttpPost("CreatePolyclinic")]
        public IActionResult CreatePolyclinic([FromForm] Polyclinic polyclinic)
        {
            var status = hospitalManager.CreatePolyclinic(polyclinic);
            if (status == ManagerStatus.OK)
                return Ok();
            else
                return BadRequest(GetErrorString(status));
        }

        [HttpPut("UpdatePolyclinic/{id}")]
        public IActionResult UpdatePolyclinic(int id, [FromForm] Polyclinic polyclinic)
        {
            var status = hospitalManager.UpdatePolyclinic(id, polyclinic);
            if (status == ManagerStatus.OK)
                return Ok();
            else
                return BadRequest(GetErrorString(status));
        }

        [HttpDelete("DeletePolyclinic")]
        public IActionResult DeletePolyclinic([FromBody] JObject polyclinics)
        {
            var error = "";
            foreach (var item in polyclinics["polyclinics"])
            {
                var status = hospitalManager.RemovePolyclinic(Convert.ToInt32(item["id"]));
                if (status != ManagerStatus.OK)
                    error = GetErrorString(status);
            }

            if (string.IsNullOrEmpty(error))
                return Ok();
            return BadRequest(error);
        }
        #endregion
        #endregion CRUD

        #region Appointments
        [HttpGet("GetDoctorAppointments/{id}")]
        public IActionResult GetDoctorAppointments(Guid id)
        {
            if (userManager.GetUser(id) == null)
                return NotFound();
            return Ok(appointmentManager.GetDoctorAppointments(userManager.GetUser(id)));
        }

        [HttpGet("GetAppointment/{id}")]
        public IActionResult GetAppointment(Guid id)
        {
            var appointment = context.Appointments
                .Include(x=>x.Doctor)
                    .ThenInclude(x=>x.UserInfo)
                .Include(x=>x.Patient)
                    .ThenInclude(x=>x.UserInfo)
                .FirstOrDefault(x => x.Id == id);
            if (appointment == null)
                return NotFound();
            return Ok(appointment);
        }

        [HttpGet("GetAllAppointments")]
        public IActionResult GetAllAppointments()
        {
            var appointments = context.Appointments
                .Include(x => x.Doctor)
                    .ThenInclude(x => x.UserInfo)
                        .ThenInclude(x => x.Clinic)
                .Include(x => x.Doctor)
                    .ThenInclude(x => x.UserInfo)
                        .ThenInclude(x => x.Hospital)
                .Include(x => x.Patient)
                    .ThenInclude(x=>x.UserInfo)
                .Select(x => new {
                    id = x.Id,
                    createdAt = x.CreatedAt,
                    doctor = x.Doctor.UserInfo.Name + " " + x.Doctor.UserInfo.Surname,
                    patient = x.Patient.UserInfo.Name + " " + x.Patient.UserInfo.Surname,
                    time = x.Time,
                    hospital = x.Doctor.UserInfo.Hospital.Name,
                    polyclinic = hospitalManager.GetPolyclinic(x.Doctor.UserInfo.Hospital, x.Doctor.UserInfo.Clinic) != null ?
                        hospitalManager.GetPolyclinic(x.Doctor.UserInfo.Hospital, x.Doctor.UserInfo.Clinic).Name :
                        x.Doctor.UserInfo.Clinic.Name });
            return Ok(appointments);
        }

        [HttpPost("CreateAppointment/{id}")]
        public IActionResult CreateAppointment(Guid id, Appointment appointment)
        {
            if (userManager.GetUser(id) == null)
                return NotFound();
            appointment.Doctor = userManager.GetUser(id);
            appointment.Patient = sessionManager.GetUser();
            var status = appointmentManager.CreateAppointment(appointment);
            if (status != ManagerStatus.OK)
                return BadRequest(GetErrorString(status));
            return Ok();
        }

        [HttpPut("UpdateAppointment/{id}")]
        public IActionResult UpdateAppointment(Guid id, Appointment appointment)
        {
            appointment.Doctor = userManager.GetUser(id);
            appointment.Patient = sessionManager.GetUser();
            var status = appointmentManager.UpdateAppointment(id, appointment);
            if (status != ManagerStatus.OK)
                return BadRequest(GetErrorString(status));
            return Ok();
        }

        [HttpDelete("DeleteAppointment")]
        public IActionResult DeleteAppointment([FromBody] JObject appointments)
        {
            var error = "";
            foreach (var item in appointments["appointments"])
            {
                var status = appointmentManager.RemoveAppointment(new Guid (item["id"].ToString()));
                if (status != ManagerStatus.OK)
                    error = GetErrorString(status);
            }

            if (string.IsNullOrEmpty(error))
                return Ok();
            return BadRequest(error);
        }
        #endregion
    }
}