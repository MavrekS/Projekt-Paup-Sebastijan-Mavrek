using System.ComponentModel.DataAnnotations;

namespace ProjektPAUP.Models
{
    public class CreateSkladisteModel
    {
        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [StringLength(40, ErrorMessage = "Mora biti duže od 1 slova.")]
        public string Ime { get; set; }

        public int Id { get; set; }
    }
}
