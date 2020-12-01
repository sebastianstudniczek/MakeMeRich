using MakeMeRich.Application.Common.Dtos;
using MediatR;
using System.Collections.Generic;

namespace MakeMeRich.Application
{
    public class GetFinancialCategoriesQuery : IRequest<List<FinancialCategoryDto>>
    {
    }
}