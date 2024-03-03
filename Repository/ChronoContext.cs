﻿using CommonInterfaces.Configuration;
using CommonInterfaces.Models.BaseEntities;
using Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.Models;

namespace Repository;

public class ChronoContext(IAppSettings appSettings) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            appSettings.GetConnectionString().ExposeSecret(), 
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