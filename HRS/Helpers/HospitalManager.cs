using HRS.Data;
using HRS.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Helpers
{
    public interface IHospitalManager
    {
        ManagerStatus CreateHospital(Hospital hospital);
        ManagerStatus CreateHospital(Hospital hospital, List<Clinic> clinic);
        ManagerStatus CreateClinic(Clinic clinic);
    }

    public class HospitalManager:IHospitalManager
    {
        private IHttpContextAccessor accessor;
        private HttpContext HttpContext;
        private readonly ManagerContext context;
        private readonly ISessionManager sessionManager;

        public HospitalManager(IHttpContextAccessor _accessor, ManagerContext _context, ISessionManager _sessionManager)
        {
            accessor = _accessor;
            HttpContext = accessor.HttpContext;
            context = _context;
            sessionManager = _sessionManager;
        } 

        public ManagerStatus CreateHospital(Hospital hospital)
        {
            if (context.Hospitals.Any(x => x.Name == hospital.Name))
                return ManagerStatus.UNKNOWN;
            hospital.CreatedAt = DateTime.Now;
            hospital.City = context.Cities.FirstOrDefault(x => x.Id == hospital.City.Id);
            hospital.Town = context.Towns.FirstOrDefault(x => x.Id == hospital.Town.Id);
            context.Add(hospital);
            context.SaveChanges();
            return ManagerStatus.OK;
        }

        public ManagerStatus CreateHospital(Hospital hospital, List<Clinic> clinics)
        {
            if (context.Hospitals.Any(x => x.Name == hospital.Name))
                return ManagerStatus.UNKNOWN;          
            hospital.CreatedAt = DateTime.Now;
            hospital.City = context.Cities.FirstOrDefault(x => x.Id == hospital.City.Id);
            hospital.Town = context.Towns.FirstOrDefault(x => x.Id == hospital.Town.Id);
            hospital.HospitalClinics = new List<HospitalClinic>();
            foreach (var clinic in clinics)
            {
                var link = new HospitalClinic()
                {
                    Clinic = clinic,
                    Hospital = hospital
                };
                hospital.HospitalClinics.Add(link);
            }
            context.Add(hospital);
            context.SaveChanges();
            return ManagerStatus.OK;
        }

        public ManagerStatus CreateClinic(Clinic clinic)
        {
            if (context.Clinics.Any(x => x.Name == clinic.Name))
                return ManagerStatus.UNKNOWN;
            clinic.CreatedAt = DateTime.Now;
            context.Add(clinic);
            context.SaveChanges();
            return ManagerStatus.OK;
        }
    }
}
