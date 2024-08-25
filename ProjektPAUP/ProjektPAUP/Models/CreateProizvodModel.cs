using System.ComponentModel.DataAnnotations;

namespace ProjektPAUP.Models
{
    public class CreateProizvodModel
    {
        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [StringLength(40, ErrorMessage = "Mora biti duže od 1 slova.")]
        public string Naziv { get; set; }

        [Range(1,120)]
        public int Garancija { get; set; }

        [Range(1,9999)]
        public int Cijena { get; set; }

        [Range(1,6)]
        public int Povrat { get; set; }       
        
        public int Id { get; set; }
    }
}
