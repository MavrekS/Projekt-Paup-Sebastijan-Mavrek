using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektPAUP.Areas.Identity.Data;
using ProjektPAUP.Data;
using ProjektPAUP.Models;
using System.Diagnostics;

namespace ProjektPAUP.Controllers
{

    public class HomeController : Controller
    {
        private readonly UserManager<ProjektPAUPUser> _userManager;
        private readonly ProjektPAUPContext _Context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ProjektPAUPContext Context, UserManager<ProjektPAUPUser> userManager)
        {
            _logger = logger;
            _Context = Context;
            _userManager = userManager;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<Proizvod> Proizvodi = _Context.Proizvodi.Where(x => x.Status == true).ToList();
            if (User.Identity.IsAuthenticated == true)
            {
                ProjektPAUPUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                       TempData["Novac"] = user.StanjeNaRacunu;
                }
            }
           
            if (Proizvodi != null)
            {
                TempData["Proizvodi"] = Proizvodi;
            }
            return View();
        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Kupi(int ProizvodId)
        {
            SkladisteProizvod SkladisteProizvod = _Context.SkladistaProizvodi.FirstOrDefault(x => x.Status == true && x.ProizvodId == ProizvodId);
            if (SkladisteProizvod == null)
            {
                TempData["NemaZalihe"] = "Kupnja nije moguca nema artikla na zalihi";
                return RedirectToAction("Index");
            }
            Proizvod Proizvod = _Context.Proizvodi.FirstOrDefault(x => x.Status == true && x.Id == ProizvodId);
            ProjektPAUPUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user.StanjeNaRacunu >= Proizvod.Cijena)
            {
                user.StanjeNaRacunu = user.StanjeNaRacunu - Proizvod.Cijena;
                _Context.Entry(user).State = EntityState.Modified;
                _Context.SaveChanges();
                SkladisteProizvod.Status = false;
                _Context.Entry(SkladisteProizvod).State = EntityState.Modified;
                _Context.SaveChanges();
                Racun Racun = new Racun();
                Racun.ProjektPAUPUserId = user.Id;
                Racun.UkupanIznos = Proizvod.Cijena;
                Racun.ProizvodId = Proizvod.Id;
                _Context.Racuni.Add(Racun);
                _Context.SaveChanges();
                TempData["Kupnja"] = "Transakcija uspješna.";
            }
            else
            {
                TempData["Error"] = "Nema dovoljno sredstva na računu.";
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> DodajNovce()
        {
            
            ProjektPAUPUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            
                user.StanjeNaRacunu = user.StanjeNaRacunu + 100;
                _Context.Entry(user).State = EntityState.Modified;
                _Context.SaveChanges();
            TempData["DodanNovac"] = "Dodano je 100 eura na račun!";

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PregledRacuna()
        {
            ProjektPAUPUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<Racun> Racuni = _Context.Racuni.Where(x => x.ProjektPAUPUserId == user.Id).ToList();
            if (Racuni != null)
            {
                TempData["Racuni"] = Racuni; 
            }
            return View();
        
        }
    }
}
