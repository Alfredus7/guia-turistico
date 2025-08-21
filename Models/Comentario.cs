using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace guia_turistico.Models
{
   

    public class Comentario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID Comentario")]
        public int ComentarioId { get; set; }

        [Required(ErrorMessage = "El texto del comentario es obligatorio")]
        [StringLength(500, ErrorMessage = "El comentario no puede exceder 500 caracteres")]
        [Display(Name = "Comentario")]
        public string Texto { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "La puntuación debe estar entre 1 y 5")]
        [Display(Name = "Puntuación")]
        public int Puntuacion { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        // Relación con IdentityUser
        [Required]
        [Display(Name = "Usuario")]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual IdentityUser Usuario { get; set; }

        // Relación con SitioTuristico
        [Required]
        [Display(Name = "Sitio Turístico")]
        public int SitioTuristicoId { get; set; }

        [ForeignKey("SitioTuristicoId")]
        public virtual SitioTuristico SitioTuristico { get; set; }

        // Propiedad no mapeada para mostrar nombre de usuario
        [NotMapped]
        [Display(Name = "Nombre Usuario")]
        public string NombreUsuario => Usuario?.UserName;
    }
}
