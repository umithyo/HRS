using HRS.Helpers;
using HRS.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Data
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            ManagerContext context = app.ApplicationServices.GetRequiredService<ManagerContext>();
            IUserManager userManager = app.ApplicationServices.GetRequiredService<IUserManager>();
            if (!context.Users.Any())
            {
                userManager.Register(
                    new User {
                        TCKimlikNo = "11111111111",
                        CreatedAt = DateTime.Now,
                        Role = "Yönetici",
                        Password = "123"
                    },
                    new UserInfo {
                        CreatedAt = DateTime.Now,
                        Name = "HRS",
                        Surname = "Yönetim",
                        Phone = "5555555555",
                        Email = "topkek@cdn.com",
                    });
            }
        }
    }
}
