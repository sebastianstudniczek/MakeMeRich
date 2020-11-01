using System;

using MakeMeRich.Application.FinancialTransactions.Common.Commands;
using MakeMeRich.Domain.Enums;

namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.UpdateExternalTransaction
{
    public class UpdateExternalTransactionCommand : UpdateFinancialTransactionCommand
    {
        public int Id { get; set; }
        public string TransactionSideName { get; set; }
        public ExternalFinancialTransactionType Type { get; set; }
        public int FinancialAccountId { get; set; }
    }
}