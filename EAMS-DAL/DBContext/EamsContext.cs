
using EAMS_ACore;
using EAMS_ACore.AuthModels;
using EAMS_ACore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace EAMS_DAL.DBContext;

public partial class EamsContext :IdentityDbContext<UserRegistration>
{
    public EamsContext()
    {
    }

    public EamsContext(DbContextOptions<EamsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssemblyMaster> AssemblyMaster { get; set; }

    public virtual DbSet<BoothMaster> BoothMaster { get; set; }
    public virtual DbSet<EventMaster> EventMaster { get; set; }


    public virtual DbSet<DistrictMaster> DistrictMaster { get; set; }
    public virtual DbSet<ParliamentConstituencyMaster> ParliamentConstituencyMaster { get; set; }

    public virtual DbSet<StateMaster> StateMaster { get; set; }
    public virtual DbSet<SectorOfficerMaster> SectorOfficerMaster { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<StateMaster>().Property(p => p.StateName).ValueGeneratedOnAdd();
        base.OnModelCreating(modelBuilder);


        // Add your custom model configurations here
    }
}