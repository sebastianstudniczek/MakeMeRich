using System.Threading.Tasks;
using MakeMeRich.Application;
using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeMeRich.WebAPI.Controllers
{
    public class ExternalTrancationsController : ApiController
    {
        [HttpGet("{id}", Name = "GetExternalTransactionById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ExternalTransactionDto>> GetById(int id)
        {
            return await Mediator.Send(
                new GetExternalTransactionByIdQuery {Id = id });
        }

    }
}
