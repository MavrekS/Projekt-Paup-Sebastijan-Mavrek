namespace ProjektPAUP.Models
{
    public class Proizvod
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = "";
        public int Garancija { get; set; }
        public int Povrat  { get; set; }
        public bool Status { get; set; }

        public int Cijena { get; set; }

        
        public List<SkladisteProizvod> SkladisteProizvod { get; set; }




        
    }
}
