﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Application.FinancialTransactions.Common.Commands;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;
using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.CreateExternalTransaction
{
    public class CreateExternalTransactionCommand : CreateFinancialTransactionCommand, IRequest<ExternalTransactionDto>
    {
        public int FinancialAccountId { get; set; }
        public string TransactionSideName { get; set; }
        public ExternalTransactionType TransactionType { get; set; }
        public IEnumerable<ExternalTransactionCategoryCreateDto> TransactionCategories { get; set; }
    }

    public class CreateExternalTransactionCommandHandler : IRequestHandler<CreateExternalTransactionCommand, ExternalTransactionDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateExternalTransactionCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExternalTransactionDto> Handle(CreateExternalTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = new ExternalTransaction
            {
                TransactionSideName = request.TransactionSideName,
                TransactionType = request.TransactionType,
                TotalAmount = request.TotalAmount,
                DueDate = request.DueDate,
                Description = request.Description,
                FinancialAccountId = request.FinancialAccountId,
            };

            foreach (var transactionCategory in request.TransactionCategories)
            {
                entity.TransactionCategories.Add(
                    new ExternalTransactionCategory
                    {
                        FinancialCategoryId = transactionCategory.FinancialCategoryId,
                        Amount = transactionCategory.Amount,
                        Description = transactionCategory.Description
                    });
            }

            _context.ExternalTransactions.Add(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<ExternalTransactionDto>(entity);
        }
    }
}
