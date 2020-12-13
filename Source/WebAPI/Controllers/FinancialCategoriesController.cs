using System.Collections.Generic;
using System.Threading.Tasks;
using MakeMeRich.Application;
using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Application.FinancialCategories.Commands.CreateFinancialCategory;
using MakeMeRich.Application.FinancialCategories.Commands.DeleteFinancialCategory;
using MakeMeRich.Application.FinancialCategories.Commands.UpdateFinancialCategory;
using MakeMeRich.Application.FinancialCategories.Queries.GetFinancialCategoryById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeMeRich.WebAPI.Controllers
{
    public class FinancialCategoriesController : ApiController
    {
        /// <summary>
        /// Get all financial categories.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FinancialCategoryDto>>> GetAll()
        {
            return await Mediator.Send(new GetFinancialCategoriesQuery());
        }

        /// <summary>
        /// Get a financial category by id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialCategoryDto>> GetById(int id)
        {
            return await Mediator.Send(new GetFinancialCategoryByIdQuery { Id = id });
        }

        /// <summary>
        /// Create a new financial category.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FinancialCategoryDto>> Create(CreateFinancialCategoryCommand command)
        {
            var dto = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { dto.Id }, dto);
        }

        /// <summary>
        /// Update an existing financial category.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, UpdateFinancialCategoryCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Delete an existing financial category by id.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteFinancialCategoryCommand { Id = id });

            return NoContent();
        }
    }
}
