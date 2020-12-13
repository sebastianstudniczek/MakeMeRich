using System.Threading.Tasks;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Application.Common.Models;
using MakeMeRich.Application.Common.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeMeRich.WebAPI.Controllers
{
    [Route("v1/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register([FromBody] UserModel userModel)
        {
            var result = await _identityService.CreateUserAsync(userModel.Email, userModel.Password);

            if (result.Succeeded is false)
            {
                return BadRequest(new AuthenticationFailedResponse
                {
                    Errors = result.Errors
                });
            }

            return Ok(new AuthenticationSuccesResponse
            {
                Token = result.Token
            });
        }


        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Login([FromBody] UserModel userModel)
        {
            var result = await _identityService.LoginUserAsync(userModel.Email, userModel.Password);

            if (result.Succeeded is false)
            {
                return BadRequest(new AuthenticationFailedResponse
                {
                    Errors = result.Errors
                });
            }

            return Ok(new AuthenticationSuccesResponse
            {
                Token = result.Token
            });
        }
    }
}
