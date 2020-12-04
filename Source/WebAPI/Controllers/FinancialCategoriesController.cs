using MakeMeRich.Application;
using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Application.FinancialCategories.Commands.CreateFinancialCategory;
using MakeMeRich.Application.FinancialCategories.Queries.GetFinancialCategoryById;

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
        public async Task<ActionResult<List<FinancialCategoryDto>>> GetAll()
        {
            return await Mediator.Send(new GetFinancialCategoriesQuery());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialCategoryDto>> GetById(int id)
        {
            return await Mediator.Send(new GetFinancialCategoryByIdQuery { Id = id });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FinancialCategoryDto>> Create(CreateFinancialCategoryCommand command)
        {
            var dto = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { dto.Id }, dto);
        }
    }
}
