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
        /// <summary>
        /// Get an internal transacton by id.
        /// </summary>
        [HttpGet("{id}", Name = "GetInternalTransactionById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<InternalTransactionDto>> GetById(int id)
        {
            return await Mediator.Send(
                new GetInternalTransactionByIdQuery { Id = id });
        }

        /// <summary>
        /// Update an existing internal transaction.
        /// </summary>
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

        /// <summary>
        /// Delete an existing internal transaction by id.
        /// </summary>
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
