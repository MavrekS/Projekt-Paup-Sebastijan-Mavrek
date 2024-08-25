namespace ProjektPAUP.Models
{
    public class SkladisteProizvod
    {
        public int Id { get; set; }
        public int SkladisteId { get; set; }
        public Skladiste Skladiste { get; set; }

        public int ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; } 

        public bool Status { get; set; }
    }
}
