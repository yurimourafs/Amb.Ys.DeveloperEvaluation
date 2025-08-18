using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Query to retrieve a sale by its SaleNumber
    /// </summary>
    public class GetSaleQuery : IRequest<GetSaleResult>
    {
        /// <summary>
        /// The sale number to search for
        /// </summary>
        public Guid Id { get; set; }

        public GetSaleQuery(Guid id)
        {
            Id = id;
        }
    }
}
