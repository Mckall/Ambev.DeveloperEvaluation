using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Interfaces;

public interface ISaleService
{
    Task<Guid> CreateSaleAsync(Sale sale);
    Task UpdateSaleAsync(Sale sale);
    Task CancelSaleAsync(Guid saleId);
    Task<Sale?> GetByIdAsync(Guid saleId);
    Task<IEnumerable<Sale>> GetAllAsync();
}
