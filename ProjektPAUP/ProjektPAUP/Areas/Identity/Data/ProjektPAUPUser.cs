using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProjektPAUP.Models;

namespace ProjektPAUP.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ProjektPAUPUser class
public class ProjektPAUPUser : IdentityUser
{
    public string Ime { get; set; }
    public string Prezime { get; set; }

    public int StanjeNaRacunu { get; set; } = 500;

    public List<Racun> Racuni { get; set; }

}

