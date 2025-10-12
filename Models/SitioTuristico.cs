using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace guia_turistico.Models
{


    public class SitioTuristico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID Sitio Turístico")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(200)]
        [Display(Name = "Nombre del Sitio")]
        public string? Nombre { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre (Inglés)")]
        public string? NombreIngles { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre (Portugués)")]
        public string? NombrePortugues { get; set; }

        [StringLength(1000)]
        [Display(Name = "Descripción (Español)")]
        public string? Descripcion { get; set; }

        [StringLength(1000)]
        [Display(Name = "Descripción (Inglés)")]
        public string? DescripcionIngles { get; set; }

        [StringLength(1000)]
        [Display(Name = "Descripción (Portugués)")]
        public string? DescripcionPortugues { get; set; }

        [Display(Name = "Dirección o Referencia")]
        [StringLength(250)]
        public string? Direccion { get; set; }

        // 🌍 Coordenadas para mapa
        [Required]
        [Display(Name = "Latitud")]
        public double Latitud { get; set; }

        [Required]
        [Display(Name = "Longitud")]
        public double Longitud { get; set; }

        // Tipo o categoría (tour)
        [Required]
        [Display(Name = "Tipo / Tour")]
        public int TipoId { get; set; }

        [ForeignKey("TipoId")]
        public virtual Tipo? Tipo { get; set; }

        // Galería de imágenes
        public virtual ICollection<ImagenSitio> Imagenes { get; set; } = new List<ImagenSitio>();

        // Comentarios de usuarios
        public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

        // ⭐ Puntuación promedio (calculada)
        [NotMapped]
        [Display(Name = "Puntuación Promedio")]
        public double PuntuacionPromedio =>
            Comentarios != null && Comentarios.Count > 0
                ? Math.Round(Comentarios.Average(c => c.Puntuacion), 1)
                : 0;
    }
}
