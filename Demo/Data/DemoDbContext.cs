using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Demo.Models;
using System.Drawing;
using Demo.Entities;

namespace Demo.Data
{
    public class DemoDbContext:IdentityDbContext<ApplicationUser>
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options):base(options)
        {

        }
        public DbSet<Company> Companies{ get; set; }
        public DbSet<Employee> employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Employee>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
