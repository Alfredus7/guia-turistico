using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace guia_turistico.Models
{


    // 🖼️ Imagen del sitio turístico
    public class ImagenSitio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID Imagen")]
        public int Id { get; set; }

        [Required(ErrorMessage = "La URL de la imagen es obligatoria")]
        [StringLength(3000)]
        [Display(Name = "URL de Imagen")]
        public string Url { get; set; }

        [Required]
        [Display(Name = "Sitio Turístico")]
        public int SitioTuristicoId { get; set; }

        [ForeignKey("SitioTuristicoId")]
        public virtual SitioTuristico SitioTuristico { get; set; }
    }
}
