using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using MakeMeRich.Application.FinancialAccounts.Queries;

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FinancialAccountDto>> Create(CreateFinancialAccountCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
