using MakeMeRich.Application.FinancialTransactions.Common.Commands;

namespace MakeMeRich.Application.FinancialTransactions.Expenses.Commands.CreateExpense
{
    public class CreateExpenseCommand : CreateFinancialTransactionCommand
    {
        public string PayeeName { get; set; }
    }
}
