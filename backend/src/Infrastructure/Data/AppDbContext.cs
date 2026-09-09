using Microsoft.EntityFrameworkCore;
using QH.Domain.Models;

namespace QH.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<House> Houses { get; set; }
    public DbSet<HousePm> HousePms { get; set; }
    public DbSet<QH.Domain.Models.Task> Tasks { get; set; }
    public DbSet<TaskDependency> TaskDependencies { get; set; }
    public DbSet<ClientDecision> ClientDecisions { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Media> Media { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<House>()
            .HasOne(h => h.Client)
            .WithMany()
            .HasForeignKey(h => h.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<House>()
            .HasIndex(h => h.ClientId);

        modelBuilder.Entity<HousePm>()
            .HasKey(hp => new { hp.HouseId, hp.PmId });

        modelBuilder.Entity<HousePm>()
            .HasOne(hp => hp.House)
            .WithMany(h => h.ProjectManagers)
            .HasForeignKey(hp => hp.HouseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HousePm>()
            .HasOne(hp => hp.Pm)
            .WithMany(u => u.ManagedHouses)
            .HasForeignKey(hp => hp.PmId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QH.Domain.Models.Task>()
            .HasOne(t => t.House)
            .WithMany(h => h.Tasks)
            .HasForeignKey(t => t.HouseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QH.Domain.Models.Task>()
            .HasOne(t => t.Craftsman)
            .WithMany(u => u.AssignedTasks)
            .HasForeignKey(t => t.CraftsmanId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<QH.Domain.Models.Task>()
            .HasIndex(t => t.HouseId);

        modelBuilder.Entity<QH.Domain.Models.Task>()
            .HasIndex(t => t.CraftsmanId);

        modelBuilder.Entity<TaskDependency>()
            .HasKey(td => new { td.TaskId, td.DependsOnTaskId });

        modelBuilder.Entity<TaskDependency>()
            .HasOne(td => td.Task)
            .WithMany(t => t.Dependencies)
            .HasForeignKey(td => td.TaskId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TaskDependency>()
            .HasOne(td => td.DependsOnTask)
            .WithMany(t => t.Dependents)
            .HasForeignKey(td => td.DependsOnTaskId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ClientDecision>()
            .HasOne(cd => cd.House)
            .WithMany(h => h.ClientDecisions)
            .HasForeignKey(cd => cd.HouseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ClientDecision>()
            .HasOne(cd => cd.RelatedTask)
            .WithMany(t => t.ClientDecisions)
            .HasForeignKey(cd => cd.RelatedTaskId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<ClientDecision>()
            .Property(cd => cd.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Item>()
            .HasOne(i => i.House)
            .WithMany(h => h.Items)
            .HasForeignKey(i => i.HouseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Item>()
            .HasOne(i => i.ParentItem)
            .WithMany(i => i.SubItems)
            .HasForeignKey(i => i.ParentItemId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Item>()
            .HasIndex(i => i.HouseId);

        modelBuilder.Entity<Item>()
            .HasIndex(i => i.ParentItemId);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.House)
            .WithMany(h => h.Comments)
            .HasForeignKey(c => c.HouseId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Task)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TaskId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Item)
            .WithMany(i => i.Comments)
            .HasForeignKey(c => c.ItemId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.TaskId);

        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.ItemId);

        modelBuilder.Entity<Media>()
            .HasOne(m => m.House)
            .WithMany(h => h.Media)
            .HasForeignKey(m => m.HouseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Media>()
            .HasOne(m => m.UploadedByUser)
            .WithMany(u => u.UploadedMedia)
            .HasForeignKey(m => m.UploadedByUserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Media>()
            .HasOne(m => m.Task)
            .WithMany(t => t.Media)
            .HasForeignKey(m => m.TaskId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Media>()
            .HasOne(m => m.Item)
            .WithMany(i => i.Media)
            .HasForeignKey(m => m.ItemId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Notification>()
            .HasIndex(n => new { n.UserId, n.IsRead })
            .HasFilter("[IsRead] = 0");

        modelBuilder.Entity<AuditLog>()
            .HasOne(a => a.User)
            .WithMany(u => u.AuditLogs)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
