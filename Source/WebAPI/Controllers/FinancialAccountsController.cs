using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MakeMeRich.Application.FinancialAccounts.Queries;
using MakeMeRich.Application.FinancialAccounts.Queries.Dtos;

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
    }
}
