using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of ISaleRepository using Entity Framework Core
    /// </summary>
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Sale?> GetByNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.FirstOrDefaultAsync(s => s.SaleNumber == saleNumber, cancellationToken);
        }

        public async Task<(List<Sale>, int, int, int)> ListPaginatedAsync(int page, int size, string order, CancellationToken cancellationToken = default)
        {
            var query = _context.Sales.AsQueryable();

            // Simple ordering by property name (default: SaleNumber)
            query = order switch
            {
                "Date" => query.OrderBy(s => s.Date),
                "TotalAmount" => query.OrderBy(s => s.TotalAmount),
                _ => query.OrderBy(s => s.SaleNumber)
            };

            var totalItems = await query.CountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(totalItems / (double)size);

            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return (items, totalItems, totalPages, page);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken);
            if (sale == null)
                return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
