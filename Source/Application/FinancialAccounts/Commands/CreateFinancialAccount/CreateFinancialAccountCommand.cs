using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Domain.Enums;
using MediatR;

using System.ComponentModel.DataAnnotations;

namespace MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount
{
    public class CreateFinancialAccountCommand : IRequest<FinancialAccountDto>
    {
        public string Title { get; set; }
        public double CurrentBalance { get; set; }

        [EnumDataType(typeof(FinancialAccountType))]
        public FinancialAccountType Type { get; set; }
    }
}
