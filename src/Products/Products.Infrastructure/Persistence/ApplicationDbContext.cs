using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Products.Application.Interfaces;
using Products.Domain.Entities;

namespace Products.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.AddInboxStateEntity(entity => entity.ToTable("InboxState", "meta"));
        builder.AddOutboxStateEntity(entity => entity.ToTable("OutboxState", "meta"));
        builder.AddOutboxMessageEntity(entity => entity.ToTable("OutboxMessage", "meta"));
    }
}