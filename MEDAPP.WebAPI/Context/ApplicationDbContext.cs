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

                //entity.Property(e => e.Id).IsRequired().IsConcurrencyToken();

                modelBuilder.Entity<Appointment>()
                .HasOne(ac => ac.AppointmentCategory)
                .WithOne(a => a.Appointment)
                .HasForeignKey<Appointment>(a => a.AppointmentCategoryId);

            });
        }

         
        
    }
}
