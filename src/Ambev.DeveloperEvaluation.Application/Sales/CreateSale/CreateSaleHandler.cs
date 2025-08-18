using Ambev.DeveloperEvaluation.Application.SalesItems;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for CreateSaleCommand
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken); 
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors); 
        
        var saleWIthSameNumber = await _saleRepository.GetByNumberAsync(command.SaleNumber, cancellationToken);
        if (saleWIthSameNumber != null)
            throw new InvalidOperationException($"Sale with the number {command.SaleNumber} already exists");

        var itemsValidator = new CreateSaleItemCommandValidator();
        if (command.Items is not null)
        {
            foreach (var item in command.Items)
            {
                var itemValidationResult = await itemsValidator.ValidateAsync(item, cancellationToken);
                if (!itemValidationResult.IsValid)
                    throw new ValidationException(itemValidationResult.Errors);
            }
        }

        // validate 20 limit same items

        // apply discounts

        var sale = _mapper.Map<Sale>(command);

        await _saleRepository.CreateAsync(sale, cancellationToken);

        return _mapper.Map<CreateSaleResult>(sale);
    }
}