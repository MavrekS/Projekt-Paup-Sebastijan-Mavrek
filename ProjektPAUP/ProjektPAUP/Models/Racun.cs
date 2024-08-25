using ProjektPAUP.Areas.Identity.Data;

namespace ProjektPAUP.Models
{
    public class Racun
    {
        public int Id { get; set; }
        
        

        public string ProjektPAUPUserId { get; set; }
        public ProjektPAUPUser Kupac { get; set; }

        public int UkupanIznos { get; set; }

        public int ProizvodId { get; set; }
    }
}
