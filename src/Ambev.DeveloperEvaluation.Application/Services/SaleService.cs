using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Services;

public class SaleService : ISaleService
{
    private readonly ISalesRepository _saleRepository;

    public SaleService(ISalesRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public Task CancelSaleAsync(Guid saleId)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> CreateSaleAsync(Sale sale)
    {
        sale.ValidateBusinessRules();

        await _saleRepository.AddAsync(sale);
        return sale.Id;
    }

    public Task<IEnumerable<Sale>> GetAllAsync()
    {
        return default;
    }

    public Task<Sale?> GetByIdAsync(Guid saleId)
    {
        return default;
    }

    public Task UpdateSaleAsync(Sale sale)
    {
        return default;
    }
}
