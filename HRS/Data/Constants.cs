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
                Icon = "fa fa-h-square",
                Action = "Hospitals",
                BackgroundClass = "bg-aqua" },
            new ManagementDashboardPagesVM{
                Header = "Kullanıcılar",
                Description = "Kullanıcıları düzenle",
                Icon = "fa fa-user-md",
                Action = "Users",
                BackgroundClass = "bg-green" },
            new ManagementDashboardPagesVM{
                Header = "Klinikler",
                Description = "Klinikleri düzenle",
                Icon = "fa fa-hospital-o",
                Action = "Clinics",
                BackgroundClass = "bg-yellow" },
            new ManagementDashboardPagesVM{
                Header = "Poliklinikler",
                Description = "Poliklinikleri düzenle",
                Icon = "fa fa-stethoscope",
                Action = "Polyclinics",
                BackgroundClass = "bg-orange" },
            new ManagementDashboardPagesVM{
                Header = "İller",
                Description = "İlleri düzenle",
                Icon = "fa fa-map-o",
                Action = "Cities",
                BackgroundClass = "bg-light-blue" },
            new ManagementDashboardPagesVM{
                Header = "İlçeler",
                Description = "İlçeleri düzenle",
                Icon = "fa fa-map-marker",
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
