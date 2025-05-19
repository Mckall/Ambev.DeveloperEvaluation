using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly DefaultContext _context;

    public SalesController(DefaultContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Sale>>> GetAll()
    {
        return await _context.Sales.Include(s => s.Items).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sale>> GetById(Guid id)
    {
        var sale = await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.Id == id);
        if (sale == null) return NotFound();
        return sale;
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateSalesRequest dto)
    {
        var items = dto.Items.Select(i => new SaleItem(i.ProductId, i.ProductName, i.Quantity, i.UnitPrice)).ToList();
        var sale = new Sale(dto.SaleNumber, dto.SaleDate, dto.ClientId, dto.ClientName, dto.BranchId, dto.BranchName, items);

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = sale.Id }, sale);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, CreateSalesRequest dto)
    {
        var sale = await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.Id == id);
        if (sale == null) return NotFound();

        var items = dto.Items.Select(i => new SaleItem(i.ProductId, i.ProductName, i.Quantity, i.UnitPrice)).ToList();
        sale.ModifySale(items);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Cancel(Guid id)
    {
        var sale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == id);
        if (sale == null) return NotFound();

        sale.CancelSale();
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
