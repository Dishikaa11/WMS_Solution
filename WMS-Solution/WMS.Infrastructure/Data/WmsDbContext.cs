using Microsoft.EntityFrameworkCore;
using System.Linq;
using WMS.Domain.Entities;

namespace WMS.Infrastructure.Data;

public class WmsDbContext : DbContext
{
    public WmsDbContext(DbContextOptions<WmsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Department> Departments { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<UserLogin> UserLogin { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Leave> Leaves { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<EmployeeProjectAllocation> EmployeeProjectAllocations{ get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserLogin>()
    .ToTable("UserLogin");

        modelBuilder.Entity<UserLogin>()
            .HasOne(u => u.Role)
            .WithMany(r => r.UserLogin)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<UserLogin>()
            .HasOne(u => u.Employee)
            .WithOne(e => e.UserLogin)
            .HasForeignKey<UserLogin>(u => u.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Employee)
            .WithMany(e => e.Attendances)
            .HasForeignKey(a => a.EmpId)
            .OnDelete(DeleteBehavior.Restrict);

       modelBuilder.Entity<Leave>()
            .Property(l => l.Status)
            .HasDefaultValue("Pending");

       modelBuilder.Entity<Leave>()
            .Property(l => l.AppliedOn)
            .HasDefaultValueSql("GETDATE()");
       modelBuilder.Entity<Announcement>()
            .Property(a => a.CreatedOn)
            .HasDefaultValueSql("GETDATE()");

       modelBuilder.Entity<Announcement>()
            .Property(a => a.IsActive)
            .HasDefaultValue(true);
       modelBuilder.Entity<Client>()
            .Property(c => c.Status)
            .HasDefaultValue(true);
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Client)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Project>()
            .Property(p => p.Status)
            .HasDefaultValue("Active");

        modelBuilder.Entity<EmployeeProjectAllocation>()
            .HasKey(x => x.AllocationId);

        modelBuilder.Entity<EmployeeProjectAllocation>()
            .HasOne(x => x.Employee)
            .WithMany(e => e.EmployeeProjectAllocations)
            .HasForeignKey(x => x.EmpId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EmployeeProjectAllocation>()
            .HasOne(x => x.Project)
            .WithMany(p => p.EmployeeProjectAllocations)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EmployeeProjectAllocation>()
            .Property(x => x.Status)
            .HasDefaultValue(true);

        modelBuilder.Entity<AuditLog>()
            .HasKey(x => x.AuditId);

        modelBuilder.Entity<AuditLog>()
            .Property(a => a.CreatedOn)
            .HasDefaultValueSql("GETDATE()");
    }



    public override async Task<int> SaveChangesAsync(
    CancellationToken cancellationToken = default)
    {
        var auditEntries = new List<AuditLog>();

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is AuditLog)
                continue;

            if (entry.State == EntityState.Added ||
                entry.State == EntityState.Modified ||
                entry.State == EntityState.Deleted)
            {
                auditEntries.Add(new AuditLog
                {
                    EntityName = entry.Entity.GetType().Name,
                    RecordId =
                    (
                        int?)
                    entry.Properties
                         .FirstOrDefault(
                             p => p.Metadata.Name.EndsWith("Id"))
                         ?.CurrentValue
                    ?? 0,
                    Action = entry.State.ToString(),
                    CreatedBy = 1, // temporary
                    CreatedOn = DateTime.Now
                });
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        if (auditEntries.Any())
        {
            AuditLogs.AddRange(auditEntries);
            await base.SaveChangesAsync(cancellationToken);
        }

        return result;
    }


}
