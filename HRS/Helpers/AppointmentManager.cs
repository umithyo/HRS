using HRS.Data;
using HRS.Models;
using HRS.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Helpers
{
    public interface IAppointmentManager
    {
        Appointment GetDoctorLatestAppointment(User doctor);
        Appointment GetPatientLatestAppointment(User patient);
        IEnumerable<Appointment> GetDoctorAppointments(User doctor);
        IEnumerable<Appointment> GetPatientAppointments(User patient);
        IEnumerable<User> GetAvailableDoctors(FilterVM filterVM);
        ManagerStatus CreateAppointment(Appointment appointment);
        ManagerStatus UpdateAppointment(Guid id, Appointment _appointment);
        ManagerStatus RemoveAppointment(Guid id);
    }

    public class AppointmentManager:IAppointmentManager
    {
        private readonly ManagerContext context;

        public AppointmentManager(ManagerContext context)
        {
            this.context = context;
        }

        public Appointment GetDoctorLatestAppointment(User doctor)
        {
            var latest = context.Appointments.Where(x=>x.Doctor.Id == doctor.Id).OrderByDescending(x => x.Time).FirstOrDefault();
            return latest != null && latest.Time > DateTime.Now ? latest : null;
        }

        public Appointment GetPatientLatestAppointment(User patient)
        {
            return context.Appointments.Where(x => x.Patient.Id == patient.Id).OrderByDescending(x => x.Time).FirstOrDefault();
        }

        public IEnumerable<Appointment> GetDoctorAppointments(User doctor)
        {
            return context.Appointments.Where(x => x.Doctor.Id == doctor.Id).OrderByDescending(x => x.Time).ToList();
        }

        public IEnumerable<Appointment> GetPatientAppointments(User patient)
        {
            return context.Appointments
                .Include(x=>x.Doctor)
                    .ThenInclude(x=>x.UserInfo)
                        .ThenInclude(x=>x.Clinic)
                .Include(x => x.Doctor)
                    .ThenInclude(x => x.UserInfo)
                        .ThenInclude(x => x.Hospital)
                .Where(x => x.Patient.Id == patient.Id)
                .OrderByDescending(x => x.Time)
                .ToList();
        }

        public IEnumerable<User> GetAvailableDoctors(FilterVM filterVM)
        {
            var doctors = context.Users.Where(x => x.Role == "Doktor")
                .Include(x => x.UserInfo)
                    .ThenInclude(x => x.Clinic)
                .Include(x => x.UserInfo)
                    .ThenInclude(x => x.Hospital)
                        .ThenInclude(x => x.City)
                .Include(x=>x.UserInfo)
                    .ThenInclude(x=>x.Hospital)
                        .ThenInclude(x=>x.Town)
                .ToList();

            if (filterVM.ClinicId != null)
            {
                doctors = doctors.Where(x => x.UserInfo.ClinicId == filterVM.ClinicId).ToList();
            }

            if (filterVM.HospitalId != null)
            {
                doctors = doctors.Where(x => x.UserInfo.HospitalId == filterVM.HospitalId).ToList();
            }

            if (filterVM.CityId != null)
            {
                doctors = doctors.Where(x => x.UserInfo.Hospital.City.Id == filterVM.CityId).ToList();
            }

            if (filterVM.TownId != null)
            {
                doctors = doctors.Where(x => x.UserInfo.Hospital.Town.Id == filterVM.TownId).ToList();
            }
            return doctors;
        }

        public ManagerStatus CreateAppointment(Appointment appointment)
        {
            try
            {
                if (context.Appointments.Any(x => x.Time.CompareTo(appointment.Time) == 0))
                    return ManagerStatus.EXISTS;
                appointment.CreatedAt = DateTime.Now;
                context.Appointments.Add(appointment);
                context.SaveChanges();
                return ManagerStatus.OK;
            }
            catch (Exception e)
            {
                return ManagerStatus.UNKNOWN;
            }
        }

        public ManagerStatus UpdateAppointment(Guid id, Appointment _appointment)
        {
            var appointment = context.Appointments.Find(id);
            if (appointment == null)
                return ManagerStatus.NOT_FOUND;
            _appointment.Id = id;
            context.Update(_appointment);
            return ManagerStatus.OK;
        }

        public ManagerStatus RemoveAppointment(Guid id)
        {
            try
            {
                var appointment = context.Appointments.FirstOrDefault(x => x.Id == id);
                if (appointment == null)
                    return ManagerStatus.NOT_FOUND;
                context.Remove(appointment);
                context.SaveChanges();
                return ManagerStatus.OK;
            }
            catch (Exception)
            {
                return ManagerStatus.UNKNOWN;
            }
        }
    }
}
