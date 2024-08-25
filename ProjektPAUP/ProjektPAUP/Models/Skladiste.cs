namespace ProjektPAUP.Models
{
    public class Skladiste
    {
        public int Id { get; set; }

        public List<SkladisteProizvod> SkladisteProizvod { get; set; }
        public string Ime { get; set; }
        public bool Status { get; set; }
    }
}
