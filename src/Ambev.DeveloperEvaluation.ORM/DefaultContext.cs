using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    private readonly IDomainEventDispatcher _dispatcher;

    public DbSet<User> Users { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options, IDomainEventDispatcher dispatcher) : base(options)
    {
        _dispatcher = dispatcher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Ignore<DomainEvent>();

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("Sales");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.SaleNumber).IsRequired();
            entity.Property(s => s.ClientName).IsRequired();
            entity.Property(s => s.BranchName).IsRequired();

            entity.HasMany(s => s.Items)
                  .WithOne()
                  .HasForeignKey("SaleId")
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.ToTable("SaleItems");
            entity.HasKey(i => i.Id);
            entity.Property(i => i.ProductName).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = ChangeTracker.Entries<IHasDomainEvents>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        var events = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in events)
            await _dispatcher.DispatchAsync(domainEvent);

        domainEntities.ForEach(entity => entity.Entity.DomainEvents.Clear());

        return result;
    }
}