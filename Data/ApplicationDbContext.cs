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

        // DbSets de la aplicación
        public DbSet<SitioTuristico> SitiosTuristicos { get; set; }
        public DbSet<ImagenSitio> ImagenesSitio { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración opcional: relaciones, restricciones, indices
            modelBuilder.Entity<SitioTuristico>()
                .HasMany(s => s.Imagenes)
                .WithOne(i => i.SitioTuristico)
                .HasForeignKey(i => i.SitioTuristicoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SitioTuristico>()
                .HasMany(s => s.Comentarios)
                .WithOne(c => c.SitioTuristico)
                .HasForeignKey(c => c.SitioTuristicoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
