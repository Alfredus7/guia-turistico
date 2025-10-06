using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using guia_turistico.Models;

namespace guia_turistico.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<SitioTuristico> SitiosTuristicos { get; set; }
        public DbSet<ImagenSitio> ImagenesSitio { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configurar eliminaciones en cascada
            builder.Entity<Comentario>()
                .HasOne(c => c.SitioTuristico)
                .WithMany(s => s.Comentarios)
                .HasForeignKey(c => c.SitioTuristicoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ImagenSitio>()
                .HasOne(i => i.SitioTuristico)
                .WithMany(s => s.Imagenes)
                .HasForeignKey(i => i.SitioTuristicoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
