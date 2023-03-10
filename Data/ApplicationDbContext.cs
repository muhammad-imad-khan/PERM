using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PERM.Models;

namespace PERM.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeMasterData> employeeMasterData {get; set;}
        public DbSet<Department> department { get; set;}
        public DbSet<Attendance> attendance { get; set;}
        public DbSet<_Tasks> _tasks { get; set;}
    }
}