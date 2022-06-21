using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FertilityPoint.Data.Modules
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<SubCounty> SubCounties { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
    }



}
