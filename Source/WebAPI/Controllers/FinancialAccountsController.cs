using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using MakeMeRich.Application.FinancialAccounts.Commands.DeleteFinancialAccount;
using MakeMeRich.Application.FinancialAccounts.Commands.UpdateFinancialAccount;
using MakeMeRich.Application.FinancialAccounts.Queries;
using MakeMeRich.Application.FinancialAccounts.Queries.GetFinancialAccountById;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeMeRich.WebAPI.Controllers
{
    public class FinancialAccountsController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FinancialAccountDto>>> GetAll()
        {
            return await Mediator.Send(new GetFinancialAccountsQuery());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialAccountDto>> GetById(int id)
        {
            return await Mediator.Send(new GetFinancialAccountByIdQuery { Id = id });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FinancialAccountDto>> Create(CreateFinancialAccountCommand command)
        {
            return await Mediator.Send(command);
        }

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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteFinancialAccountCommand { Id = id });

            return NoContent();
        }
    }
}
