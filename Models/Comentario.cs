﻿using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace guia_turistico.Models
{


    // 💬 Comentario de usuario (IdentityUser)
    public class Comentario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID Comentario")]
        public int ComentarioId { get; set; }

        [Required(ErrorMessage = "Debe escribir un comentario")]
        [StringLength(500)]
        [Display(Name = "Comentario")]
        public string Texto { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "La puntuación debe estar entre 1 y 5")]
        [Display(Name = "Puntuación")]
        public int Puntuacion { get; set; }

        [Display(Name = "Fecha de publicación")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        // Usuario Identity
        [Required]
        [Display(Name = "Usuario")]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual IdentityUser Usuario { get; set; }

        // Sitio turístico
        [Required]
        [Display(Name = "Sitio Turístico")]
        public int SitioTuristicoId { get; set; }

        [ForeignKey("SitioTuristicoId")]
        public virtual SitioTuristico SitioTuristico { get; set; }

        // Campo no mapeado
        [NotMapped]
        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario => Usuario?.UserName;
    }
}
