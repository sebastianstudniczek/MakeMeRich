using System.Linq;
using MakeMeRich.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace MakeMeRich.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(error => error.Description));
        }
    }
}
