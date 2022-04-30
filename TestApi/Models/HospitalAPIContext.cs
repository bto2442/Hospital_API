using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TestApi.Models;

namespace TestApi.Models
{
    public class HospitalAPIContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public HospitalAPIContext(DbContextOptions<HospitalAPIContext> options, IConfiguration configuration) : base(options)
        {
            Configuration=configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("hospital");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Staff> Staffs { get; set; } = null!;
        public DbSet<Prescription> Prescriptions { get; set; } = null!;
        public DbSet<Diagnosis> Diagnosis { get; set; } = null!;
    }
}
