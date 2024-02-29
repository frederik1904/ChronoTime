using System.Diagnostics;
using Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using Repository.Models;
using Repository.Models.BaseEntities;

namespace Repository;

public class ChronoContext : DbContext
{
    private readonly AppSettings _appSettings;

    public ChronoContext(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            _appSettings.ConnectionString.ExposeSecret(), 
            b => b.MigrationsAssembly("DatabaseMigrationHandler")
            );
    }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
    
    private void PreSave()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is IBaseEntity baseEntity)
            {
                var now = DateTime.UtcNow;
                switch (entry.State)
                {
                    case EntityState.Added:
                        baseEntity.Created = now;
                        baseEntity.Changed = now;
                        break;
                    case EntityState.Modified:
                        baseEntity.Changed = now;
                        break;
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        PreSave();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges(bool accept)
    {
        PreSave();
        return base.SaveChanges(accept);
    }

    public T SaveInDb<T>(object o)
    {
        return (T)this.Add(o).Entity;
    }
}