using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Organizations.Domain.Entities;

namespace Organizations.Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Unit> Units { get; }
    
    public DbSet<Product> Products { get; }
    
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    

    DatabaseFacade Database { get; }
}