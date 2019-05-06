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
        ManagerStatus UpdateHospital(Hospital hospital, List<Clinic> clinic);
        ManagerStatus RemoveHospital(int id);
        ManagerStatus CreateClinic(Clinic clinic);
        ManagerStatus UpdateClinic(int id, Clinic clinic);
        ManagerStatus RemoveClinic(int id);
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
                return ManagerStatus.EXISTS;
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
                return ManagerStatus.EXISTS;          
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

        public ManagerStatus UpdateHospital(Hospital hospital, List<Clinic> clinics)
        {
            if (!context.Hospitals.Any(x => x.Id == hospital.Id))
                return ManagerStatus.NOT_FOUND;
            
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
            hospital.City = context.Cities.FirstOrDefault(x => x.Id == hospital.City.Id);
            hospital.Town = context.Towns.FirstOrDefault(x => x.Id == hospital.Town.Id);
            hospital.Name = hospital.Name;
            context.SaveChanges();
            return ManagerStatus.OK;
        }

        public ManagerStatus RemoveHospital(int id)
        {
            var hospital = context.Hospitals.FirstOrDefault(x => x.Id == id);
            if (hospital == null)
                return ManagerStatus.NOT_FOUND;
            context.Remove(hospital);
            context.SaveChanges();
            return ManagerStatus.OK;
        }

        public ManagerStatus CreateClinic(Clinic clinic)
        {
            if (context.Clinics.Any(x => x.Name == clinic.Name))
                return ManagerStatus.NOT_FOUND;
            clinic.CreatedAt = DateTime.Now;
            context.Add(clinic);
            context.SaveChanges();
            return ManagerStatus.OK;
        }

        public ManagerStatus UpdateClinic(int id, Clinic _clinic)
        {
            var clinic = context.Clinics.FirstOrDefault(x => x.Id == id);
            if (clinic == null)
                return ManagerStatus.NOT_FOUND;
            clinic.Name = _clinic.Name;
            context.SaveChanges();
            return ManagerStatus.OK;
        }

        public ManagerStatus RemoveClinic(int id)
        {
            var clinic = context.Clinics.FirstOrDefault(x => x.Id == id);
            if (clinic == null)
                return ManagerStatus.NOT_FOUND;
            context.Remove(clinic);
            context.SaveChanges(); 
            return ManagerStatus.OK;
        }
    }
}
