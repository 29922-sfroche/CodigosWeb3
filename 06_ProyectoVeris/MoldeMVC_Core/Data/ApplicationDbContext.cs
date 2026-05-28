using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Models;

namespace MoldeMVC_Core.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<MoldeMVC_Core.Models.Especialidades> Especialidades { get; set; } = default!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MoldeMVC_Core.Models.Pacientes> Pacientes { get; set; } = default!;

        public DbSet<MoldeMVC_Core.Models.Medicos> Medicos { get; set; } = default!;

    }
}
