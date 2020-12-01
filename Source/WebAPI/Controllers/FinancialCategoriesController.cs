using MakeMeRich.Application;
using MakeMeRich.Application.Common.Dtos;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeMeRich.WebAPI.Controllers
{
    public class FinancialCategoriesController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<FinancialCategoryDto>> GetAll()
        {
            return await Mediator.Send(new GetFinancialCategoriesQuery());
        }

    }
}
