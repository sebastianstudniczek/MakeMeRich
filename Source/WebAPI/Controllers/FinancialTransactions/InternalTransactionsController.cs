using System.Threading.Tasks;
using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using MakeMeRich.Application.FinancialTransactions.InternalTransactions.Commands.DeleteInternalTransaction;
using MakeMeRich.Application.FinancialTransactions.InternalTransactions.Commands.UpdateInternalTransaction;
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

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, UpdateInternalTransactionCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id, DeleteInternalTransactionCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
