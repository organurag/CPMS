using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CPMS.API.Models
{
    public class ProjectContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure many-to-many relationship
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Employees)
                .WithMany(e => e.Projects)
                .Map(m =>
                {
                    m.ToTable("ProjectEmployees");
                    m.MapLeftKey("ProjectId");
                    m.MapRightKey("EmployeeId");
                });
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}