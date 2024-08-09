using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using tp_lab_4.Models;

namespace tp_lab_4.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Afiliado> afiliados { get; set; }
        public DbSet<Ticket> tickets { get; set; }
        public DbSet<TicketDetalle> TicketDetalles { get; set; }
        public DbSet<Estado> estados { get; set; }
    }
}
