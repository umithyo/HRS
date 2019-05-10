﻿// <auto-generated />
using System;
using HRS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HRS.Migrations
{
    [DbContext(typeof(ManagerContext))]
    partial class ManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HRS.Models.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClinicId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMP");

                    b.Property<Guid>("DoctorId");

                    b.Property<int>("HospitalId");

                    b.Property<Guid>("PatientId");

                    b.Property<int>("PolyclinicId");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("HospitalId");

                    b.HasIndex("PatientId");

                    b.HasIndex("PolyclinicId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("HRS.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("HRS.Models.Clinic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("HRS.Models.Hospital", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TownId");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("TownId");

                    b.ToTable("Hospitals");
                });

            modelBuilder.Entity("HRS.Models.HospitalClinic", b =>
                {
                    b.Property<int>("HospitalId");

                    b.Property<int>("ClinicId");

                    b.HasKey("HospitalId", "ClinicId");

                    b.HasIndex("ClinicId");

                    b.ToTable("HospitalClinic");
                });

            modelBuilder.Entity("HRS.Models.Polyclinic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClinicId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMP");

                    b.Property<int>("HospitalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.HasIndex("HospitalId");

                    b.ToTable("Polyclinics");
                });

            modelBuilder.Entity("HRS.Models.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CityId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("HRS.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Role");

                    b.Property<string>("TCKimlikNo")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<Guid?>("UserInfoId");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HRS.Models.UserInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClinicId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int?>("HospitalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.HasIndex("HospitalId");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("HRS.Models.Appointment", b =>
                {
                    b.HasOne("HRS.Models.Clinic", "Clinic")
                        .WithMany()
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HRS.Models.User", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HRS.Models.Hospital", "Hospital")
                        .WithMany()
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HRS.Models.User", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HRS.Models.Polyclinic", "Polyclinic")
                        .WithMany()
                        .HasForeignKey("PolyclinicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRS.Models.Hospital", b =>
                {
                    b.HasOne("HRS.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HRS.Models.Town", "Town")
                        .WithMany()
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRS.Models.HospitalClinic", b =>
                {
                    b.HasOne("HRS.Models.Clinic", "Clinic")
                        .WithMany("HospitalClinics")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HRS.Models.Hospital", "Hospital")
                        .WithMany("HospitalClinics")
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRS.Models.Polyclinic", b =>
                {
                    b.HasOne("HRS.Models.Clinic", "Clinic")
                        .WithMany()
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HRS.Models.Hospital", "Hospital")
                        .WithMany()
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRS.Models.Town", b =>
                {
                    b.HasOne("HRS.Models.City", "City")
                        .WithMany("Towns")
                        .HasForeignKey("CityId");
                });

            modelBuilder.Entity("HRS.Models.User", b =>
                {
                    b.HasOne("HRS.Models.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId");
                });

            modelBuilder.Entity("HRS.Models.UserInfo", b =>
                {
                    b.HasOne("HRS.Models.Clinic", "Clinic")
                        .WithMany()
                        .HasForeignKey("ClinicId");

                    b.HasOne("HRS.Models.Hospital", "Hospital")
                        .WithMany()
                        .HasForeignKey("HospitalId");
                });
#pragma warning restore 612, 618
        }
    }
}
