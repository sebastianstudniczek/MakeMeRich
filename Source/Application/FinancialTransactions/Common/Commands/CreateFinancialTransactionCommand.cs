using System;
using System.Collections.Generic;

using MakeMeRich.Domain.Entities;

using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.Common.Commands
{
    public abstract class CreateFinancialTransactionCommand : IRequest<int>
    {
        public double TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
    }
}
