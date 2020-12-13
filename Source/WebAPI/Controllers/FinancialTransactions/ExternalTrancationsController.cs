using System.Threading.Tasks;
using MakeMeRich.Application;
using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.DeleteExternalTransaction;
using MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.UpdateExternalTransaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeMeRich.WebAPI.Controllers.FinancialTransactions
{
    public class ExternalTrancationsController : ApiController
    {
        /// <summary>
        /// Get an external transacton by id with its transaction categories.
        /// </summary>
        [HttpGet("{id}", Name = "GetExternalTransactionById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ExternalTransactionDto>> GetById(int id)
        {
            return await Mediator.Send(
                new GetExternalTransactionByIdQuery { Id = id });
        }

        /// <summary>
        /// Update an existing external transaction and its transaction categories.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, UpdateExternalTransactionCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Delete an existing external transaction by id with its transaction categories.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id, DeleteExternalTransactionCommand command)
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
