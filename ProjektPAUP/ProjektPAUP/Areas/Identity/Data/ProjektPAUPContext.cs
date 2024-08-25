using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektPAUP.Areas.Identity.Data;
using ProjektPAUP.Models;

namespace ProjektPAUP.Data;

public class ProjektPAUPContext : IdentityDbContext<ProjektPAUPUser>
{
    public DbSet<Proizvod> Proizvodi { get; set; }
    public DbSet<Racun> Racuni { get; set; }
    public DbSet<Skladiste> Skladista { get; set; }

    public DbSet<SkladisteProizvod> SkladistaProizvodi { get; set; } 



    public ProjektPAUPContext(DbContextOptions<ProjektPAUPContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
