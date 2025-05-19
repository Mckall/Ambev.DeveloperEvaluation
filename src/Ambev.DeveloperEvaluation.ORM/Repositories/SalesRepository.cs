using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SalesRepository : ISalesRepository
{
    private readonly DefaultContext _context;

    public SalesRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Sale?> GetByIdAsync(Guid id)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Sale>> GetAllAsync()
    {
        return await _context.Sales
            .Include(s => s.Items)
            .ToListAsync();
    }

    public async Task AddAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
    }

    public async Task UpdateAsync(Sale sale)
    {
        _context.Sales.Update(sale);
    }

    public async Task CancelAsync(Guid id)
    {
        var sale = await GetByIdAsync(id);
        if (sale == null)
            throw new InvalidOperationException("Sale not founded.");

        sale.CancelSale();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
