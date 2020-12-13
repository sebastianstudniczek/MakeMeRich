using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using MakeMeRich.Application;
using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using MakeMeRich.Application.FinancialAccounts.Commands.DeleteFinancialAccount;
using MakeMeRich.Application.FinancialAccounts.Commands.UpdateFinancialAccount;
using MakeMeRich.Application.FinancialAccounts.Queries.GetFinancialAccountById;
using MakeMeRich.Application.FinancialAccounts.Queries.GetFinancialAccounts;
using MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.CreateExternalTransaction;
using MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Queries.GetExternaltransactionsForFinancialAccountQuery;
using MakeMeRich.Application.FinancialTransactions.InternalTransactions.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeMeRich.WebAPI.Controllers
{
    public class FinancialAccountsController : ApiController
    {
        #region FinancialAccounts
        /// <summary>
        /// Get all financial accounts.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FinancialAccountDto>>> GetAll()
        {
            return await Mediator.Send(new GetFinancialAccountsQuery());
        }

        /// <summary>
        /// Get a financial account by id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialAccountDto>> GetById(int id)
        {
            return await Mediator.Send(new GetFinancialAccountByIdQuery { Id = id });
        }

        /// <summary>
        /// Create a new financial account.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FinancialAccountDto>> Create(CreateFinancialAccountCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Update an existing financial account.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, UpdateFinancialAccountCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Delete an existing financial account by id.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteFinancialAccountCommand { Id = id });

            return NoContent();
        }
        #endregion

        #region ExternalTransactions

        /// <summary>
        /// Get all external transactions for a specific financial account.
        /// </summary>
        [HttpGet("{id}/externaltransactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ExternalTransactionDto>>> GetExternalTransactions(int id)
        {
            return await Mediator.Send(
                new GetExternalTransactionsForFinancialAccountQuery
                {
                    FinancialAccountId = id
                });
        }

        /// <summary>
        /// Create a new external transaction for a specific financial account.
        /// </summary>
        [HttpPost("{id}/externaltransactions")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExternalTransactionDto>> CreateExternalTransaction(int id, CreateExternalTransactionCommand command)
        {
            if (id != command.FinancialAccountId)
            {
                return BadRequest();
            }

            var dto = await Mediator.Send(command);

            return CreatedAtRoute(
                "GetExternalTransactionById",
                new { dto.Id },
                dto);
        }
        #endregion

        #region InternalTransactions

        /// <summary>
        /// Get all internal transactions for a specific financial account.
        /// </summary>
        [HttpGet("{id}/internaltransactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<InternalTransactionDto>>> GetInternalTransactions(int id)
        {
            return await Mediator.Send(
                new GetInternalTransactionsForFinancialAccountQuery
                {
                    FinancialAccountId = id
                });
        }

        /// <summary>
        /// Create a new internal transaction for a specific financial account.
        /// </summary>
        [HttpPost("{id}/internaltransactions")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InternalTransactionDto>> CreateInternalTransaction(int id, CreateInternalTransactionCommand command)
        {
            if (id != command.SendingAccountId)
            {
                return BadRequest();
            }

            var dto = await Mediator.Send(command);

            return CreatedAtRoute(
                "GetInternalTransactionById",
                new { dto.Id },
                dto);
        }
        #endregion
    }
}
