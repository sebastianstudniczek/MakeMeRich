using System;
using System.Collections.Generic;
using System.Text;

using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.Common.Commands
{
    public class UpdateFinancialTransactionCommand : IRequest
    {
        public int TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
    }
}
