using MEDAPP.Models;
using MEDAPP.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEDAPP.WebAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<AppointmentCategory> AppointmentCategory { get; set; }
        public virtual DbSet<Appointment> Appointment { get; set; }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Role> Role { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Security Entities

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.UserId });
                
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(re => re.UserRole);

            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasMany(re => re.UserRole);

            });

            #endregion

            #region Seed
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "ADMIN", Description="ADMIN" },
                new Role { RoleId = 2, Name = "USER" , Description="ADMIN" }
                );


            modelBuilder.Entity<AppointmentCategory>().HasData(
               new AppointmentCategory { Id = 1, Name = "General", Description = "General" },
               new AppointmentCategory { Id = 2, Name = "Dentistry", Description = "Dentistry" },
               new AppointmentCategory { Id = 3, Name = "Pediatrics", Description = "Pediatrics" },
               new AppointmentCategory { Id = 4, Name = "Neurology", Description = "Neurology" }
               );
            

            #endregion

            modelBuilder.Entity<Appointment>(entity => {

                entity.HasKey(e => e.Id );

                entity.Property(e => e.PatientId).IsRequired();
                entity.Property(e => e.AppointmentCategoryId).IsRequired();
                //entity.Property(e => e.Id).IsRequired().IsConcurrencyToken();

                entity
                .HasOne(ac => ac.AppointmentCategory);

            });

            modelBuilder.Entity<AppointmentCategory>(entity => {

                entity.HasKey(e => e.Id);

                entity
                 .HasMany(a => a.Appointment).WithOne(ac => ac.AppointmentCategory).HasForeignKey(ac => ac.AppointmentCategoryId);

            });


            modelBuilder.Entity<Patient>(entity => {

                entity.HasKey(e => e.Id);
                
                entity
                 .HasMany(a => a.Appointment).WithOne(p => p.Patient).HasForeignKey(p => p.PatientId);

            });


        }



    }
}
