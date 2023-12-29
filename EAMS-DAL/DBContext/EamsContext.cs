
using EAMS_ACore;
using EAMS_ACore.AuthModels;
using EAMS_ACore.Models;
using EAMS_ACore.NotificationModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace EAMS_DAL.DBContext;

public partial class EamsContext : IdentityDbContext<UserRegistration>
{
    public EamsContext()
    {
    }

    public EamsContext(DbContextOptions<EamsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserState> UserState { get; set; }
    public virtual DbSet<UserDistrict> UserDistrict { get; set; }
    public virtual DbSet<UserPCConstituency> UserPCConstituency { get; set; }
    public virtual DbSet<UserAssembly> UserAssembly { get; set; }

    public virtual DbSet<AssemblyMaster> AssemblyMaster { get; set; }
    public virtual DbSet<BoothMaster> BoothMaster { get; set; }
    public virtual DbSet<EventMaster> EventMaster { get; set; }
    public virtual DbSet<DistrictMaster> DistrictMaster { get; set; }
    public virtual DbSet<ParliamentConstituencyMaster> ParliamentConstituencyMaster { get; set; }
    public virtual DbSet<StateMaster> StateMaster { get; set; }
    public virtual DbSet<SectorOfficerMaster> SectorOfficerMaster { get; set; }
    public virtual DbSet<ElectionInfoMaster> ElectionInfoMaster { get; set; }
    public virtual DbSet<SlotManagementMaster> SlotManagementMaster { get; set; }
    public virtual DbSet<PollDetail> PollDetails { get; set; }
    public virtual DbSet<PollInterruption> PollInterruptions { get; set; }
    public virtual DbSet<PollInterruptionHistory> PollInterruptionHistory { get; set; }
    public virtual DbSet<Notification> Notification { get; set; }

    public virtual DbSet<SMSTemplate> SMSTemplate { get; set; }
    public virtual DbSet<SMSSent> SMSSent { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        base.OnModelCreating(modelBuilder);


        // Add your custom model configurations here
    }
}