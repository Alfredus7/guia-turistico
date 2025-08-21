using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace guia_turistico.Models
{
    

    public class ImagenSitio
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; } // Ruta de la imagen
        public int SitioTuristicoId { get; set; }
        [ForeignKey("SitioTuristicoId")]
        public SitioTuristico SitioTuristico { get; set; }
    }
}
