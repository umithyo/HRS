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
            context.Add(hospital);
            context.SaveChanges();
            return ManagerStatus.OK;
        }
    }
}
