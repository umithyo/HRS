using HRS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Data
{
    public static class Constants
    {
        public static List<ManagementDashboardPagesVM> ManagementPages = new List<ManagementDashboardPagesVM>()
        {
            new ManagementDashboardPagesVM{
                Header = "Hastaneler",
                Description = "Hastaneleri düzenle",
                Icon = "ion ion-home",
                Action = "Hospitals",
                BackgroundClass = "bg-aqua" },
            new ManagementDashboardPagesVM{
                Header = "Kullanıcılar",
                Description = "Kullanıcıları düzenle",
                Icon = "ion ion-person-add",
                Action = "Users",
                BackgroundClass = "bg-green" },
            new ManagementDashboardPagesVM{
                Header = "Klinikler",
                Description = "Klinikleri düzenle",
                Icon = "ion ion-medkit",
                Action = "Clinics",
                BackgroundClass = "bg-yellow" },
            new ManagementDashboardPagesVM{
                Header = "Poliklinikler",
                Description = "Poliklinikleri düzenle",
                Icon = "ion ion-medkit",
                Action = "Polyclinics",
                BackgroundClass = "bg-orange" },
            new ManagementDashboardPagesVM{
                Header = "İller",
                Description = "İlleri düzenle",
                Icon = "ion ion-outlet",
                Action = "Cities",
                BackgroundClass = "bg-light-blue" },
            new ManagementDashboardPagesVM{
                Header = "İlçeler",
                Description = "İlçeleri düzenle",
                Icon = "ion ion-outlet",
                Action = "Towns",
                BackgroundClass = "bg-lime" },
        };
    }

    public enum ManagerStatus
    {
        OK,
        UNKNOWN,
        USER_NOT_FOUND,
        USER_TC_EXISTS,
        USER_EMAIL_EXISTS,
        USER_PHONE_EXISTS,
        USER_WRONG_CREDENTIALS
    };
}
