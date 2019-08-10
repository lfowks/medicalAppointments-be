using MEDAPP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEDAPP.WebAPI.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Patient> Patient { get; set; }

        public virtual DbSet<AppointmentCategory> AppointmentCategory { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
