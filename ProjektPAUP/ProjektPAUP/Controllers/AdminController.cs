using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektPAUP.Data;
using ProjektPAUP.Models;

namespace ProjektPAUP.Controllers
{
    [Authorize(Roles = "Admininistator")]
    public class AdminController : Controller
    {
        private readonly ProjektPAUPContext _Context;

        public AdminController(ProjektPAUPContext Context) { _Context = Context; }
        public IActionResult IndexSkladiste()
        {

            List<Skladiste> Skladista = new List<Skladiste>();
            Skladista= _Context.Skladista.Where(x => x.Status == true).ToList();
            return View(Skladista);
        }
        public IActionResult IndexProizvod()
        {
            List<Proizvod> Proizvodi = new List<Proizvod>();
            Proizvodi = _Context.Proizvodi.Where(x => x.Status == true).ToList();
            


            return View(Proizvodi);
        }
        public IActionResult IndexInventura()
        {
            List<SkladisteProizvod> SkladisteProizvodi = new List<SkladisteProizvod>();
            SkladisteProizvodi = _Context.SkladistaProizvodi.Include(x => x.Proizvod).Include(x => x.Skladiste).Where(x => x.Status == true).ToList();

            return View(SkladisteProizvodi);
        }
        [HttpGet]
        public IActionResult CreateSkladiste()
        {


            return View();
        }
        [HttpPost]
        public IActionResult CreateSkladiste(CreateSkladisteModel Model)
        {
            Skladiste DalPostoji = _Context.Skladista.FirstOrDefault(x => x.Ime.ToUpper() == Model.Ime.ToUpper() && x.Status == true);
            if (DalPostoji != null)
            {
                TempData["Error"] = "Skladiste več postoji!";
                return RedirectToAction("CreateSkladiste");
            }
            Skladiste Skladiste = new Skladiste();
            Skladiste.Ime = Model.Ime;
            Skladiste.Status = true;
            _Context.Skladista.Add(Skladiste);
            _Context.SaveChanges();
            return View();
        }
        [HttpGet]
        public IActionResult CreateProizvod()
        {


            return View();
        }

        [HttpPost]
        public IActionResult CreateProizvod(CreateProizvodModel Model)
        {
            Proizvod DalPostoji = _Context.Proizvodi.FirstOrDefault(x => x.Naziv.ToUpper() == Model.Naziv.ToUpper() && x.Status == true);
            if (DalPostoji != null)
            {

                TempData["Error"] = "Proizvod več postoji!";
                return RedirectToAction("CreateProizvod");
            }
            Proizvod Proizvod = new Proizvod();
            
                Proizvod.Naziv = Model.Naziv;
                Proizvod.Garancija = Model.Garancija;
                Proizvod.Povrat = Model.Povrat;
                Proizvod.Cijena = Model.Cijena;
            
            Proizvod.Status = true;
            _Context.Proizvodi.Add(Proizvod);
            _Context.SaveChanges();
            return View();


            
        }


        [HttpGet]
        public IActionResult CreateInventura()
        {
            
            List<Proizvod> Proizvodi = new List<Proizvod>();
            Proizvodi = _Context.Proizvodi.Where(x => x.Status == true).ToList();
            List<Skladiste> Skladista = new List<Skladiste>();
            Skladista = _Context.Skladista.Where(x => x.Status == true).ToList();
            CreateInventuraModel createInventuraModel = new CreateInventuraModel();
            if (Proizvodi != null)
            {
                TempData["Proizvodi"] = Proizvodi;
            }

            if (Skladista != null)
            {
                TempData["Skladista"] = Skladista;
            }

            return View();
        }

        [HttpPost]
        
        public IActionResult CreateInventura(CreateInventuraModel Model)
        {
            
            SkladisteProizvod skladisteProizvod = new SkladisteProizvod();
            skladisteProizvod.SkladisteId = Model.SkladisteId;
            skladisteProizvod.ProizvodId = Model.ProizvodId;
            skladisteProizvod.Status = true;
            _Context.SkladistaProizvodi.Add(skladisteProizvod);
            _Context.SaveChanges();
            return RedirectToAction("CreateInventura");
            
        }

        [HttpGet]
        public IActionResult DeleteProizvod()
        {
            List<Proizvod> Proizvodi = new List<Proizvod>();
            Proizvodi = _Context.Proizvodi.Where(x => x.Status == true).ToList();

            if (Proizvodi != null)
            {
                TempData["Proizvodi"] = Proizvodi;
            }

            return View();
        }

        [HttpPost]
        public IActionResult DeleteProizvod(CreateProizvodModel Model)
        {
            Proizvod Proizvod = _Context.Proizvodi.FirstOrDefault(x => x.Status == true && x.Id == Model.Id);
            Proizvod.Status = false;
            _Context.Entry(Proizvod).State = EntityState.Modified;
            _Context.SaveChanges();

            return RedirectToAction("DeleteProizvod");
        }

        [HttpGet]
        public IActionResult DeleteSkladiste()
        {
            List<Skladiste> Skladista = new List<Skladiste>();
            Skladista = _Context.Skladista.Where(x => x.Status == true).ToList();

            if (Skladista != null)
            {
                TempData["Skladista"] = Skladista;
            }

            return View();
        }

        [HttpPost]
        public IActionResult DeleteSkladiste(CreateSkladisteModel Model)
        {

            List <SkladisteProizvod> SkladistaProizvodi = _Context.SkladistaProizvodi.Where(x => x.SkladisteId == Model.Id).ToList();
            foreach (var item in SkladistaProizvodi)
            {
                item.Status = false;
                _Context.Entry(item).State = EntityState.Modified;
                _Context.SaveChanges();
            }
            
            Skladiste Skladiste = _Context.Skladista.FirstOrDefault(x => x.Status == true && x.Id == Model.Id);
            Skladiste.Status = false;
            _Context.Entry(Skladiste).State = EntityState.Modified;
            _Context.SaveChanges();

            return RedirectToAction("DeleteSkladiste");
        }

        [HttpGet]
        public IActionResult DeleteInventura()
        {
            List<SkladisteProizvod> SkladistaProizvodi = new List<SkladisteProizvod>();
            SkladistaProizvodi = _Context.SkladistaProizvodi.Include(x => x.Proizvod).Where(x => x.Status == true).ToList();

            if (SkladistaProizvodi != null)
            {
                TempData["SkladistaProizvodi"] = SkladistaProizvodi;
            }

            return View();
        }

        [HttpPost]
        public IActionResult DeleteInventura(CreateInventuraModel Model)
        {

            SkladisteProizvod SkladistaProizvodi = _Context.SkladistaProizvodi.FirstOrDefault(x => x.Id == Model.Id);
            
                SkladistaProizvodi.Status = false;
                _Context.Entry(SkladistaProizvodi).State = EntityState.Modified;
                _Context.SaveChanges();
                      
            return RedirectToAction("DeleteInventura");
        }


        [HttpGet]
        public IActionResult ModifyProizvod(int ProizvodId)
        {
            Proizvod Proizvod = new Proizvod();
            Proizvod = _Context.Proizvodi.FirstOrDefault(x => x.Status == true && x.Id == ProizvodId);

            CreateProizvodModel Model = new CreateProizvodModel();
            Model.Naziv = Proizvod.Naziv;
            Model.Id = Proizvod.Id;
            Model.Cijena = Proizvod.Cijena;
            Model.Garancija = Proizvod.Garancija;
            Model.Povrat = Proizvod.Povrat;

            return View(Model);
        }

        [HttpPost]
        public IActionResult ModifyProizvod(CreateProizvodModel Model)
        {

            Proizvod Proizvod = _Context.Proizvodi.FirstOrDefault(x => x.Id == Model.Id);
            Proizvod DalPostoji = _Context.Proizvodi.FirstOrDefault(x => x.Status == true && x.Naziv.ToUpper() == Model.Naziv.ToUpper());

            if (DalPostoji != null && Proizvod.Naziv != Model.Naziv)
            {
                
                TempData["Error"] = "Proizvod več postoji!";
                return RedirectToAction("ModifyProizvod",new {ProizvodId = Proizvod.Id });
            }
            Proizvod.Naziv = Model.Naziv;
            Proizvod.Garancija = Model.Garancija;
            Proizvod.Povrat = Model.Povrat;
            Proizvod.Cijena = Model.Cijena;


            _Context.Entry(Proizvod).State = EntityState.Modified;
            _Context.SaveChanges();

            return RedirectToAction("ModifyProizvod", new { ProizvodId = Proizvod.Id });
        }
    }
}
