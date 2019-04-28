﻿using HRS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Data
{
    public class ManagerContext : DbContext
    {
        public static Dictionary<string, string> ConnectionStrings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionStrings["MySQL"]);
            //optionsBuilder.UseSqlServer(ConnectionStrings["MSSQL"]);
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Clinic> Clinics { get; set; }      
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Polyclinic> Polyclinics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HospitalPolyclinic>()
                .HasKey(t => new { t.HospitalId, t.PolyclinicId });

            modelBuilder.Entity<HospitalPolyclinic>()
                .HasOne(pt => pt.Hospital)
                .WithMany(p => p.HospitalPolyclinics)
                .HasForeignKey(pt => pt.HospitalId);

            modelBuilder.Entity<HospitalPolyclinic>()
                .HasOne(pt => pt.Polyclinic)
                .WithMany(t => t.HospitalPolyclinics)
                .HasForeignKey(pt => pt.PolyclinicId);
        }
    }
}
