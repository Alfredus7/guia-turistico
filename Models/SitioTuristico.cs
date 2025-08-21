using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace guia_turistico.Models
{
    

    public class SitioTuristico
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; } // Dirección o coordenadas
        public ICollection<ImagenSitio> Imagenes { get; set; } // Carrusel Bootstrap
        public ICollection<Comentario> Comentarios { get; set; }
        public double PuntuacionPromedio { get; set; }
    }
}
