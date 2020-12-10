using System.Threading.Tasks;
using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using MakeMeRich.Application.FinancialTransactions.InternalTransactions.Queries.GetInternalTransactionById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeMeRich.WebAPI.Controllers.FinancialTransactions
{
    public class InternalTransactionsController : ApiController
    {
        [HttpGet("{id}", Name = "GetInternalTransactionById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<InternalTransactionDto>> GetById(int id)
        {
            return await Mediator.Send(
                new GetInternalTransactionByIdQuery { Id = id });
        }
    }
}
