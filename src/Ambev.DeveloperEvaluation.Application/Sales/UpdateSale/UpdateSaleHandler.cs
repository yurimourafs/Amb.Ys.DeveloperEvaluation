using Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Application.SalesItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for UpdateSaleCommand
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var saleWIthSameNumber = await _saleRepository.GetByNumberAsync(command.SaleNumber, cancellationToken);
        if (saleWIthSameNumber != null && saleWIthSameNumber.Id != command.Id)
            throw new InvalidOperationException($"Sale with the number {command.SaleNumber} already exists");

        var itemsValidator = new UpdateSaleItemCommandValidator();
        if (command.Items is not null)
        {
            foreach (var item in command.Items)
            {
                var itemValidationResult = await itemsValidator.ValidateAsync(item, cancellationToken);
                if (!itemValidationResult.IsValid)
                    throw new ValidationException(itemValidationResult.Errors);
            }
        }

        var sale = _mapper.Map<Sale>(command);
        sale.Update();

        var domainValidation = sale.Validate();
        if (!domainValidation.IsValid)
            throw new ValidationException(string.Join(".", domainValidation.Errors.Select(v => v.Detail)));

        sale.ApplyDiscount();

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        return _mapper.Map<UpdateSaleResult>(sale);
    }
}