using HRS.Data;
using HRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        ManagerStatus CreatePolyclinic(Polyclinic clinic);
        ManagerStatus UpdatePolyclinic(int id, Polyclinic clinic);
        ManagerStatus RemovePolyclinic(int id);
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

        public ManagerStatus UpdateHospital(Hospital _hospital, List<Clinic> clinics)
        {
            if (!context.Hospitals.Any(x => x.Id == _hospital.Id))
                return ManagerStatus.NOT_FOUND;
            var hospital = context.Hospitals.Include(x=>x.HospitalClinics).FirstOrDefault(x => x.Id == _hospital.Id);
            hospital.HospitalClinics.Clear();
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
            hospital.City = context.Cities.FirstOrDefault(x => x.Id ==_hospital.City.Id);
            hospital.Town = context.Towns.FirstOrDefault(x => x.Id == _hospital.Town.Id);
            hospital.Name = _hospital.Name;
            context.Update(hospital);
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
                return ManagerStatus.EXISTS;
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

        public ManagerStatus CreatePolyclinic(Polyclinic polyclinic)
        {
            if (context.Polyclinics.Any(x => x.Name == polyclinic.Name))
                return ManagerStatus.EXISTS;
            polyclinic.CreatedAt = DateTime.Now;
            context.Add(polyclinic);
            context.SaveChanges();
            return ManagerStatus.OK;
        }

        public ManagerStatus UpdatePolyclinic(int id, Polyclinic _polyclinic)
        {
            var polyclinic = context.Polyclinics.FirstOrDefault(x => x.Id == id);
            if (polyclinic == null)
                return ManagerStatus.NOT_FOUND;
            polyclinic.Name = _polyclinic.Name;
            polyclinic.Clinic = context.Clinics.Find(_polyclinic.ClinicId);
            polyclinic.Hospital = context.Hospitals.Find(_polyclinic.HospitalId);
            context.SaveChanges();
            return ManagerStatus.OK;
        }

        public ManagerStatus RemovePolyclinic(int id)
        {
            var polyclinic = context.Polyclinics.FirstOrDefault(x => x.Id == id);
            if (polyclinic == null)
                return ManagerStatus.NOT_FOUND;
            context.Remove(polyclinic);
            context.SaveChanges();
            return ManagerStatus.OK;
        }
    }
}
