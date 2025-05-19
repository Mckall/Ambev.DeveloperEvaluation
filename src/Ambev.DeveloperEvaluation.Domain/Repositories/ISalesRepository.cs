using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISalesRepository
{
    Task<Sale?> GetByIdAsync(Guid id);
    Task<List<Sale>> GetAllAsync();
    Task AddAsync(Sale sale);
    Task UpdateAsync(Sale sale);
    Task CancelAsync(Guid id);
    Task<bool> SaveChangesAsync();
}
