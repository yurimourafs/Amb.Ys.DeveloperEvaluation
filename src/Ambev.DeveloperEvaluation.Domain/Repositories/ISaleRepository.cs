using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Sale entity operations
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Creates a new sale in the repository
        /// </summary>
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing sale in the repository
        /// </summary>
        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale by its unique identifier
        /// </summary>
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists sales with pagination and ordering
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="order"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// List of sales, total items, total pages, current page
        /// </returns>
        Task<(List<Sale>, int, int, int)> ListPaginatedAsync(int page, int size, string order, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a sale from the repository
        /// </summary>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Sale?> GetByNumberAsync(string saleNumber, CancellationToken cancellationToken = default);
    }
}
