using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace guia_turistico.Models
{
    // 🌿 Tipo o categoría de sitio (sirve también como sugerencia de tour)
    public class Tipo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID Tipo")]
        public int TipoId { get; set; }

        [Required(ErrorMessage = "El nombre del tipo es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Nombre del Tipo o Tour")]
        public string Nombre { get; set; }

        [StringLength(300)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        // 🖼️ Imagen representativa del tipo (ej. /images/tipos/aventura.jpg)
        [StringLength(250)]
        [Display(Name = "Imagen del Tipo o Tour")]
        public string ImagenUrl { get; set; }

        // 🔗 Relación con sitios turísticos
        public virtual ICollection<SitioTuristico> Sitios { get; set; } = new List<SitioTuristico>();
    }
}
